using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Building/NewBuilding")]
public class BuildingSO : ScriptableObject
{
    public string Id;

    public Sprite BuildingImage;
    public string BuildingName;

    public int XSize;
    public int YSize;

    public int HitPoint;

    public string GetSizeText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(XSize);
        stringBuilder.Append("x");
        stringBuilder.Append(YSize);

        return stringBuilder.ToString();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
            Id = System.Guid.NewGuid().ToString();
    }
#endif
}
