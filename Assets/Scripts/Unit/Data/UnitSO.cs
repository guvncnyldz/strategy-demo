using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "ScriptableObjects/Unit/NewUnit")]
public class UnitSO : ScriptableObject
{
    public string Id;

    public Sprite UnitImage;
    public string UnitName;

    public int HitPoint;
    public float Speed;
    public float Damage;

    [Range(1, 20)]
    public float AttackRange;
    public float AttackCooldown;

    public UnitController Prefab;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (string.IsNullOrEmpty(Id))
            Id = System.Guid.NewGuid().ToString();
    }
#endif
}
