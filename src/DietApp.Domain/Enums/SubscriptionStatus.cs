namespace DietApp.Domain.Enums;

public enum SubscriptionStatus
{
    PendingPayment = 1,
    PaymentNotified = 2,
    Active = 3,
    Expired = 4,
    Canceled = 5,
    Suspended = 6,
    Trial = 7
}
