namespace EventBus;


//TODO: Just a reminder here that this once belonged to the partial class InMemoryEventBusSubscriptionManager
//though its a bullshit design
public class SubscriptionInfo
{
    public bool IsDynamic { get; }
    public Type HandlerType { get; }

    private SubscriptionInfo(bool isDynamic, Type handlerType)
    {
        IsDynamic = isDynamic;
        HandlerType = handlerType;
    }

    public static SubscriptionInfo Dynamic(Type handlerType) =>
        new SubscriptionInfo(true, handlerType);

    public static SubscriptionInfo Typed(Type handlerType) =>
        new SubscriptionInfo(false, handlerType);
}

