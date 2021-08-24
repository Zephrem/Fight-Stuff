using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Slot : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public AbilityState state = AbilityState.cooldown;

    private void Start()
    {
        cooldownTime = ability.cooldownTime;
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;

            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    public void Cast(Character_Stats myStats, Character_Stats targetStats)
    {
        if (state == AbilityState.ready && targetStats.currentHealth > 0 && myStats.currentHealth > 0)
        {
            ability.Cast(myStats, targetStats);
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }
    }
}
