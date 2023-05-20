using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private bool damage;

    private void Start()
    {
        damage = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(DamageEffect(collision.gameObject));
        }
    }

    IEnumerator DamageEffect(GameObject enemy)
    {
        if (enemy.GetComponent<Enemy>().enemyhealth <= 0)
        {
            enemy.GetComponent<EnemyNavmesh>().anim.SetBool("Dead", true);
            yield return new WaitForSeconds(1);
            enemy.GetComponent<EnemyNavmesh>().isDead = true;
        }
        else
        {
            yield return new WaitForSeconds(1);
            enemy.GetComponent<EnemyNavmesh>().anim.SetBool("Hit", true);
            enemy.GetComponent<Enemy>().enemyhealth -= 20;
            damage = false;
            yield return new WaitForSeconds(1);
            enemy.GetComponent<EnemyNavmesh>().anim.SetBool("Hit", false);
            damage = true;
        }
    }
}
