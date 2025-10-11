using UnityEngine;
using UnityEngine.UI;

public class DisplaySunlightState : MonoBehaviour
{
    public Image maxSunlightImage, sunlightImage, halfSunImage, nighttimeImage;
    private SunlightStates sunlightStates;

    public Color visibleColor, invisibleColor;

    public float timeToFade = 0.5f;
    private float timer = 0f;
    private Image toFadeIn, toFadeOut;

    private SunlightStates.ObjectState previousState;

    private void Start()
    {
        sunlightStates = FindFirstObjectByType<SunlightStates>();
        previousState = sunlightStates.State;
    }

    void Update()
    {
        ResetFade(sunlightStates.State);

        timer += Time.deltaTime;

        if (timer <= timeToFade && toFadeIn && toFadeOut)
        {
            toFadeIn.color = Color.Lerp(invisibleColor, visibleColor, timer / timeToFade);
            toFadeOut.color = Color.Lerp(invisibleColor, visibleColor, 1 - (timer / timeToFade));
        }
        else
        {
            switch (sunlightStates.State)
            {
                case SunlightStates.ObjectState.MaxSunlight:
                    maxSunlightImage.color = visibleColor;
                    sunlightImage.color = invisibleColor;
                    halfSunImage.color = invisibleColor;
                    nighttimeImage.color = invisibleColor;
                    break;
                case SunlightStates.ObjectState.Sunlight:
                    maxSunlightImage.color = invisibleColor;
                    sunlightImage.color = visibleColor;
                    halfSunImage.color = invisibleColor;
                    nighttimeImage.color = invisibleColor;
                    break;
                case SunlightStates.ObjectState.HalfSun:
                    maxSunlightImage.color = invisibleColor;
                    sunlightImage.color = invisibleColor;
                    halfSunImage.color = visibleColor;
                    nighttimeImage.color = invisibleColor;
                    break;
                case SunlightStates.ObjectState.Nighttime:
                    maxSunlightImage.color = invisibleColor;
                    sunlightImage.color = invisibleColor;
                    halfSunImage.color = invisibleColor;
                    nighttimeImage.color = visibleColor;
                    break;
            }
        }
    }

    private void ResetFade(SunlightStates.ObjectState currentState)
    {
        if(previousState == currentState) { return; }

        toFadeIn = GetImageFromState(currentState);
        toFadeOut = GetImageFromState(previousState);

        timer = 0f;

        previousState = currentState;
    }

    private Image GetImageFromState(SunlightStates.ObjectState state)
    {
        switch (state)
        {
            case SunlightStates.ObjectState.MaxSunlight:
                return maxSunlightImage;
            case SunlightStates.ObjectState.Sunlight:
                return sunlightImage;
            case SunlightStates.ObjectState.HalfSun:
                return halfSunImage;
            case SunlightStates.ObjectState.Nighttime:
                return nighttimeImage;
            default:
                Debug.Log("No valid state given.");
                break;
        }

        return null;
    }
}
