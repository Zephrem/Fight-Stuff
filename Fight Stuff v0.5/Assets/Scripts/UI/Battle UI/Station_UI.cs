using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Station_UI : MonoBehaviour
{
    public Slider hp;
    public Slider delay;
    public Transform stand;

    public virtual void SetUI(GameObject character)
    {
        character.transform.position = stand.position;
        gameObject.SetActive(true);
        hp.maxValue = character.GetComponent<Character_Stats>().maxHealth.GetValue();
        hp.value = character.GetComponent<Character_Stats>().currentHealth;
        delay.maxValue = character.GetComponent<Character_Stats>().attackDelay.GetValue();
    }

    public void UpdateDelay(float currentDelay)
    {
        delay.value = currentDelay;
    }
}
