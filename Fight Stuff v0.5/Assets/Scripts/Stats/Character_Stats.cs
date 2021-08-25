using UnityEngine;

public class Character_Stats : MonoBehaviour
{

    public delegate void OnTakeDamage(int value);
    public OnTakeDamage OnTakeDamageCallback;

    public delegate void OnHeal(int value);
    public OnHeal OnHealCallback;

    public int currentHealth { get; private set; }

    public Stat maxHealth;
    public Stat minDamage;
    public Stat maxDamage;
    public Stat armor;
    public Stat attackDelay;
    public int xp;

    private void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());

        if(OnTakeDamageCallback != null)
        {
            OnTakeDamageCallback.Invoke(damage);
        }
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        heal = Mathf.Clamp(heal, 0, maxHealth.GetValue() - currentHealth);
        currentHealth += heal;

        if (OnHealCallback != null)
        {
            OnHealCallback.Invoke(heal);
        }
    }

    public virtual void Die()
    {
        //Override
    }
}
