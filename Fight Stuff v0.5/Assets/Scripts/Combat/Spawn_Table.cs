using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public GameObject enemySpawn;
    public int spawnChance;
}

[CreateAssetMenu(fileName = "New Spawn Table", menuName = "NPCs/Spawn Table")]
public class Spawn_Table : ScriptableObject
{
    public int maxRoll;

    public EnemySpawn[] enemies;


    public GameObject spawnEnemy()
    {
        int cumProb = 0;
        int roll = Random.Range(0, maxRoll);

        for (int i = 0; i < enemies.Length; i++)
        {
            cumProb += enemies[i].spawnChance;
            if (roll <= cumProb)
            {
                GameObject newEnemy = Instantiate(enemies[i].enemySpawn, new Vector2(0, 0), Quaternion.identity);
                return newEnemy;
            }
        }
        return null;
    }
}
