using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public (string, Sprite) GetInformation();
    public UnityAction<IInteractable> InteractionInterruptEvent { get; set; }
}
