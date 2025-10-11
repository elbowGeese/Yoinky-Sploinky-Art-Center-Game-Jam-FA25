using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberSpawner : MonoBehaviour
{
    public TextMeshProUGUI numberDisplay;

    public RandomNumberGenerator randomNumberGeneratorReference;


    void Start()
    {
        string number = GetRandomNumber();
        numberDisplay.text = number;

    }

    public string GetRandomNumber()
    {
        if (randomNumberGeneratorReference != null)
        {
            return randomNumberGeneratorReference.GetRandomNumber();
        }
        else
        {
            Debug.LogWarning("halp");
            return "I return nothin";
        }
    }

}
