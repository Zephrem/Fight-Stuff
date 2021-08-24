using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Character_Stats
{

    public int level { get; private set; }
    public int xpToLevel { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        Equipment_Manager.instance.onEquipmentChanged += OnEquipmentChanged;
        level = 1;
        xpToLevel = NextLevel();
    }

    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            armor.AddModifier(newItem.armorMod);
            damage.AddModifier(newItem.damageMod);
        }

        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorMod);
            damage.RemoveModifier(oldItem.damageMod);
        }
    }

    public int NextLevel()
    {
        return Mathf.RoundToInt((4 * Mathf.Pow(level, 3)) / 5); 
    }

    public void GainXP(int xpAmount)
    {
        xp += xpAmount;
        if (xp >= xpToLevel)
        {
            while(xp >= xpToLevel)
            {
                level += 1;
                xp -= xpToLevel;
                xpToLevel = NextLevel();
            }
        }
    }

    public override void Die()
    {
        base.Die();
        Player_Manager.instance.KillPlayer();
    }
}
