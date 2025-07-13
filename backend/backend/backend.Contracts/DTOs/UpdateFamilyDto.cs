namespace backend.Contracts.DTOs;

public class UpdateFamilyDto
{
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Guid SectionId { get; set; }
    public Guid VersionId { get; set; }
    public Guid ManufacturerId { get; set; }
    public string? Description { get; set; }
}