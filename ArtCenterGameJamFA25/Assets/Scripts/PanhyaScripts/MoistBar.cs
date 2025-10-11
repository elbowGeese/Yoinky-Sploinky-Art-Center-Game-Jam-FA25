using UnityEngine;
using UnityEngine.UI;
public class MoistBar : MonoBehaviour
{
    public PlantScript plantScript;
    public float maximum;
    public float growth;
    //public Image mask;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.maxValue = plantScript.maxMoist;
        slider.minValue = plantScript.minMoist;

    }

    // Update is called once per frame
    void Update()
    {
        growth = plantScript.moisture;
        maximum = plantScript.maxMoist;

        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        slider.value = plantScript.moisture;
        //float fillAmount = (float)growth / (float)maximum;
        //mask.fillAmount = fillAmount;
    }
}
