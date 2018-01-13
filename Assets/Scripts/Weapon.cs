using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Player player;

    [SerializeField]
    private float AttacksPerSecond = 1.0f;
    private float AttackCounter = 0.0f;

    private bool bCanAttack = false;

	// Use this for initialization
	void Start ()
    {
        // Bad. Should assign some other way. Public?
        player = transform.parent.parent.GetComponent<Player>();

        bCanAttack = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        AttackCounter += Time.deltaTime;

        if(AttackCounter >= (1/AttacksPerSecond))
        {
            //Debug.Log("Setting CanAttack to TRUE");
            bCanAttack = true;
            AttackCounter -= (1/AttacksPerSecond);
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            HealthComp EnemyHealthComponent = collision.gameObject.GetComponent<HealthComp>();
            if(EnemyHealthComponent && bCanAttack)
            {
                bCanAttack = false;
                EnemyHealthComponent.OnHit(1);
                Debug.Log("End Health Enemy: " + EnemyHealthComponent.GetCurrentHealth());
            }
        }
    }
}
