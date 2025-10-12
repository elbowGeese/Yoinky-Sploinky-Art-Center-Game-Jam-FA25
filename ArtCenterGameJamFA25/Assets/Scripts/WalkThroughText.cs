using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class WalkThroughText : MonoBehaviour
{
    public TMP_Text textDisplay;
    public GameObject waitingIndicator;
    public float speed = 0.3f;
    public Animator fadeAnim;
    public string[] dialogs;

    void Start()
    {
        waitingIndicator.SetActive(false);
        textDisplay.text = "";

        StartCoroutine(WalkThroughRoutine());
    }

    IEnumerator WalkThroughRoutine()
    {
        string displaying = "";

        for (int i = 0; i < dialogs.Length; i++)
        {
            displaying += "\n\n";
            string next = dialogs[i];

            // set progress of dialog by character
            bool done = false;
            bool interrupted = false;
            while (!done && !interrupted)
            {
                char c = next[0];
                next = next.Substring(1);
                displaying += c;
                textDisplay.text = displaying;

                if (next == string.Empty)
                {
                    Debug.Log("String is empty... moving on");
                    done = true;
                }

                yield return new WaitForSeconds(speed * Time.deltaTime);
            }

            // set progress of dialog by block
            Debug.Log("Showing full display");
            string fullDisplay = "";
            for(int j = 0; j <= i; j++)
            {
                fullDisplay += "\n\n";
                fullDisplay += dialogs[j];  
            }
            textDisplay.text = fullDisplay;

            // wait for input
            Debug.Log("Waiting for input.");
            waitingIndicator.SetActive(true);
            while (!Mouse.current.leftButton.wasPressedThisFrame)
            {
                yield return null;
            }
            waitingIndicator.SetActive(false);
        }

        // display entire dialog together
        Debug.Log("Showing complete display");
        string completeDisplayText = "";
        for (int j = 0; j < dialogs.Length; j++)
        {
            completeDisplayText += "\n\n";
            completeDisplayText += dialogs[j];
        }

        Debug.Log("Waiting for final input");
        // wait for input
        waitingIndicator.SetActive(true);
        while (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            yield return null;
        }
        waitingIndicator.SetActive(false);

        fadeAnim.SetTrigger("fadeIn");
    }
}
