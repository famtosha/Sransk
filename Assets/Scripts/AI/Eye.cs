﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private GameObject Weapon;
    private Weapon weapon;
    private float UpdateTime = 0.2f;
    private float TimeLeft;

    private void Start()
    {
        TimeLeft = UpdateTime;
        weapon = Weapon.GetComponent<Weapon>();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        if(TimeLeft <= 0)
        {
            TimeLeft = UpdateTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position,30,~(1 <<9));
            Debug.DrawRay(transform.position, Player.transform.position - transform.position,Color.red,1000f);
            print(hit.transform.gameObject.name);
            print(Player.name);
            if (hit.transform.gameObject.name == Player.name)
            {
                LookAtPlayer();
                weapon.UseItem();
                RB.AddForce((Player.transform.position - gameObject.transform.position).normalized * 50);
            }
        }
        
    }

    void LookAtPlayer()
    {
        var y = Player.transform.position;
        var dir = new Vector3(y.x - transform.position.x, y.y - transform.position.y);
        transform.up = dir;
    }
}