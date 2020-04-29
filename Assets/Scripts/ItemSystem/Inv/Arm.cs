﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public List<Inventory> inventorys;
    public AnotherUI anotherUI;

    public bool IsAnyChestOpened = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var item = collision.gameObject;
        if (item.GetComponent<Item>() != null)
        {
            PickUpItem(item);
        }
    }

    public void OpenInventory(Inventory inventory)
    {
        anotherUI.OpenInventory(inventory);
        IsAnyChestOpened = true;
    }

    public void CloseInventory()
    {
        anotherUI.CloseInvenotory();
        IsAnyChestOpened = false;
    }

    private void PickUpItem(GameObject Item)
    {
        foreach (var inv in inventorys)
        {
            if (inv.AddToFreeSlot(Item))
            {
                Item.SetActive(false);
                Item.transform.SetParent(gameObject.transform);
                break;
            }
        }
     
    }

    public void DropItem(GameObject Item)
    {
        if (Item == null) return;
        Item.SetActive(true);
        Item.transform.SetParent(null);
        Item.transform.Translate(2, 0, 0);
    }
}