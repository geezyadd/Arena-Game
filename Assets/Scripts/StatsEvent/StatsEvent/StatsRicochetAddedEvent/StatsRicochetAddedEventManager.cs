using UnityEngine.Events;

public static class StatsRicochetAddedEventManager 
{
    static PlayerBulletController invoker;
    static UnityAction listener;

    public static void AddEventInvoker(PlayerBulletController script)
    {
        invoker = script;
        if (listener != null)
        {
            invoker.AddBountyAddedEventListener(listener);
        }
    }
    public static void AddEventListener(UnityAction handler)
    {
        listener = handler;
        if (invoker != null)
        {
            invoker.AddBountyAddedEventListener(listener);
        }
    }
}
