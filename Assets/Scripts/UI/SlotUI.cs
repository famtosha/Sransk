﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image ItemSprite;
    public InvenotryUI BackPackUI;
    public Text Text;
    public UIManager UIManager;

    public int ID;

    public void UpdateData(ItemData trashData)
    {
        Debug.Log("Updating Data: " + ID);
        if (trashData.Sprite != null) ItemSprite.sprite = trashData.Sprite;
        if(trashData.Count > 1)
        {
            Text.text = trashData.Count.ToString();
        }
        else
        {
            Text.text = "";
        }
    }  

    public void Drop()
    {
        BackPackUI.DropItem(ID);
    }

    public void ChangeColor(Color color)
    {
        ItemSprite.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BackPackUI.NewClick(ID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager == null) return;

        UIManager.EnableItemContexMenu();
        UIManager.ItemContexMenu.transform.position = Input.mousePosition + new Vector3(100,-20,0);
        var item = BackPackUI.inventory.GetSlotInfo(ID);
        if(item != null)
        {
            var x = item.GetComponent<Item>();
            UIManager.ItemContexMenu.GetComponent<ItemCont>().Upadate(x.Name, x.GetInfo());
        }
        else
        {
            UIManager.ItemContexMenu.GetComponent<ItemCont>().Upadate("", "");
        }     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIManager == null) return;
        UIManager.DisableItemContexMenu();
    }
}