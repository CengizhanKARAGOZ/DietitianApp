namespace DietApp.Domain.Enums;

public enum ImportJobStatus
{
    Uploaded = 1,
    Parsing = 2,
    MappingRequired = 3,
    MappingComplete = 4,
    Validating = 5,
    Processing = 6,
    Completed = 7,
    Failed = 8,
    PartialSuccess = 9
}
