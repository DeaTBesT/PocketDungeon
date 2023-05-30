using UnityEngine;

public abstract class RoomEvent : ScriptableObject
{
    [SerializeField] protected float eventChance;

    public float GetEventChance => eventChance;

    public abstract void ActivateEvent();
}
