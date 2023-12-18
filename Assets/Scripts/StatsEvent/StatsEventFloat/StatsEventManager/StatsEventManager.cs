using UnityEngine.Events;

public static class StatsEventManager 
{
    static IBounty invoker;
    static UnityAction<float> listener;

    public static void AddEventInvoker(IBounty script)
    {
        invoker = script;
        if (listener != null)
        {
            invoker.AddBountyAddedEventListener(listener);
        }
    }
    public static void AddEventListener(UnityAction<float> handler)
    {
        listener = handler;
        if (invoker != null)
        {
            invoker.AddBountyAddedEventListener(listener);
        }
    }
}
