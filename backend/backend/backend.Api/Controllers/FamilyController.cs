using backend.Contracts.DTOs;
using backend.core.Interfaces;
using backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService _service;
    private readonly AppDbContext _db;

    public FamilyController(IFamilyService service, AppDbContext db)
    {
        _service = service;
        _db = db;
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
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateFamilyDto dto)
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

    [HttpGet("{id}/attachments")]
    public async Task<IActionResult> GetAttachments(Guid id)
    {
        var family = await _service.GetByIdAsync(id);
        if (family is null) return NotFound();

        var files = family.Attachments.Select(a => new
        {
            a.Id,
            a.FileName,
            a.FilePath
        });

        return Ok(files);
    }

    [HttpGet("attachments/{attachmentId}")]
    public async Task<IActionResult> DownloadAttachment(Guid attachmentId)
    {
        var file = await _db.FamilyFiles.FindAsync(attachmentId);
        if (file is null) return NotFound();

        var path = Path.Combine(Directory.GetCurrentDirectory(), file.FilePath);
        var bytes = await System.IO.File.ReadAllBytesAsync(path);
        return File(bytes, file.ContentType, file.FileName);
    }
}