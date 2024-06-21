using UnityEngine;

public enum E_PlantState
{
    Disable,
    Enable
}

public class Plant : MonoBehaviour
{
    private E_PlantState plantState = E_PlantState.Disable;

    public E_PlantType plantType = E_PlantType.Sunflower;

    private void Start()
    {
        TranslateDisable();
    }

    private void Update()
    {
        switch(plantState)
        {
            case E_PlantState.Disable:
                DisableUpdate();
                break;

            case E_PlantState.Enable:
                EnableUpdate();
                break;

            default:
                break;
        }
    }

    private void DisableUpdate()
    {
    }

    private void EnableUpdate()
    {
    }

    private void TranslateDisable()
    {
        plantState = E_PlantState.Disable;
        GetComponent<Animator>().enabled = false;
    }
}