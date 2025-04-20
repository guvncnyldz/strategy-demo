using System.Collections;
using System.Collections.Generic;

public class GPUService : ServiceBase
{
    private Dictionary<int, GPUInstanceGroup> _instanceGroupDict;

    protected override void Awake()
    {
        base.Awake();

        _instanceGroupDict = new();
    }

    public void Register(IGPUInstanceable instanceable, int id)
    {
        if (!_instanceGroupDict.ContainsKey(id))
        {
            GPUInstanceGroup gPUInstanceGroup = new GPUInstanceGroup(instanceable.GetGPUMesh(), instanceable.GetGPUMaterial());
            _instanceGroupDict[id] = gPUInstanceGroup;
        }

        _instanceGroupDict[id].AddInstanceable(instanceable);
    }

    void Update()
    {
        DrawMeshInstanced();
    }

    void DrawMeshInstanced()
    {
        foreach (KeyValuePair<int, GPUInstanceGroup> group in _instanceGroupDict)
        {
            group.Value.DrawMesh();
        }
    }
}
