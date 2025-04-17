using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "ScriptableObjects/Unit/NewUnit")]
public class UnitSO : ScriptableObject
{
    public string Id { get; private set; }
    
    public Sprite UnitImage;
    public string UnitName;

    public int HitPoint;
    public float Speed;
    public float Damage;

    [Range(1, 20)]
    public float AttackRange;
    public float AttackCooldown;

    public UnitController Prefab;
    
    void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
            Id = GUID.Generate().ToString();
    }
}
