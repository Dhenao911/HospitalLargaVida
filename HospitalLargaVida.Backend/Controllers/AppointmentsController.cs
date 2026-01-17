using HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto;
using HospitalLargaVida.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalLargaVida.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<ICollection<AppointmentDetailDto>>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}", Name = "GetAppointmentByIdAsync")]
        public async Task<ActionResult<AppointmentDetailDto>> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                return Ok(appointment);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No existe"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "CreateAppointmentAsync")]
        public async Task<ActionResult<AppointmentDetailDto>> CreateAppointmentAsync([FromBody] CreateAppointmentDto appointmentDto)
        {
            try
            {
                var appointmentCreated = await _appointmentService.CreateAppointmentAsync(appointmentDto);
                return CreatedAtRoute("GetAppointmentByIdAsync", new { id = appointmentCreated.AppointmentId }, appointmentCreated);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("no existe"))
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}", Name = "DeleteAppointmentAsync")]
        public async Task<ActionResult<bool>> DeleteAppointmentAsync(int id)
        {
            try
            {
                var appointmentDeleted = await _appointmentService.DeleteAppointmentAsync(id);
                return Ok(appointmentDeleted);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("no existe"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }
    }
}