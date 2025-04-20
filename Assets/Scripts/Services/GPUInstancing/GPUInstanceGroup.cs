using System.Collections.Generic;
using UnityEngine;

public class GPUInstanceGroup
{
    private Mesh _mesh;
    private Material _material;
    private List<IGPUInstanceable> _instanceables;

    public GPUInstanceGroup(Mesh mesh, Material material)
    {
        _instanceables = new List<IGPUInstanceable>();
        _mesh = mesh;
        _material = material;
    }

    public void AddInstanceable(IGPUInstanceable instanceable)
    {
        //That's the limit
        if (_instanceables.Count >= 1023)
            return;

        _instanceables.Add(instanceable);
    }

    public void DrawMesh()
    {
        Matrix4x4[] matrices = new Matrix4x4[_instanceables.Count];
        for (int i = 0; i < _instanceables.Count; i++)
        {
            matrices[i] = Matrix4x4.TRS(_instanceables[i].Position, Quaternion.identity, Vector3.one);
        }

        Graphics.DrawMeshInstanced(_mesh, 0, _material, matrices);
    }
}
