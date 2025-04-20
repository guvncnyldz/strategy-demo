using UnityEngine;

public interface IGPUInstanceable
{
    public Vector3 Position { get;}
    public Mesh GetGPUMesh();
    public Material GetGPUMaterial();
}
