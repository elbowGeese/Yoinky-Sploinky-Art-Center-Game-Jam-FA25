using UnityEngine;

public class PlantScript : MonoBehaviour
{

    public int plantGrowth;
    public enum ObjectState
    {
        MaxGrowth,
        Growth,
        Decay,
        MaxDecay
    }

    public ObjectState currentState;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ObjectState.MaxGrowth:
                mGrowth();
                break;
            case ObjectState.Growth:
                Grow();
                break;
            case ObjectState.Decay:
                DecayFunction();
                break;
            case ObjectState.MaxDecay:
                MaxDecayFunction();
                break;

        }
    }

    void mGrowth()
    {
        
    }

    void Grow()
    {

    }

    void DecayFunction()
    {

    }

    void MaxDecayFunction()
    {

    }
}
