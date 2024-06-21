using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    private static SunManager instance;
    public static SunManager Instance => instance;

    [SerializeField]
    private int sunPoint;

    public int SunPoint
    {
        get => sunPoint;
    }

    public TextMeshProUGUI sunPointText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateSunPointText();
    }

    private void UpdateSunPointText()
    {
        sunPointText.text = sunPoint.ToString();
    }

    public void SubSunPoint(int point)
    {
        sunPoint -= point;
        UpdateSunPointText();
    }
}