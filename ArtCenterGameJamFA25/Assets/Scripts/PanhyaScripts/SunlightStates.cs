using UnityEngine;

public class SunlightStates : MonoBehaviour
{
    public enum ObjectState
    {
        MaxSunlight,
        Sunlight,
        HalfSun,
        Nighttime
    }

    public bool maxsun;
    public bool normalsun;
    public bool minsun;
    public bool nosun;

    public ObjectState State;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case ObjectState.MaxSunlight:
                mSun();
                break;
            case ObjectState.Sunlight:
                Sun();
                break;
            case ObjectState.HalfSun:
                minSun();
                break;
            
            case ObjectState.Nighttime:
                noSun();
                break;
        }
    }

    void mSun()
    {
        normalsun = false;
        maxsun = true;
        minsun = false;
        nosun = false;

    }

    void Sun()
    {
        normalsun = true;
        maxsun = false;
        minsun = false;
        nosun = false;
        
    }
    void minSun()
    {
        normalsun = false;
        maxsun = false;
        minsun = true;
        nosun = false;
    }
    void noSun()
    {
        normalsun = false;
        maxsun = false;
        minsun = false;
        nosun = true;
    }


}
