using HospitalLargaVida.Backend.DAL.Dtos.AgendaDto;
using HospitalLargaVida.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalLargaVida.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendasController : ControllerBase
    {
        private readonly IAgendaServices _agendaServices;

        public AgendasController(IAgendaServices agendaServices)
        {
            _agendaServices = agendaServices;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<ICollection<AgendaDetailsDto>>> GetAllAgendasAsync()
        {
            try
            {
                var agendas = await _agendaServices.GetAllAgendasAsync();
                return Ok(agendas);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No existen"))
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}", Name = "GetAgendasByIdAsync")]
        public async Task<ActionResult<ICollection<AgendaDetailsDto>>> GetAgendasByIdAsync(int id)
        {
            try
            {
                var agendas = await _agendaServices.GetAgendaByIdAsync(id);
                return Ok(agendas);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No existen"))
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "CreatedAgendaAsync")]
        public async Task<ActionResult<AgendaDetailsDto>> CreatedAgendaAsync([FromBody] CreateAgendaDto agendaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdAgenda = await _agendaServices.CreateAgendaAsync(agendaDto);
                return CreatedAtRoute("GetAgendasByIdAsync", new { id = createdAgenda.IdAgenda }, createdAgenda);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("ya existe") || ex.Message.Contains("no existe"))
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex1.Message);
            }
        }
    }
}