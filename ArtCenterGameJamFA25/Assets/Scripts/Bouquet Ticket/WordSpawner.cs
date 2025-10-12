using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class A_WordSpawner : MonoBehaviour
    
{
    //store the string not temp
    public string flowerName;

    public TextMeshProUGUI text;

    public A_RandomWordGenerator randomWordGeneratorReference;

    void Start()
    {
        //string word = GetRandomWord();
        flowerName = GetRandomWord() ;

        text.text = flowerName;
    }

    public string GetRandomWord()
    {
        if (randomWordGeneratorReference != null)
        {
            return randomWordGeneratorReference.GetRandomWord();
        }
        else
        {
            Debug.LogWarning("RandomWordGenerator reference not set!");
            return "i return nothin";
        }
    }
}