using UnityEngine;

[CreateAssetMenu(fileName = "NonVisualGridFactory", menuName = "ScriptableObjects/GridFactory/NonVisualGridFactory")]
public class NonVisualGridFactory : GridFactory
{
    public override IGridNode GetGridNode()
    {
        return new GridNode();
    }
}
