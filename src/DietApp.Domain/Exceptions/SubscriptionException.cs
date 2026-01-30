namespace DietApp.Domain.Exceptions;

public class SubscriptionException : DomainException
{
    public SubscriptionException(string message)
        : base(message, "SUBSCRIPTION_ERROR")
    {
    }

    public static SubscriptionException NotActive()
        => new("Subscription is not active. Please renew your subscription.");

    public static SubscriptionException Expired()
        => new("Subscription has expired.");

    public static SubscriptionException ClientLimitReached(int limit)
        => new($"Client limit ({limit}) has been reached for your subscription plan.");

    public static SubscriptionException StorageLimitReached()
        => new("Storage limit has been reached for your subscription plan.");
}
