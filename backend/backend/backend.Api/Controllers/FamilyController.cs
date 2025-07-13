using backend.Contracts.DTOs;
using backend.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class FamilyController : ControllerBase
{
    public readonly IFamilyService _service;

    public FamilyController(IFamilyService service)
    {
        _service = service;
    }

    [HttpPost("/upload")]
    public async Task<IActionResult> Upload([FromForm] FamilyUploadDto dto)
    {
        await _service.UploadAsync(dto);
        return Ok(new { message = "Upload successful" });
    }
}