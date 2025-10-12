using UnityEngine;

public class OverarchingTicket : MonoBehaviour
{
    public A_WordSpawner[] wordSpawnerRef;

    public NumberSpawner[] numberSpawnerRef;

    public int[] numberHave = new int [3];

    public AudioSource chime;

    //public int points = 0; 
    

public bool TryToSubmit (string flowerName)
    {
        //foreach (A_WordSpawner wordSpawner in wordSpawnerRef)
        //{
        //    if (flowerName == wordSpawner.flowerName)
        //    {

        //    }
        //}

        for (int i = 0; i < wordSpawnerRef.Length; i ++  )
        {
            if (flowerName == wordSpawnerRef[i].flowerName )
            {
                if (numberHave[i] < numberSpawnerRef[i].amountNeeded)
                {
                    numberHave[i] += 1;
                    numberSpawnerRef[i].displayNumber(numberHave[i]);

                    TicketIsComplete();

                    chime.Play(); 

                    return true;
                }
            }    
        }

        return false;
    }

private void TicketIsComplete ()
    {
        for (int i = 0; i < numberSpawnerRef.Length; i ++ )
        {
            if (numberHave [i] != numberSpawnerRef[i].amountNeeded)
            {
                return;
            }

        }

        int points = 0;

        for (int i = 0; i < wordSpawnerRef.Length; i++)
        {
            points += GetPointValue(wordSpawnerRef[i].flowerName) * numberHave[i];
        }


        FindAnyObjectByType<Score>().AddScore(points);
        Destroy(gameObject);

    }

private int GetPointValue (string plantName)
    {
        if (plantName == "Daisy")
        {
            return PointDataManager.daisyPoints;
        }
        if (plantName == "Peony")
        {
            return PointDataManager.peonyPoints;
        }
        if (plantName == "Jasmine")
        {
            return PointDataManager.jasminePoints;
        }
        //if (plantName == "Daisy")
        //{
        //    return PointDataManager.daisyPoints;
        //}
        //if (plantName == "Lily")
        //{
        //    return PointDataManager.lilyPoints;
        //}
        return (0);
    }


}
