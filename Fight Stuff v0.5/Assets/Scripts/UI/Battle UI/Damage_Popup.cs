using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage_Popup : MonoBehaviour
{

    public static Damage_Popup Create(Vector2 position, int damageAmount, bool isCritical, bool isHeal)
    {
        Transform damagePopupTransform = Instantiate(Game_Assets.instance.damagePrefab, position, Quaternion.identity);

        Damage_Popup damagePopup = damagePopupTransform.GetComponent<Damage_Popup>();

        damagePopup.Setup(damageAmount, isCritical, isHeal);

        return damagePopup;
    }

    private TextMeshPro textMesh;
    private float disappearTimer;
    private int direction;
    private Color textColor;

    private float moveYSpeed = 3f;
    private float moveXSpeed = 2f;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int value, bool isCritical, bool isHeal)
    {
        textMesh.SetText(value.ToString());

        if (!isHeal)
        {
            if (isCritical)
            {
                textColor = Color.red;
            }
            else
            {
                textColor = Color.white;
            }
        }
        else
        {
            textColor = Color.green;
        }

        textMesh.color = textColor;
        disappearTimer = .5f;
        direction = Random.Range(0, 2);
    }

    private void Update()
    {
        if (direction == 1)
        {
            moveXSpeed = -2f;
        }

        transform.position += new Vector3(moveXSpeed, moveYSpeed) * Time.deltaTime;

        moveYSpeed -= 0.05f;

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
