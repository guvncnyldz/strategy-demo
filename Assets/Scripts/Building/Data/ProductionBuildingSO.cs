using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProductionBuilding", menuName = "ScriptableObjects/Building/NewProductionBuilding")]
public class ProductionBuildingSO : BuildingSO
{
    public Sprite SpawnPointImage;
    public List<UnitSO> Productions;
}
