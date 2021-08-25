using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Character_Stats
{

    public int level { get; private set; }
    public int xpToLevel { get; private set; }

    int healthMod;
    int damageMod;



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
            minDamage.AddModifier(newItem.minDamageMod);
            maxDamage.AddModifier(newItem.maxDamageMod);
        }

        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorMod);
            minDamage.RemoveModifier(oldItem.minDamageMod);
            maxDamage.RemoveModifier(oldItem.maxDamageMod);
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
                LevelUp();
                xp -= xpToLevel;
                xpToLevel = NextLevel();
            }
        }
    }

    void LevelUp()
    {
        maxHealth.RemoveModifier(healthMod);
        maxDamage.RemoveModifier(damageMod);

        healthMod = level * 1;
        damageMod = Mathf.FloorToInt(level * 0.5f);

        maxHealth.AddModifier(healthMod);
        maxDamage.AddModifier(damageMod);
    }

    public override void Die()
    {
        base.Die();
        Player_Manager.instance.KillPlayer();
    }
}
