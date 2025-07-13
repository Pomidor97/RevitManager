using backend.Contracts.DTOs;
using backend.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService _service;

    public FamilyController(IFamilyService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] FamilyUploadDto dto)
    {
        await _service.UploadAsync(dto);
        return Ok(new { message = "Файл успешно загружен" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var families = await _service.GetAllAsync();
        return Ok(families);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var family = await _service.GetByIdAsync(id);
        return family is null ? NotFound() : Ok(family);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFamilyDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok(new { message = "Семейство обновлено" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { message = "Семейство удалено" });
    }
}