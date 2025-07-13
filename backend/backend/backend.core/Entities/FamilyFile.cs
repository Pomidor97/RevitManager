namespace backend.core.Entities;

public class FamilyFile
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string ContentType { get; set; } = null!;

    public Family Family { get; set; } = null!;
}