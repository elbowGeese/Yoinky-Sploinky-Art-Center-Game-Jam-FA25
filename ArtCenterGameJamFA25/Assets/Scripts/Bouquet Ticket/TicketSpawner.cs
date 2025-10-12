using UnityEngine;

public class TicketSpawner : MonoBehaviour
{
    public GameObject ticketPrefab;

    // Update is called once per frame
    void Update()
    {


       int tickets = FindObjectsOfType<OverarchingTicket>().Length;

        if (tickets < 1 )
        {
            GameObject storeThatShit = Instantiate(ticketPrefab, transform);

        }
    }
}
