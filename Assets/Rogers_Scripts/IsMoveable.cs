using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMoveable : MonoBehaviour
{
    [HideInInspector]
    public bool isMoveable = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            isMoveable = false;
        }        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            isMoveable = true;
        }
    }
}
