using UnityEngine;

[CreateAssetMenu(fileName = "VisualGridFactory", menuName = "ScriptableObjects/GridFactory/VisualGridFactory")]
public class VisualGridFactory : GridFactory
{
    public VisualGridNode visualGridNode;

    public override IGridNode GetGridNode()
    {
        //Pooling is not needed here
        return Instantiate(visualGridNode);
    }
}
