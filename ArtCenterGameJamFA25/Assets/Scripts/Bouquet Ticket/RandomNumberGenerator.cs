using UnityEngine;

public class RandomNumberGenerator : MonoBehaviour
{
    public int GetRandomNumber()
    {
        int randomIndex = Random.Range(1, 3);
        //string randomNumber = randomIndex.ToString();

        return randomIndex;
    }

}
