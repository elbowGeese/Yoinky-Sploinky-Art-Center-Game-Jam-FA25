using UnityEngine;

public class A_RandomWordGenerator : MonoBehaviour
{
    private static string[] wordList = { "Daisy", "Tulip", "Rose", "Baby's Breath", "Lily" };

    public string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        string randomWord = wordList[randomIndex];

        return randomWord;
    }
}
