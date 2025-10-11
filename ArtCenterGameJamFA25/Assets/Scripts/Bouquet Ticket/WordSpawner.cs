using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WordSpawner : MonoBehaviour
    
{
    public TextMeshProUGUI text;

    public RandomWordGenerator randomWordGeneratorReference;

    void Start()
    {
        string word = GetRandomWord();
        text.text = word;
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