﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public GameObject bulletPrefab;
    public int BulletSpeed;

    private int AmmoLeft;
    private float FiringRate;
    private float NextFire;
    private bool IsReloading = false;

    new private void Start()
    {
        base.Start();
        WeaponData weaponData = (WeaponData)ItemDataCurrend;
        AmmoLeft = weaponData.MagazineCapacity;
        FiringRate = weaponData.FiringRate;
    }

    public override string GetInfo()
    {
        return AmmoLeft + " / " + ((WeaponData)ItemDataCurrend).MagazineCapacity;
    }

    public override void EquipItem()
    {
        base.EquipItem();
        IsReloading = false;
    }

    public virtual void Reload()
    {
        StartCoroutine(StartReload());
    }

    private IEnumerator StartReload()
    {
        IsReloading = true;
        yield return new WaitForSeconds(((WeaponData)ItemDataCurrend).ReloadTime);
        AmmoLeft = ((WeaponData)ItemDataCurrend).MagazineCapacity;
        IsReloading = false;
    }

    public override void UseItem()
    {

        if (Time.time > NextFire && !IsReloading)
        {
            NextFire = Time.time + FiringRate;
            if (AmmoLeft > 0)
            {
                Vector3 Start = gameObject.transform.position;

                Vector2 TrueStart = Start;
                Vector2 TrueDirect = transform.right;

                RaycastHit2D hit = Physics2D.Raycast(TrueStart, TrueDirect, 30,~(1 << 8));

                if (hit)
                { 
                    var target = hit.transform.gameObject.GetComponent<ITarget>();
                    if(target != null)
                    {
                        target.DealDamage(((WeaponData)ItemDataCurrend).Damage);
                    }

                    var targetRB = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                    if (targetRB)
                    {
                        targetRB.AddForce(TrueDirect.normalized * 100);
                    }

                    Debug.DrawRay(TrueStart, hit.point - TrueStart, Color.red, 0.2f);
                }

                
                AmmoLeft--;
            }
        }        
    }
}
