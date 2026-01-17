using HospitalLargaVida.Backend.DAL.Dtos.PatientDto;
using HospitalLargaVida.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalLargaVida.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ICollection<PatientDetailDto>>> GetAllPatientAsync()
    {
        try
        {
            var patients = await _patientService.GetAllPatient();
            return Ok(patients);
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
    [HttpGet("{id}", Name = "GetPatientByIdAsyn")]
    public async Task<ActionResult<PatientDetailDto>> GetPatientByIdAsync(string id)
    {
        try
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            return Ok(patient);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("No existen"))
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
    [HttpPost(Name = "CreatePatientAsync")]
    public async Task<ActionResult<PatientDetailDto>> CreatePatientAsync([FromBody] CreatePatientDto patientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var createdPatient = await _patientService.CreatePatientAsync(patientDto);

            return CreatedAtRoute("GetPatientByIdAsyn"
                , new { id = createdPatient.PatientId }
                , createdPatient);
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
    [HttpPut("{id}", Name = "UpdatePatientAsync")]
    public async Task<ActionResult<PatientDetailDto>> UpdatePatientDetail([FromBody] UpdatePatientDto patientDto, string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var updatedPatient = await _patientService.UpdatePatientAsync(patientDto, id);
            return Ok(updatedPatient);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("no existe"))
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex1) when (ex1.Message.Contains("ya existe"))
        {
            return Conflict(ex1.Message);
        }
        catch (Exception ex2)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex2.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeletePatientAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeletePatientAsync(string id)
    {
        try
        {
            var patientDelete = await _patientService.DeletePatientAsync(id);

            return Ok(patientDelete);
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