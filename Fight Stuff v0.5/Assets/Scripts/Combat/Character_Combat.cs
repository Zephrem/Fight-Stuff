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
        targetStats.TakeDamage(myStats.damage.GetValue());
    }
}
