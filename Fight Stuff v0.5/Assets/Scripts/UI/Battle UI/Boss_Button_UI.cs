using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss_Button_UI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Button button;

    private Boss_Progress bossProgress;

    private void Start()
    {
        bossProgress = gameObject.GetComponent<Boss_Progress>();

        slider.maxValue = bossProgress.maxProgress;
    }

    public void UpdateUI()
    {
        slider.value = bossProgress.currentProgress;

        if (slider.value >= slider.maxValue)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

}
