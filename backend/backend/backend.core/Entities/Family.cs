namespace backend.core.Entities;

public class Family
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Guid SectionId { get; set; }
    public Guid VersionId { get; set; }
    public Guid ManufacturerId { get; set; }
    public string? Description { get; set; }
    public string FilePath { get; set; } = null!;
    public DateTime UploadDate { get; set; }
    public DateTime LastUpdate { get; set; }
}