using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Enemy_Stats : Character_Stats
{
    public delegate void OnEnemyDeath();
    public OnEnemyDeath OnEnemyDeathCallback;

    protected Battle_Manager battleManager;
    public Drop_Table dropTable;
    
    public Ability_Pool abilityPool;
    protected SkeletonAnimation myAnim;
    protected Player_Stats playerStats;

    void Start()
    {
        if (GetComponent<Ability_Pool>() != null)
        {
            abilityPool = GetComponent<Ability_Pool>();
        }

        if (GetComponent<SkeletonAnimation>())
        {
            myAnim = GetComponent<SkeletonAnimation>();
        }

        battleManager = GameObject.Find("Battle Manager").GetComponent<Battle_Manager>();
        playerStats = Player_Manager.instance.GetComponent<Player_Stats>();
    }

    private void Update()
    {
        UseAbility();
    }

    public override void Die()
    {
        base.Die();

        if (myAnim)
        {
            myAnim.AnimationState.SetAnimation(0, "Dead", false);
            myAnim.AnimationState.Complete += OnDeadAnimComplete;
        }

        if (OnEnemyDeathCallback != null)
        {
            OnEnemyDeathCallback.Invoke();
        }

        playerStats.GainXP(xp);
        DropItem();
    }

    void OnDeadAnimComplete(TrackEntry trackEntry)
    {
        myAnim.AnimationState.ClearTracks();
        StartCoroutine(battleManager.DelayedSpawn());
    }

    public void DropItem()
    {
        if (dropTable)
        {
            Inventory_Manager.instance.Add(dropTable.rollItem());
        }
    }

    public virtual void UseAbility()
    {
        if (abilityPool != null && currentHealth > 0)
        {
            foreach(Ability_Slot slot in abilityPool.slots)
            {
                if (slot.state == Ability_Slot.AbilityState.ready)
                {
                    slot.Cast(this, playerStats);
                }
            }
        }
    }
}
