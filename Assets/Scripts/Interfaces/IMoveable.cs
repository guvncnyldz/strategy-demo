using UnityEngine;

public interface IMoveable : ICommandable
{
    public void Move(IGridNode target);
}
