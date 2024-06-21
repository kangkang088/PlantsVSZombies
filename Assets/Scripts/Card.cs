using UnityEngine;
using UnityEngine.UI;

public enum E_CardState
{
    Cooling, WaitingSUn, Ready
}

public enum E_PlantType
{
    Sunflower, Peashooter
}

public class Card : MonoBehaviour
{
    private E_CardState cardState = E_CardState.Cooling;

    public E_PlantType plantType = E_PlantType.Sunflower;

    public GameObject cardLight;
    public GameObject cardGray;
    public Image cardMask;

    [SerializeField]
    private float cdTime = 2;

    private float cdTimer = 0;

    [SerializeField]
    private int needSunPoint = 50;

    private void Update()
    {
        switch(cardState)
        {
            case E_CardState.Cooling:
                CoolingUpdate();
                break;

            case E_CardState.WaitingSUn:
                WaitingUpdate();
                break;

            case E_CardState.Ready:
                ReadyUpdate();
                break;

            default:
                break;
        }
    }

    private void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;

        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;

        if(cdTimer >= cdTime)
        {
            TranslateToWaitingSun();
        }
    }

    private void WaitingUpdate()
    {
        if(needSunPoint <= SunManager.Instance.SunPoint)
        {
            TranslateToReady();
        }
    }

    private void ReadyUpdate()
    {
        if(needSunPoint > SunManager.Instance.SunPoint)
        {
            TranslateToWaitingSun();
        }
    }

    private void TranslateToWaitingSun()
    {
        cardState = E_CardState.WaitingSUn;

        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }

    private void TranslateToReady()
    {
        cardState = E_CardState.Ready;

        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }

    private void TranslateToCooling()
    {
        cardState = E_CardState.Cooling;

        cdTimer = 0;

        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }

    public void OnClick()
    {
        if(needSunPoint > SunManager.Instance.SunPoint)
            return;

        //消耗阳光值，进行种植
        bool isSuccessful = HandManager.Instance.AddPlant(plantType);
        if(isSuccessful)
        {
            SunManager.Instance.SubSunPoint(needSunPoint);

            //状态转变
            TranslateToCooling();
        }
    }
}