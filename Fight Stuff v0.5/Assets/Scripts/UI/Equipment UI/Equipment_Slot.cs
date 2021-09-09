using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Equipment_Slot : MonoBehaviour
{
    Equipment equipment;

    Equipment_Manager equipmentManager;

    public string defaultText = "Empty";
    public Sprite defaultIcon;

    public TextMeshProUGUI equipmentText;
    public Image equipmentIcon;

    public EquipmentSlot slotType;

    void Start()
    {
        equipmentManager = Equipment_Manager.instance;

        //equipmentText.text = defaultText;

        equipmentIcon.sprite = defaultIcon;
    }

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        //equipmentText.text = equipment.name;
        equipmentIcon.sprite = equipment.icon;
    }

    public void ClearSlot()
    {
        equipment = null;
        //equipmentText.text = defaultText;
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
