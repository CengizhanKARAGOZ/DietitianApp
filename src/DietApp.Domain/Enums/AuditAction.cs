namespace DietApp.Domain.Enums;

public enum AuditAction
{
    Login = 1,
    Logout = 2,
    FailedLogin = 3,
    PasswordChange = 4,

    Create = 10,
    Read = 11,
    Update = 12,
    Delete = 13,

    Export = 20,
    Import = 21,

    SubscriptionCreated = 30,
    SubscriptionActivated = 31,
    SubscriptionExpired = 32,
    SubscriptionCanceled = 33,
    PaymentNotified = 34,
    PaymentApproved = 35,
    PaymentRejected = 36,

    Impersonation = 40,
    TenantSuspended = 41,

    FileUpload = 50,
    FileDownload = 51,
    FileDelete = 52
}
