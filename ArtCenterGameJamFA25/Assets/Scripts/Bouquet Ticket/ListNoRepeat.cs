using UnityEngine;

public class ListNoRepeat : MonoBehaviour
    //attach the WordSpawner script to each text in the scene meant to display a flower type
    //all text objects are referecning from the same RandomWordGenerator script, and this causes them to repeat sometimes
    //do not allow duplicate words to display
{
    public RandomWordGenerator randomWordGeneratorReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
