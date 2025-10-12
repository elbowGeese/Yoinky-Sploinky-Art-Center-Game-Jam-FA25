using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remainingTime;
    public GameObject timesUp;
    public Score scoreScript;
    public Animator cloudanimator;
    //public TextMeshProUGUI finalScore;
    //public TextMeshProUGUI bestScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //finalScore.text = "Final Score:" + scoreScript.CurrentScore.ToString("00000");
        //bestScore.text = "Best Score:" + PlayerPrefs.GetInt("Highscore");
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //timesUp.SetActive(true);
            
            StartCoroutine(EndSequence());
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        
    }
    IEnumerator EndSequence()
    {
        cloudanimator.SetBool("timesup", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("End Scene");
        yield return new WaitForSeconds(2);
        cloudanimator.SetBool("scenechanged", true );
    }
}
