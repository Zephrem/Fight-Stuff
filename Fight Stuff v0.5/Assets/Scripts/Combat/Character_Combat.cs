using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Combat : MonoBehaviour
{
    Character_Stats myStats;

    private void Start()
    {
        myStats = GetComponent<Character_Stats>();
    }

    public void Attack(Character_Stats targetStats)
    {
        int damage;
        bool isCritical = false;

        damage = Random.Range(myStats.minDamage.GetValue(), myStats.maxDamage.GetValue() + 1);

        if (Random.Range(0, 100) < myStats.critChance.GetValue())
        {
            isCritical = true;
        }

        targetStats.TakeDamage(damage, isCritical);
    }
}
