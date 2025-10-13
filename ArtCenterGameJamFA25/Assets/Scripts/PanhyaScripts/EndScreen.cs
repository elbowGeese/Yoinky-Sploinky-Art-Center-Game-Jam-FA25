using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestscore;
    public Animator cloudAnim;
    public GameObject average;
    public GameObject bad;
    public GameObject best;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score.text = "Final Score: " + PlayerPrefs.GetInt("MostRecentScore");
        bestscore.text = "Best Score: " + PlayerPrefs.GetInt("Highscore");
        //cloudAnim.SetBool("scenechanged", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("MostRecentScore") >= 600)
        {
            average.SetActive(true);

        }

        if (PlayerPrefs.GetInt("MostRecentScore") < 600)
        {
            bad.SetActive(true);

        }

        if (PlayerPrefs.GetInt("MostRecentScore") >= 1000)
        {
            best.SetActive(true);

        }

    }
}
