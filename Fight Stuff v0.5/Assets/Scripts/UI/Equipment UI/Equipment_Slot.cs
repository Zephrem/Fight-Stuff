using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Equipment_Slot : MonoBehaviour
{
    Equipment equipment;

    Equipment_Manager equipmentManager;

    public string defaultText = "Empty";

    public TextMeshProUGUI equipmentText;

    public EquipmentSlot slotType;

    void Start()
    {
        equipmentManager = Equipment_Manager.instance;

        equipmentText.text = defaultText;
    }

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        equipmentText.text = equipment.name;
    }

    public void ClearSlot()
    {
        equipment = null;
        equipmentText.text = defaultText;
    }

    public void UseEquipment()
    {
        if(equipment != null)
        {
            equipmentManager.Unequip((int)slotType);
        }
    }
}
