using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_Slot : MonoBehaviour
{
    Item item;

    public Image itemIcon;

    public void AddItem (Item newItem)
    {
        item = newItem;

        if (newItem.isStackable)
        {
            //Add a stack number.
            itemIcon.sprite = item.icon;
            itemIcon.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            itemIcon.sprite = item.icon;
            itemIcon.color = new Color32(255, 255, 255, 255);
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color32(0, 0, 0, 0);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
