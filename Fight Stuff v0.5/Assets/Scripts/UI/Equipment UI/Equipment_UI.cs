using UnityEngine;

public class Equipment_UI : MonoBehaviour
{
    public Transform equipmentParent;
    public GameObject equipmentUI;

    Equipment_Manager equipmentManager;

    Equipment_Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = Equipment_Manager.instance;
        equipmentManager.onEquipmentChanged += UpdateUI;

        slots = equipmentParent.GetComponentsInChildren<Equipment_Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
    }

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(newItem != null && slots[i].slotType == newItem.equipSlot)
            {
                slots[i].AddEquipment(newItem);
            }
            else if(newItem == null && slots[i].slotType == oldItem.equipSlot)
            {
                slots[i].ClearSlot();
            }
        }
    }
}
