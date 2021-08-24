using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Leech", menuName = "Abilities/Leech")]
public class Leech_Ability : Ability
{
    float duration;
    public float drainFrequency;
    public int drainAmount;

    public override void Cast(Character_Stats myStats, Character_Stats targetStats)
    {
        duration = activeTime;

        Player_Manager.instance.StartCoroutine(Drain(myStats, targetStats));
    }

    IEnumerator Drain(Character_Stats myStats, Character_Stats targetStats)
    {

        int totalTicks = Mathf.FloorToInt(duration / drainFrequency);

        while (totalTicks > 0 && targetStats.currentHealth > 0 && myStats.currentHealth > 0)
        {
            targetStats.TakeDamage(drainAmount);
            myStats.Heal(drainAmount);

            totalTicks -= 1;

            yield return new WaitForSeconds(drainFrequency);
        }
    }
}
