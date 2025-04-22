using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMove : MonoBehaviour
{
    [HideInInspector]
    public bool isMoveable = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            isMoveable = false;
            print("" + collision.gameObject.name);
        }
        else
        {
            isMoveable = true;
            print("nothing");
        }
    }
}
