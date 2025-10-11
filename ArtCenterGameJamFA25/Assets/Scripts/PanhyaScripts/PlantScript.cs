using UnityEngine;
using UnityEngine.UI;
public class PlantScript : MonoBehaviour
{
    public SunlightStates SunlightScript;
    public float plantGrowth;
    public bool wantsMaxSun;
    public bool wantsSun;
    public bool wantsMinSun;
    public bool wantsNoSun;
    //public float time;
    public float minMoist;
    public float maxMoist;
    public float moisture;
    public bool isGrown;
    public bool isBurnt;
    public bool isDrowned;
    public float plantGrowthMax;
    
    

   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.deltaTime;
        
        
        
            mGrowth(timePassed);
            Grow(timePassed);
            DecayFunction(timePassed);
            MaxDecayFunction(timePassed);
            
        

        if (moisture >= maxMoist)
        {
            isDrowned = true;

        }

        if (moisture <= minMoist)
        {
            isBurnt = true;
        }
        if (plantGrowth >= plantGrowthMax)
        {
            isGrown = true;
        }
        
        //switch (currentState)
        //{
        //    case ObjectState.MaxGrowth:
        //        mGrowth();
        //        break;
        //    case ObjectState.Growth:
        //        Grow();
        //        break;
        //    case ObjectState.Decay:
        //        DecayFunction();
        //        break;
        //    case ObjectState.MaxDecay:
        //        MaxDecayFunction();
        //        break;

        //}
    }

    void mGrowth(float timePassed)
    {
        
        if (wantsMaxSun == true)
        {

            if (SunlightScript.maxsun == true)
            {

                plantGrowth += 2 * timePassed;
                moisture -= 1 * timePassed;

            }
        }  
            if (wantsSun == true)
            {
                if (SunlightScript.normalsun == true)
                {
                    plantGrowth += 2 * timePassed;
                    moisture -= 1 * timePassed;
                }

            }
            if (wantsMinSun == true)
            {
                if (SunlightScript.minsun == true)
                {
                    plantGrowth += 2 * timePassed;
                moisture += 1 * timePassed;
            }

            }
            if (wantsNoSun == true)
            {
                if (SunlightScript.nosun == true)
                {
                    plantGrowth += 2 * timePassed;
                moisture += 1 * timePassed;
            }

            }
    }
    

    void Grow(float timePassed)
    {
        if (wantsMaxSun == true)
        {
            if (SunlightScript.normalsun == true)
            {
                plantGrowth += 1 * timePassed;
                moisture -= 1 * timePassed;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                plantGrowth += 1 * timePassed;
                moisture += 1 * timePassed;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                plantGrowth +=1 * timePassed ;
                moisture += 1 * timePassed;
            }


        }
        if (wantsNoSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                plantGrowth += 1 * timePassed;
                moisture += 1 * timePassed;
            }
        }
        
    }

    void DecayFunction(float timePassed)
    {
        if (wantsMaxSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                moisture += 1 * timePassed;
                //plantGrowth -= 1;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                //plantGrowth -= 1;
                moisture += 1 * timePassed;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.normalsun == true)
            {
                //plantGrowth -= 1;
                moisture -= 1 * timePassed;
            }

        }
        if (wantsNoSun == true)
        {
            if (SunlightScript.normalsun == true)
            {
                moisture -= 1 * timePassed;
                //plantGrowth -= 1;
            }

        }
    }

    void MaxDecayFunction(float timePassed)
    {
        if (wantsMaxSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                moisture += 2 * timePassed;
                //plantGrowth -= 2;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                moisture += 2 * timePassed;
                //plantGrowth -= 2;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.maxsun == true)
            {
                moisture -= 2 * timePassed;
                //plantGrowth -= 2;
            }

        }
        if (wantsNoSun == true)
        {
            if (SunlightScript.maxsun == true)
            {
                moisture -= 2 * timePassed;
                //plantGrowth -= 2;
            }

        }
    }

    
}

