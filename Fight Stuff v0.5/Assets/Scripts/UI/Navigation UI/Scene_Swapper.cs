using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Swapper : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField] GameObject abilityPanel;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        currentScene = SceneManager.GetSceneByBuildIndex(1);
    }

    private void FixedUpdate()
    {
        MakeActive();
    }

    public void LoadScene(int sceneIndex)
    {

        if(!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
        {
            SceneManager.UnloadSceneAsync(currentScene);
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
            currentScene = SceneManager.GetSceneByBuildIndex(sceneIndex);

            if (sceneIndex != 1)
            {
                Player_Manager.instance.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
                abilityPanel.SetActive(true);
            }
            else
            {
                Player_Manager.instance.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                abilityPanel.SetActive(false);
            }
        }
    }

    void MakeActive()
    {
        if (SceneManager.GetActiveScene() != currentScene)
        {
            SceneManager.SetActiveScene(currentScene);
        }
    }
}
