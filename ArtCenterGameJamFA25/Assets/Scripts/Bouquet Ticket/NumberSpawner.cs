using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberSpawner : MonoBehaviour
{
    public TextMeshProUGUI numberDisplay;

    public RandomNumberGenerator randomNumberGeneratorReference;

    public int amountNeeded;


    void Start()
    {
        //string number = GetRandomNumber();
        amountNeeded = GetRandomNumber();

        //numberDisplay.text = amountNeeded.ToString();

        displayNumber(0);

    }

    public int GetRandomNumber()
    {
        if (randomNumberGeneratorReference != null)
        {
            return randomNumberGeneratorReference.GetRandomNumber();
        }
        else
        {
            Debug.LogWarning("halp");
            return 0;
        }
    }

    public void displayNumber( int sexy)
    {
        string toDisplay = sexy + " / " + amountNeeded;

        numberDisplay.text = toDisplay;
    }

}
