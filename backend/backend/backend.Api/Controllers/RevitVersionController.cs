using backend.Contracts.DTOs;
using backend.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevitVersionController : ControllerBase
{
    private readonly IRevitVersionService _service;

    public RevitVersionController(IRevitVersionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRevitVersionDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok(new { message = "Version created." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRevitVersionDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok(new { message = "Version updated." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { message = "Version deleted." });
    }
}