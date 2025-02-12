using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UploadedVirus : ICondition
{

    private ResourceManager resourceManager;
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        if (resourceManager == null)
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();

        List<BuildingClickable> availableBuildings = new List<BuildingClickable>();
        foreach (BuildingClickable building in resourceManager.buildings)
        {
            if (!building.isHacked && !building.uploadVirus && !building.downloadFiles && !building.HasTask())
                availableBuildings.Add(building);
        }
        
        if(availableBuildings.Count == 0){
            return false;
        }

        buildingClickable = availableBuildings[Random.Range(0, availableBuildings.Count)];
        return true;
    }

    public bool Check()
    {
        return buildingClickable.isHacked;
    }

    public float TimeToSolve() 
    {
        return buildingClickable.timeForVirusUpload + 5.5f;
    }

    public float ReputationLoss()
    {
        return 0.25f;
    }

    public ICondition Clone() 
    {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus()
    {
        return buildingClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon()
    {
        return ETaskIcon.UPLOAD;
    }

}