using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode()]
public class GrowthBar : MonoBehaviour
{
    public PlantScript plantScript;
    public float maximum;
    public float growth;
    public Image mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        growth = plantScript.plantGrowth;
        maximum = plantScript.plantGrowthMax;
        
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float fillAmount = (float)growth / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
