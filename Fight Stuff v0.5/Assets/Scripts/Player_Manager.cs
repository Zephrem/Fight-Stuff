using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class Player_Manager : MonoBehaviour
{
    #region Singleton
    public static Player_Manager instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject player;
    private Player_Stats myStats;
    private SkeletonAnimation myAnim;

    private void Start()
    {
        myStats = GetComponent<Player_Stats>();
        myAnim = GetComponentInChildren<SkeletonAnimation>();
    }

    public void KillPlayer()
    {
        myAnim.AnimationState.SetAnimation(0, "Death", false);
        myAnim.AnimationState.Complete += OnDeadAnimComplete;
    }

    void OnDeadAnimComplete(TrackEntry trackEntry)
    {
        myAnim.AnimationState.Complete -= OnDeadAnimComplete;

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        GameObject.Find("Game Manager").GetComponent<Scene_Swapper>().LoadScene(1);
        yield return new WaitWhile(() => SceneManager.GetSceneByBuildIndex(1).isLoaded == false);
        myStats.Heal(myStats.maxHealth.GetValue());
        myAnim.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
}
