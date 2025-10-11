using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    public float timeToIncrease = 0.4f;
    private int previousScore = 0;

    private Coroutine gradualIncreaseRoutine;

    public void DisplayScore(int score)
    {
        if(gradualIncreaseRoutine != null) { StopCoroutine(gradualIncreaseRoutine); }

        gradualIncreaseRoutine = StartCoroutine(GradualIncrease(score));

        scoreText.text = "Score: " + score.ToString("00000");
    }

    IEnumerator GradualIncrease(int score)
    {
        float target = (float) previousScore;

        float timer = 0f;
        while(timer < timeToIncrease)
        {
            timer += Time.deltaTime;

            target = Mathf.Lerp(previousScore, (float) score, timer / timeToIncrease);
            scoreText.text = "Score: " + target.ToString("00000");

            yield return null;
        }

        scoreText.text = "Score: " + score.ToString("00000");
        previousScore = score;
    }
}
