using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComp : MonoBehaviour
{
    public delegate void OnEntityHit(int DamageToTake);
    public OnEntityHit OnHit;

    // 1 "Health" is half a heart? Or a quarter heart?
    // Not a full heart. Would fill the screen with hearts
    [SerializeField]
    private int MaxHealth;
    [SerializeField]
    private int CurrentHealth;

    private void Awake()
    {
        OnHit = TakeDamage;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void SetMaxHealth(int InMaxHealth)
    {
        MaxHealth = InMaxHealth;
    }

    public void TakeDamage(int DamageToTake)
    {
        CurrentHealth -= DamageToTake;

        if (CurrentHealth <= 0)
            Destroy(transform.gameObject);
    }

    public void GainHealth(int HealthToGain)
    {
        CurrentHealth += HealthToGain;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    public int GetMaxHealth() { return MaxHealth; }
    public int GetCurrentHealth() { return CurrentHealth; }
}
