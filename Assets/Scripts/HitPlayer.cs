using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    private bool damage;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DamageEffect(collision.gameObject));
        }
    }

    IEnumerator DamageEffect(GameObject player)
    {
        if (player.GetComponent<Player>().Health <= 0)
        {
            player.GetComponent<PlayerController>().isDead = true;
            player.GetComponent<PlayerController>().anim.SetBool("Dead", true);
        }
        else
        {
            yield return new WaitForSeconds(1);
            player.GetComponent<PlayerController>().anim.SetBool("Hit", true);
            player.GetComponent<Player>().Health -= 20;
            yield return new WaitForSeconds(1);
            player.GetComponent<PlayerController>().anim.SetBool("Hit", false);
            if (player.GetComponent<Player>().Health <= 0)
            {
                player.GetComponent<PlayerController>().isDead = true;
                player.GetComponent<PlayerController>().anim.SetBool("Dead", true);
            }   
        }
    }
}
