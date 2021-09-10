using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Equipment_Slot : MonoBehaviour
{
    Equipment equipment;

    Equipment_Manager equipmentManager;

    public Sprite defaultIcon;
    public Image equipmentIcon;

    public EquipmentSlot slotType;

    void Start()
    {
        equipmentManager = Equipment_Manager.instance;
        equipmentIcon.sprite = defaultIcon;
    }

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        equipmentIcon.sprite = equipment.icon;
    }

    public void ClearSlot()
    {
        equipment = null;
        equipmentIcon.sprite = defaultIcon;
    }

    public void UseEquipment()
    {
        if(equipment != null)
        {
            equipmentManager.Unequip((int)slotType);
        }
    }
}
