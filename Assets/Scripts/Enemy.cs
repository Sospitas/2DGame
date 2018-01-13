using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    string Name;
    int Damage;

    public void SetEnemyName(string InName)
    {
        Debug.Log(InName);
        Name = InName;
    }
    public string GetEnemyName()
    {
        return Name;
    }

    public void SetEnemyDamage(int InDamage)
    {
        Damage = InDamage;
    }
    public int GetEnemyDamage()
    {
        return Damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HealthComp PlayerHealthComponent = collision.gameObject.GetComponent<HealthComp>();

            if(PlayerHealthComponent)
            {
                PlayerHealthComponent.OnHit(Damage);
            }
        }
    }
}
