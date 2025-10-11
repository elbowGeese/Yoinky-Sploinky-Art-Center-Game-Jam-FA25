using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public SunlightStates SunlightScript;
    public int plantGrowth;
    public bool wantsMaxSun;
    public bool wantsSun;
    public bool wantsMinSun;
    public bool wantsNoSun;
    public float time;
    public int minMoist;
    public int maxMoist;
    public int moisture;
    public bool isGrown;
    public bool isBurnt;
    public bool isDrowned;
    public int plantGrowthMax;
   
    

   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >=1)
        {
            mGrowth();
            Grow();
            DecayFunction();
            MaxDecayFunction();
            time = 0;
        }

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

    void mGrowth()
    {
        if (wantsMaxSun == true)
        {

            if (SunlightScript.maxsun == true)
            {

                plantGrowth += 2;

            }
            if (wantsSun == true)
            {
                if (SunlightScript.normalsun == true)
                {
                    plantGrowth += 2;
                }

            }
            if (wantsMinSun == true)
            {
                if (SunlightScript.minsun == true)
                {
                    plantGrowth += 2;
                }

            }
            if (wantsNoSun == true)
            {
                if (SunlightScript.nosun == true)
                {
                    plantGrowth += 2;
                }

            }
        }
    }

    void Grow()
    {
        if (wantsMaxSun == true)
        {
            if (SunlightScript.normalsun == true)
            {
                plantGrowth += 1;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                plantGrowth += 1;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                plantGrowth +=1 ;
            }

        }
        
    }

    void DecayFunction()
    {
        if (wantsMaxSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                moisture += 1;
                //plantGrowth -= 1;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                //plantGrowth -= 1;
                moisture += 1;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.normalsun == true)
            {
                //plantGrowth -= 1;
                moisture -= 1;
            }

        }
        if (wantsNoSun == true)
        {
            if (SunlightScript.normalsun == true)
            {
                moisture -= 1;
                //plantGrowth -= 1;
            }

        }
    }

    void MaxDecayFunction()
    {
        if (wantsMaxSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                moisture += 2;
                //plantGrowth -= 2;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                moisture += 2;
                //plantGrowth -= 2;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.maxsun == true)
            {
                moisture -= 2;
                //plantGrowth -= 2;
            }

        }
        if (wantsNoSun == true)
        {
            if (SunlightScript.maxsun == true)
            {
                moisture -= 2;
                //plantGrowth -= 2;
            }

        }
    }
}

