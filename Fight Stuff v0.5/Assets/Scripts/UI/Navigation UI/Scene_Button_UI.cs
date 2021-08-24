using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Scene_Button_UI : MonoBehaviour
{
    public int sceneIndex;
    public string chapterName;
    public TextMeshProUGUI text;
    public Button button;

    GameObject gameManager;
    Character_Stats playerStats;

    private void Update()
    {
        if (playerStats.currentHealth <= 0)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        playerStats = Player_Manager.instance.player.GetComponent<Character_Stats>();
        text.SetText(chapterName);
    }

    public void ChangeScene()
    {
        if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
        {
            gameManager.GetComponent<Scene_Swapper>().LoadScene(sceneIndex);
        }
    }
}
