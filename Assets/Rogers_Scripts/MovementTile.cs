using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTile : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider2D tileCollider;
    [HideInInspector]
    public SpriteRenderer sprite;
    [HideInInspector]
    public bool isActive; //shows

    // Start is called before the first frame update
    void Start()
    {
        tileCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        isActive = false;
    }
    private void Update()
    {
        if(!isActive)
        {
            sprite.enabled = false;
            tileCollider.enabled = false;
        }
        else
        {
            sprite.enabled = true;
            tileCollider.enabled = true;
        }
    }
}
