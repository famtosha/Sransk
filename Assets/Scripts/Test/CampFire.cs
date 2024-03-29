﻿using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] float tempPower = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == GameMan.instance.Player)
        {
            float dist = Vector3.Distance(collision.gameObject.transform.position, gameObject.transform.position);
            collision.GetComponent<Stats>().playerStats.Temperature += tempPower / dist;
        }
    }
}
