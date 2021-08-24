using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drop
{
    public Item itemDrop;
    public int lootChance;
}

[CreateAssetMenu(fileName = "New Table", menuName = "Inventory/Drop Table")]
public class Drop_Table: ScriptableObject
{
    public int maxRoll;

    public Drop[] drops;
    

    public Item rollItem()
    {
        int cumProb = 0;
        int roll = Random.Range(0, maxRoll);

        for(int i = 0; i < drops.Length; i++)
        {
            cumProb += drops[i].lootChance;
            if(roll <= cumProb)
            {
                return Instantiate(drops[i].itemDrop);
            }
        }

        return null;
    }
}
