using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player != null && player.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().CanClimb = true;
            print("Touching ladder");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player != null && player.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().CanClimb = false;
            print("Leaving ladder");
        }
    }
}
