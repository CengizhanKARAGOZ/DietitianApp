namespace DietApp.Domain.Exceptions;

public class TenantException : DomainException
{
    public TenantException(string message)
        : base(message, "TENANT_ERROR")
    {
    }

    public static TenantException NotFound(Guid tenantId)
        => new($"Tenant with ID '{tenantId}' was not found.");

    public static TenantException InActive()
        => new("Tenant account is inactive.");

    public static TenantException AccessDenied()
        => new("You don't have access to this tenant's resources.");
}
