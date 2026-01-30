namespace DietApp.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string recipient, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default);
    Task SendEmailConfirmationAsync(string recipient, string confirmationLink, string language = "tr-TR", CancellationToken cancellationToken = default);
    Task SendPasswordResetAsync(string recipient, string resetLink, string language = "tr-TR", CancellationToken cancellationToken = default);
    Task SendPaymentApprovedAsync(string recipient, string planName, DateTime endDate, string language = "tr-TR", CancellationToken cancellationToken = default);
    Task SendPaymentRejectedAsync(string recipient, string reason, string language = "tr-TR", CancellationToken cancellationToken = default);
    Task SendSubscriptionExpiringAsync(string recipient, string planName, DateTime endDate, int daysRemaining, string language = "tr-TR", CancellationToken cancellationToken = default);
}
