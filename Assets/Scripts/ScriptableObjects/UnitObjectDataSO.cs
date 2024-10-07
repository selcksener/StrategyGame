using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataSO", menuName = "ScriptableObjects/BuildingSystem/UnitObjectDataSO")]
public class UnitObjectDataSO : ScriptableObject
{
    public List<UnitData> unitsData = new List<UnitData>();
}


[System.Serializable]
public class UnitData
{
    [field: SerializeField] public string Name { get; private set; }
    public UnitAttackData UnitAttackData;
    public HealthData UnitHealthData;
    public Sprite UnitUIImage;
    public PoolObjectType PoolObjectType;
    public GameObject unitPrefab;
}

[System.Serializable]
public class UnitAttackData
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float RateOfFire { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
}

[System.Serializable]
public class HealthData
{
    [field: SerializeField]public int MaxHealth { get; private set; }
    [field: SerializeField]public bool AutoRegenerate { get; private set; }
    [field: SerializeField]public float RegenerationRate { get; private set; }
}