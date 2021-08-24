using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability_Slot : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;

    Battle_Manager currentBattle;

    [SerializeField] private KeyCode abilityKey;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public AbilityState state = AbilityState.ready;

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (currentBattle != null)
                {
                    if (Input.GetKeyDown(abilityKey) && currentBattle.enemyStats.currentHealth > 0)
                    {
                        Cast(Player_Manager.instance.GetComponent<Player_Stats>(), currentBattle.enemyStats);

                        state = AbilityState.active;
                        activeTime = ability.activeTime;
                    }
                }
                break;

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

    public void FindBattle(Battle_Manager battle)
    {
        currentBattle = battle;
    }
}
