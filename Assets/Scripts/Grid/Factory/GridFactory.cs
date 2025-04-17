using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridFactory : ScriptableObject
{
    public abstract IGridNode GetGridNode();
}
