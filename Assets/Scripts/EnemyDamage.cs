using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private bool damage;
    Enemy enemy;

    private void Start()
    {
        damage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (damage)
            {
                enemy = other.GetComponent<Enemy>();
                StartCoroutine(DamageEffect());
            }
        }
    }

    IEnumerator DamageEffect()
    {
        Debug.Log("Hit");
        damage = false;
        enemy.enemyhealth -= 10;
        yield return new WaitForSeconds(1);
        damage = true;
    }
}
