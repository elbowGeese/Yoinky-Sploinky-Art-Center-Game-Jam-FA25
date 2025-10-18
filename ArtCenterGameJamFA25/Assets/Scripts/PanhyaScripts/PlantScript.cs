using UnityEngine;
using UnityEngine.UI;
public class PlantScript : MonoBehaviour
{
    public string plantName = "Daisy";

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
    
    public float plantGrowthMax;

    public Animator _animator;

    public AudioSource heatdeath, flooddeath;

    // animation bools
    //public bool isBurnt;
    //public bool isDrowned;
    //public bool growth2;
    //public bool growth3;
    //public bool growth4;
    public bool isDead = false;

    public bool isPaused = false;

    void Start()
    {
        SunlightScript = FindFirstObjectByType<SunlightStates>();
        isGrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused) return;
        
        float timePassed = Time.deltaTime;



        mGrowth(timePassed);
        Grow(timePassed);
        DecayFunction(timePassed);
        MaxDecayFunction(timePassed);
        
        

        //anim bools
        if (plantGrowth >= 0.30*plantGrowthMax)
        {
            _animator.SetBool("Growth2", true);
        }
        if (plantGrowth >= 0.60 * plantGrowthMax)
        {
            _animator.SetBool("Growth3", true);
        }
        if (plantGrowth >= plantGrowthMax)
        {
            _animator.SetBool("Growth4", true);
        }



        //if (moisture >= 0.90*maxMoist)
        //{
        //    _animator.SetBool("isDrowned", true);

        //}

        //if (moisture >= 0.10*maxMoist)
        //{
        //    _animator.SetBool("isBurnt", true);
        //}

        if (moisture >= maxMoist)
        {
            _animator.SetBool("isWaterDead", true);

            if (isDead == false)
            {
                flooddeath.Play();
                isDead = true;
            }

        }

        if (moisture <= minMoist)
        {
            _animator.SetBool("isBurntDead", true);

            if (isDead == false)
            {
                heatdeath.Play();
                isDead = true;
            }
        }


        if (plantGrowth >= plantGrowthMax)
        {
            isGrown = true;
        }

        // end of anim bools
        
       
    }

    void mGrowth(float timePassed)
    {
        
        if (wantsMaxSun == true)
        {

            if (SunlightScript.maxsun == true)
            {

                plantGrowth += 5 * timePassed;
                moisture -= 1 * timePassed;

            }
        }  
            if (wantsSun == true)
            {
                if (SunlightScript.normalsun == true)
                {
                    plantGrowth += 5 * timePassed;
                    moisture -= 1 * timePassed;
                }

            }
            if (wantsMinSun == true)
            {
                if (SunlightScript.minsun == true)
                {
                    plantGrowth += 5 * timePassed;
                moisture += 1 * timePassed;
            }

            }
            if (wantsNoSun == true)
            {
                if (SunlightScript.nosun == true)
                {
                    plantGrowth += 5 * timePassed;
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
                plantGrowth += 2 * timePassed;
                moisture -= 1 * timePassed;
            }

        }
        if (wantsSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                plantGrowth += 2 * timePassed;
                moisture += 1 * timePassed;
            }

        }
        if (wantsMinSun == true)
        {
            if (SunlightScript.nosun == true)
            {
                plantGrowth +=2 * timePassed ;
                moisture += 1 * timePassed;
            }


        }
        if (wantsNoSun == true)
        {
            if (SunlightScript.minsun == true)
            {
                plantGrowth += 2 * timePassed;
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

