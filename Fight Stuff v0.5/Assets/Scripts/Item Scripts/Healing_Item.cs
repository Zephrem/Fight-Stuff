using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Item", menuName = "Inventory/Healing Item")]
public class Healing_Item : Item
{
    public int healAmount;

    public override void Use()
    {
        base.Use();
        RemoveFromInventory();
        Player_Manager.instance.GetComponent<Player_Stats>().Heal(healAmount);
    }
}
