using UnityEngine.Events;

public interface ICommandable
{
    public UnityAction<ICommandable> CommandInterruptEvent {get; set;}
}