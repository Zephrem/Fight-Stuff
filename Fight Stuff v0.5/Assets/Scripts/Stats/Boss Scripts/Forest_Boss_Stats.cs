using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_Boss_Stats : Enemy_Stats
{

    public override void UseAbility()
    {
        abilityPool.slots[0].Cast(this, playerStats);

        if (currentHealth <= maxHealth.GetValue() / 2)
        {
            abilityPool.slots[1].Cast(this, playerStats);
        }
    }
}
