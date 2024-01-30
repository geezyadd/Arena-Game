using UnityEngine.Events;

public interface IBounty 
{
    void AddBountyAddedEventListener(UnityAction<float> listener);
}
