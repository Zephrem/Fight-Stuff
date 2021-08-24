using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage_Popup : MonoBehaviour
{

    public static Damage_Popup Create(Vector2 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(Game_Assets.instance.damagePrefab, position, Quaternion.identity);

        Damage_Popup damagePopup = damagePopupTransform.GetComponent<Damage_Popup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTimer = .5f;
    }

    private void Update()
    {
        float moveYSpeed = 3f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;

            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
