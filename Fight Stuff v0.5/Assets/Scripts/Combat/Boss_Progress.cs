using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Progress : MonoBehaviour
{
    public int currentProgress { get; private set; }
    public int maxProgress;

    // Start is called before the first frame update
    void Start()
    {
        currentProgress = 0;
    }

    void AddProgress()
    {
        UnSubscribe();
        Mathf.Clamp(currentProgress += 1, 0, maxProgress);
        gameObject.GetComponent<Boss_Button_UI>().UpdateUI();
    }

    public void ResetProgress()
    {
        currentProgress = 0;
        gameObject.GetComponent<Boss_Button_UI>().UpdateUI();
    }

    public void Subscribe()
    {
        GameObject.Find("Battle Manager").GetComponent<Battle_Manager>()
            .enemyStats.OnEnemyDeathCallback += AddProgress;
    }
    public void UnSubscribe()
    {
        GameObject.Find("Battle Manager").GetComponent<Battle_Manager>()
            .enemyStats.OnEnemyDeathCallback -= AddProgress;
    }
}
