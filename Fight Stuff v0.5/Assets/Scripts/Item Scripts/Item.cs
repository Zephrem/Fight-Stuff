using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isStackable = false;
    public int amount = 1;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        amount -= 1;

        if (amount <= 0)
        {
            Inventory_Manager.instance.Remove(this);
        }
        else
        {
            if (Inventory_Manager.instance.onItemChangedCallback != null)
            {
                Inventory_Manager.instance.onItemChangedCallback.Invoke();
            }
        }
    }
}
