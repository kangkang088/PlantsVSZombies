using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private static HandManager instance;
    public static HandManager Instance => instance;

    public List<Plant> plantPrefabList = new();

    private Plant currentPlant;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        if(currentPlant == null)
            return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        currentPlant.transform.position = mouseWorldPosition;
    }

    public bool AddPlant(E_PlantType plantType)
    {
        if(currentPlant != null)
            return false;

        Plant plantPrefab = GetPlantPrefab(plantType);
        if(plantPrefab == null)
        {
            print("要种植的植物不存在");
            return false;
        }
        currentPlant = Instantiate(plantPrefab);
        return true;
    }

    private Plant GetPlantPrefab(E_PlantType plantType)
    {
        foreach(Plant plant in plantPrefabList)
        {
            if(plant.plantType == plantType)
                return plant;
        }
        return null;
    }

    public void OnCellClick(Cell cell)
    {
        if(currentPlant == null)
            return;

        bool isSuccessful = cell.AddPlant(currentPlant);

        if(isSuccessful)
        {
            currentPlant = null;
        }
    }
}