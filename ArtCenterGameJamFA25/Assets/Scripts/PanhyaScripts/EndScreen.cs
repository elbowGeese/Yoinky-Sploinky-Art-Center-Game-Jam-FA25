using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestscore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score.text = "Final Score:" + PlayerPrefs.GetInt("MostRecentScore");
        bestscore.text = "Best Score:" + PlayerPrefs.GetInt("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
