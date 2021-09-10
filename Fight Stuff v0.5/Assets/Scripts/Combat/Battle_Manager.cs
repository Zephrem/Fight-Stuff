using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

public class Battle_Manager : MonoBehaviour
{
    private float playerDelay;
    private float enemyDelay;

    public Station_UI playerStation;
    public Station_UI enemyStation;

    GameObject playerGo;
    public GameObject enemyGo { get; private set; }

    public Enemy_Stats enemyStats { get; private set; }
    Character_Combat enemyCombat;
    SkeletonAnimation enemyAnim;

    Player_Stats playerStats;
    Character_Combat playerCombat;
    SkeletonAnimation playerAnim;

    public Spawn_Table enemySpawner;
    public GameObject dungeonBoss;
    public Boss_Progress bossProgress;

    // Start is called before the first frame update
    void Start()
    {
        playerGo = Player_Manager.instance.player;
        playerStats = playerGo.GetComponent<Player_Stats>();
        playerCombat = playerGo.GetComponent<Character_Combat>();
        playerAnim = playerGo.GetComponentInChildren<SkeletonAnimation>();

        playerStats.OnTakeDamageCallback += PlayerDamagePopup;
        playerStats.OnTakeDamageCallback += StationUpdate;
        playerStats.OnHealCallback += StationUpdate;
        playerStats.OnHealCallback += PlayerHealPopup;
        playerAnim.AnimationState.Event += OnPlayerAttackAnimFinish;

        foreach (Player_Ability_Slot slot in playerGo.GetComponents<Player_Ability_Slot>())
        {
            slot.FindBattle(this);
        }

        StationUpdate(0);
        StartCoroutine(DelayedSpawn());

        playerDelay = playerStats.attackDelay.GetValue();
    }

    private void Update()
    {
        TurnTimer();
    }

    void TurnTimer()
    {
        if (enemyGo != null && enemyStats.currentHealth > 0 && playerStats.currentHealth > 0)
        {
            if (playerDelay <= 0)
            {
                playerAnim.AnimationState.SetAnimation(0, "Attack2", false);

                playerDelay = playerStats.attackDelay.GetValue();
            }
            else if (enemyDelay <= 0)
            {
                enemyAnim.AnimationState.SetAnimation( 0, "Attack", false);

                enemyDelay = enemyStats.attackDelay.GetValue();
            }
            else
            {
                playerDelay -= Time.deltaTime;

                enemyDelay -= Time.deltaTime;
            }

            playerStation.UpdateDelay(playerStats.attackDelay.GetValue() - playerDelay);

            if (enemyStats.currentHealth <= 0)
            {
                enemyStation.gameObject.SetActive(false);
            }
            else
            {
                enemyStation.UpdateDelay(enemyStats.attackDelay.GetValue() - enemyDelay);
            }
        }

        if (enemyGo)
        {
            if (enemyStats.currentHealth <= 0)
            {
                ResetDelay();
            }
        }
    }

    void ResetDelay()
    {
        playerDelay = playerStats.attackDelay.GetValue();
        playerStation.UpdateDelay(0);

        if (enemyGo)
        {
            enemyDelay = enemyStats.attackDelay.GetValue();
            enemyStation.UpdateDelay(0);
        }

    }

    void OnPlayerAttackAnimFinish(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "OnDamaging")
        {
            playerCombat.Attack(enemyStats);
            playerAnim.AnimationState.AddAnimation(0, "Idle", true, 0);
        }  
    }

    void OnEnemyAttackAnimFinish(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "OnDamaging")
        {
            enemyCombat.Attack(playerStats);
            enemyAnim.AnimationState.AddAnimation(0, "Idle", true, 0);
        }
    }

    #region POPUPS

    public void PlayerDamagePopup(int damage, bool isCritical)
    {
        Vector2 sliderPos = playerStation.stand.position;
        Damage_Popup.Create(sliderPos, damage, isCritical, false);
    }

    public void PlayerHealPopup(int damage)
    {
        Vector2 sliderPos = playerStation.stand.position;
        Damage_Popup.Create(sliderPos, damage, false, true);
    }

    public void EnemyDamagePopup(int damage, bool isCritical)
    {
        Vector2 sliderPos = enemyStation.stand.position;
        Damage_Popup.Create(sliderPos, damage, isCritical, false);
    }
    public void EnemyHealPopup(int damage)
    {
        Vector2 sliderPos = enemyStation.stand.position;
        Damage_Popup.Create(sliderPos, damage, false, true);
    }

    #endregion

    public IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(.5f);
        SpawnEnemy();
        enemyDelay = enemyStats.attackDelay.GetValue();
    }

    public void SpawnEnemy()
    {
        DespawnEnemy();
        UpdateEnemy(enemySpawner.spawnEnemy());
        ResetDelay();

        bossProgress.Subscribe();
        StationUpdate(0);
    }

    public void SpawnBoss()
    {
        bossProgress.ResetProgress();
        DespawnEnemy();
        UpdateEnemy(Instantiate(dungeonBoss, new Vector2(0, 0), Quaternion.identity));
        ResetDelay();

        StationUpdate(0);
    }

    public void DespawnEnemy()
    {
        if (enemyGo != null)
        {
            enemyStats.OnTakeDamageCallback -= EnemyDamagePopup;
            enemyStats.OnTakeDamageCallback -= StationUpdate;
            enemyStats.OnHealCallback -= StationUpdate;
            enemyStats.OnHealCallback -= EnemyHealPopup;
            enemyAnim.AnimationState.Event -= OnEnemyAttackAnimFinish;

            Destroy(enemyGo);
        }
    }

    public void UpdateEnemy(GameObject enemy)
    {
        enemyGo = enemy;
        enemyStats = enemyGo.GetComponent<Enemy_Stats>();
        enemyCombat = enemyGo.GetComponent<Character_Combat>();
        enemyAnim = enemyGo.GetComponent<SkeletonAnimation>();

        enemyStats.OnTakeDamageCallback += EnemyDamagePopup;
        enemyStats.OnTakeDamageCallback += StationUpdate;
        enemyStats.OnHealCallback += StationUpdate;
        enemyStats.OnHealCallback += EnemyHealPopup;
        enemyAnim.AnimationState.Event += OnEnemyAttackAnimFinish;
    }

    public void StationUpdate(int x, bool y)
    {
        playerStation.SetUI(playerGo);

        if (enemyGo)
        {
            enemyStation.SetUI(enemyGo);
        }
    }

    public void StationUpdate(int x)
    {
        playerStation.SetUI(playerGo);

        if (enemyGo)
        {
            enemyStation.SetUI(enemyGo);
        }
    }

    private void OnDestroy()
    {
        playerStats.OnTakeDamageCallback -= PlayerDamagePopup;
        playerStats.OnTakeDamageCallback -= StationUpdate;
        playerStats.OnHealCallback -= StationUpdate;
        playerAnim.AnimationState.Event -= OnPlayerAttackAnimFinish;
        playerStats.OnHealCallback -= PlayerHealPopup;

        foreach (Player_Ability_Slot slot in playerGo.GetComponents<Player_Ability_Slot>())
        {
            slot.FindBattle(null);
        }
    }
}
