using UnityEngine;

[CreateAssetMenu(menuName = "Events/New null event")]
public class NullEvent : RoomEvent
{
    public override void ActivateEvent()
    {
        return;
    }
}
