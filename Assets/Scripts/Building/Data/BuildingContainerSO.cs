using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingContainer", menuName = "ScriptableObjects/Building/BuildingContainer")]
public class BuildingContainerSO : ScriptableObject
{
    public List<BuildingSO> buildingSOList;
}
