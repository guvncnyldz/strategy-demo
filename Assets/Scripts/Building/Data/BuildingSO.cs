using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Building/NewBuilding")]
public class BuildingSO : ScriptableObject
{
    public string Id { get; private set; }

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

    void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
            Id = GUID.Generate().ToString();
    }
}
