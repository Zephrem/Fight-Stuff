using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slam", menuName = "Abilities/Slam")]
public class Slam_Ability : Ability
{

    public float damageMod;

    public override void Cast(Character_Stats myStats, Character_Stats targetStats)
    {
        targetStats.TakeDamage(Mathf.RoundToInt(myStats.damage.GetValue() * damageMod));
    }
}
