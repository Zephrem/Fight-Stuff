using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Station_UI : Station_UI
{
    public Slider xp;
    public TextMeshProUGUI levelText;

    public override void SetUI(GameObject character)
    {
        base.SetUI(character);
        xp.maxValue = character.GetComponent<Player_Stats>().xpToLevel;
        xp.value = character.GetComponent<Player_Stats>().xp;
        levelText.text = character.GetComponent<Player_Stats>().level.ToString();
    }
}
