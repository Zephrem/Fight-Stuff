using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ability_UI : MonoBehaviour
{
    [SerializeField] Player_Ability_Slot slot;
    Sprite mySprite;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = slot.ability.icon;
        GetComponent<Image>().sprite = mySprite;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (slot.state != Player_Ability_Slot.AbilityState.ready)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 125);
        }
        else
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}
