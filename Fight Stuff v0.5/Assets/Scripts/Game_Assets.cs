using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Assets : MonoBehaviour
{
    private static Game_Assets i;

    public static Game_Assets instance
    {
        get
        {
            if (i == null) i = (Instantiate(Resources.Load("Game Assets")) as
                    GameObject).GetComponent<Game_Assets>();
            return i;
        }
    }

    public Transform damagePrefab;

}
