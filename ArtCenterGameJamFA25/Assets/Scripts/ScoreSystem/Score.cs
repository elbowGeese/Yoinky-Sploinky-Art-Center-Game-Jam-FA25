using UnityEngine;

public class Score : MonoBehaviour
{
    private int currentScore;
    public int CurrentScore { get { return currentScore; } }

    private ScoreDisplay scoreDisplay;

    void Start()
    {
        currentScore = 0;

        scoreDisplay = gameObject.GetComponent<ScoreDisplay>();
        scoreDisplay.DisplayScore(currentScore);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;

        scoreDisplay.DisplayScore(currentScore);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("MostRecentScore", currentScore);

        int highscore = PlayerPrefs.GetInt("Highscore");
        if (currentScore > highscore)
        {
            PlayerPrefs.SetInt("Highscore", currentScore);
        }
    }
}
