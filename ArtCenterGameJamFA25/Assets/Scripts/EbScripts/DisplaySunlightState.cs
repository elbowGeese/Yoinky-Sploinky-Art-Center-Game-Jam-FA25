using UnityEngine;
using UnityEngine.UI;

public class DisplaySunlightState : MonoBehaviour
{
    public Image maxSunlightImage, sunlightImage, halfSunImage, nighttimeImage;
    private SunlightStates sunlightStates;

    public Color visibleColor, invisibleColor;

    private void Start()
    {
        sunlightStates = FindFirstObjectByType<SunlightStates>();
    }

    void Update()
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
