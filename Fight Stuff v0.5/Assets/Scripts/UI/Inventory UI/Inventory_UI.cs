using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory_Manager inventory;

    Inventory_Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory_Manager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<Inventory_Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
