namespace backend.Contracts.DTOs;

public class FamilyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string FilePath { get; set; } = null!;
    public DateTime UploadDate { get; set; }
    public DateTime LastUpdate { get; set; }

    public Guid CategoryId { get; set; }
    public Guid SectionId { get; set; }
    public Guid VersionId { get; set; }
    public Guid ManufacturerId { get; set; }

    public List<AttachmentDto> Attachments { get; set; } = new();
}