using HospitalLargaVida.Backend.DAL.Dtos.DoctorDto;
using HospitalLargaVida.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalLargaVida.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ICollection<DoctorDetailsDto>>> GetAllDoctorsAsync()
    {
        try
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
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
    [HttpGet("{id}", Name = "GetDoctorByIdAsyn")]
    public async Task<ActionResult<DoctorDetailsDto>> GetDoctorByIdAsync(string id)
    {
        try
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            return Ok(doctor);
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
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost(Name = "CreateDoctorAsync")]
    public async Task<ActionResult<DoctorDetailsDto>> CreateDoctorAsync([FromBody] CreateDoctorDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdDoctor = await _doctorService.CreateDoctorAsync(doctorDto);
            return CreatedAtRoute("GetDoctorByIdAsyn", new { id = createdDoctor.DoctorId }, createdDoctor);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("ya existe"))
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
    [ProducesResponseType(StatusCodes.Status409Conflict)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}", Name = "UpdateDoctorAsync")]
    public async Task<ActionResult<DoctorDetailsDto>> UpdateDoctorAsync(string id, [FromBody] UpdateDoctorDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctorDto, id);
            return Ok(updatedDoctor);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("ya existe"))
        {
            return Conflict(ex.Message);
        }
        catch (InvalidOperationException ex1) when (ex1.Message.Contains("no existe"))
        {
            return NotFound(ex1.Message);
        }
        catch (Exception ex2)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex2.Message);
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}", Name = "DeleteDoctorAsync")]
    public async Task<ActionResult<bool>> DeleteDoctorAsync(string id)
    {
        try
        {
            var deleted = await _doctorService.DeleteDoctorAsync(id);
            return Ok(deleted);
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