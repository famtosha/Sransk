﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [SerializeField] private GameObject ItemNowTouch = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var x = collision.gameObject.GetComponent<interactebleItem>();
        if (x)
        {
            ItemNowTouch = x.gameObject;
            x.TouchObj(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(ItemNowTouch != null)
        {
            if(collision.gameObject == ItemNowTouch)
            {
                ItemNowTouch.GetComponent<interactebleItem>().TouchObj(false);
                ItemNowTouch = null;              
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(ItemNowTouch != null)
            {
                ItemNowTouch.GetComponent<interactebleItem>().UseObj(gameObject.transform.parent.gameObject);
            }
        }
    }
}