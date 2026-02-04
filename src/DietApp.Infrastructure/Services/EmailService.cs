using DietApp.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DietApp.Infrastructure.Services;

#pragma warning disable CA1848
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string recipient, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        // TODO: SMTP veya email provider (SendGrid, AWS SES vb.) entegrasyonu yapılacak
        _logger.LogInformation("Email gönderiliyor: {Recipient}, Konu: {Subject}", recipient, subject);

        await Task.CompletedTask;
    }

    public async Task SendEmailConfirmationAsync(string recipient, string confirmationLink, string language = "tr-TR", CancellationToken cancellationToken = default)
    {
        string subject = language == "tr-TR"
            ? "E-posta Adresinizi Onaylayın"
            : "Confirm Your Email";

        string body = language == "tr-TR"
            ? $"<p>Merhaba,</p><p>E-posta adresinizi onaylamak için <a href='{confirmationLink}'>buraya tıklayın</a>.</p>"
            : $"<p>Hello,</p><p>Please <a href='{confirmationLink}'>click here</a> to confirm your email address.</p>";

        await SendEmailAsync(recipient, subject, body, true, cancellationToken);
    }

    public async Task SendPasswordResetAsync(string recipient, string resetLink, string language = "tr-TR", CancellationToken cancellationToken = default)
    {
        string subject = language == "tr-TR"
            ? "Şifre Sıfırlama Talebi"
            : "Password Reset Request";

        string body = language == "tr-TR"
            ? $"<p>Merhaba,</p><p>Şifrenizi sıfırlamak için <a href='{resetLink}'>buraya tıklayın</a>.</p><p>Bu bağlantı 1 saat geçerlidir.</p>"
            : $"<p>Hello,</p><p>Please <a href='{resetLink}'>click here</a> to reset your password.</p><p>This link is valid for 1 hour.</p>";

        await SendEmailAsync(recipient, subject, body, true, cancellationToken);
    }

    public async Task SendPaymentApprovedAsync(string recipient, string planName, DateTime endDate, string language = "tr-TR", CancellationToken cancellationToken = default)
    {
        string subject = language == "tr-TR"
            ? "Ödemeniz Onaylandı"
            : "Payment Approved";

        string body = language == "tr-TR"
            ? $"<p>Merhaba,</p><p><strong>{planName}</strong> planı için ödemeniz onaylandı.</p><p>Abonelik bitiş tarihi: {endDate:dd.MM.yyyy}</p>"
            : $"<p>Hello,</p><p>Your payment for <strong>{planName}</strong> plan has been approved.</p><p>Subscription end date: {endDate:yyyy-MM-dd}</p>";

        await SendEmailAsync(recipient, subject, body, true, cancellationToken);
    }

    public async Task SendPaymentRejectedAsync(string recipient, string reason, string language = "tr-TR", CancellationToken cancellationToken = default)
    {
        string subject = language == "tr-TR"
            ? "Ödeme Bildirimi Reddedildi"
            : "Payment Notification Rejected";

        string body = language == "tr-TR"
            ? $"<p>Merhaba,</p><p>Ödeme bildiriminiz reddedildi.</p><p>Sebep: {reason}</p><p>Lütfen tekrar deneyin veya bizimle iletişime geçin.</p>"
            : $"<p>Hello,</p><p>Your payment notification has been rejected.</p><p>Reason: {reason}</p><p>Please try again or contact us.</p>";

        await SendEmailAsync(recipient, subject, body, true, cancellationToken);
    }

    public async Task SendSubscriptionExpiringAsync(string recipient, string planName, DateTime endDate, int daysRemaining, string language = "tr-TR", CancellationToken cancellationToken = default)
    {
        string subject = language == "tr-TR"
            ? "Aboneliğiniz Sona Ermek Üzere"
            : "Your Subscription is Expiring Soon";

        string body = language == "tr-TR"
            ? $"<p>Merhaba,</p><p><strong>{planName}</strong> planı aboneliğiniz {daysRemaining} gün içinde ({endDate:dd.MM.yyyy}) sona erecek.</p><p>Kesintisiz hizmet için lütfen yenileyin.</p>"
            : $"<p>Hello,</p><p>Your <strong>{planName}</strong> subscription will expire in {daysRemaining} days ({endDate:yyyy-MM-dd}).</p><p>Please renew to continue using our services.</p>";

        await SendEmailAsync(recipient, subject, body, true, cancellationToken);
    }
}
#pragma warning restore CA1848
