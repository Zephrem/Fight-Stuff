using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    #region Singleton
    public static Inventory_Manager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space;

    public List<Item> items = new List<Item>();

    public void Add (Item item)
    {
        bool inInventory = false;

        if (item.isStackable) 
        {

            foreach (Item inventoryItem in items)
            {

                if (inventoryItem.name == item.name)
                {
                    inventoryItem.amount += item.amount;
                    inventoryItem.amount = Mathf.Clamp(inventoryItem.amount, 0, inventoryItem.stackSize);
                    inInventory = true;
                }
            }

            if (!inInventory)
            {

                if (items.Count >= space)
                {
                    Debug.Log("Not enough inventory space");
                    return;
                }

                items.Add(item);
            }
        }
        else
        {

            if (items.Count >= space)
            {
                Debug.Log("Not enough inventory space");
                return;
            }

            items.Add(item);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
