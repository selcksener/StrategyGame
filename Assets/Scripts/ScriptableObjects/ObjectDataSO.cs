using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataSO",menuName = "ScriptableObjects/BuildingSystem/ObjectDataSO")]
public class ObjectDataSO : ScriptableObject
{
    public List<ObjectData> objectsData = new List<ObjectData>();

    public ObjectData GetObjectDataById(int id)
    {
        for (int i = 0; i < objectsData.Count; i++)
        {
            if (objectsData[i].ID == id) return objectsData[i];
        }

        return null;
    }
}

[System.Serializable]
public class ObjectData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get;private set; }
    [field: SerializeField] public BuildingType buildingType;
    [TextArea(3,10)]
    public string description;
    public Vector2Int Size;
    public GameObject Prefab;
    public HealthData HealthData;
    public Sprite Image;
    public PoolObjectType PoolObjectType;
    public List<BuildRequirement> requirements = new List<BuildRequirement>();
    public List<BuildBenefits> benefits = new List<BuildBenefits>();
    [field:SerializeField] public List<BuildingDependency> buildDependency { get;private set; }
}

[System.Serializable]
public class BuildRequirement
{
    
}

[System.Serializable]
public class BuildBenefits
{
    
}

public enum BuildingType
{
    None,
    Barrack,
    SoldierUnit,
    PowerPlant
}

[System.Serializable]
public class BuildingDependency
{
    public BuildingType buildingType;
    public string productionName;
}