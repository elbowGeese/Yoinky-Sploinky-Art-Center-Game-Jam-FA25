using UnityEngine;
using UnityEngine.UI;
public class MoistBar : MonoBehaviour
{
    public PlantScript plantScript;
    public int maximum;
    public int growth;
    public Image mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        float fillAmount = (float)growth / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
