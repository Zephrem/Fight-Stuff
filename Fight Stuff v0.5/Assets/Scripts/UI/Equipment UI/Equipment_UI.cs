using UnityEngine;
using UnityEngine.UI;

public class Equipment_UI : MonoBehaviour
{
    public Transform equipmentParent;
    public GameObject equipmentUI;

    Equipment_Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        Equipment_Manager.instance.onEquipmentChanged += UpdateUI;
        slots = equipmentParent.GetComponentsInChildren<Equipment_Slot>();
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

    public void ToggleUI()
    {
        if (equipmentUI.transform.localScale == new Vector3(1, 1, 1))
        {
            equipmentUI.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            equipmentUI.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
