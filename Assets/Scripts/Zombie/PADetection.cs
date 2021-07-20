using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PADetection : MonoBehaviour
{
    public bool playerIn = false;
    //these on trigger enter functions are automatically called when an object enters the collider attached to the Patrol Area
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerIn = false;
        }
    }
}
