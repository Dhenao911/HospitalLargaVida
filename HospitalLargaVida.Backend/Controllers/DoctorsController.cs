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

    [HttpGet]
    public async Task<ActionResult<ICollection<DoctorDetailsDto>>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    [HttpGet("{id}", Name = "GetDoctorByIdAsyn")]
    public async Task<ActionResult<DoctorDetailsDto>> GetDoctorByIdAsync(string id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        return Ok(doctor);
    }

    [HttpPost(Name = "CreateDoctorAsync")]
    public async Task<ActionResult<DoctorDetailsDto>> CreateDoctorAsync([FromBody] CreateDoctorDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdDoctor = await _doctorService.CreateDoctorAsync(doctorDto);
        return CreatedAtRoute("GetDoctorByIdAsyn", new { id = createdDoctor.DoctorId }, createdDoctor);
    }

    [HttpPut("{id}", Name = "UpdateDoctorAsync")]
    public async Task<ActionResult<DoctorDetailsDto>> UpdateDoctorAsync(string id, [FromBody] UpdateDoctorDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctorDto, id);
        return Ok(updatedDoctor);
    }

    [HttpDelete("{id}", Name = "DeleteDoctorAsync")]
    public async Task<ActionResult> DeleteDoctorAsync(string id)
    {
        await _doctorService.DeleteDoctorAsync(id);
        return NoContent();
    }
}