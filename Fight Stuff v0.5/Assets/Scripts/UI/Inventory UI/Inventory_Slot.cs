using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_Slot : MonoBehaviour
{
    Item item;

    public string defaultText = "Empty";

    public TextMeshProUGUI itemText;

    public void AddItem (Item newItem)
    {
        item = newItem;

        if (newItem.isStackable)
        {
            itemText.text = item.name + " x " + item.amount;
        }
        else
        {
            itemText.text = item.name;
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemText.text = defaultText;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
