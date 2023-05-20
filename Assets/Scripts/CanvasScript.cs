using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject attacktext;
    public GameObject one;
    public GameObject two;
    public GameObject talk;
    public GameObject argue;
    public GameObject talkone;
    public GameObject talktwo;
    public GameObject talkthree;
    public GameObject finaltalkone;
    public GameObject finaltalktwo;
    public EnemyNavmesh enemy;
    private void Start()
    {
        StartCoroutine(Talkingone());
    }

    public void Talk()
    {
        two.SetActive(false);
        second();
    }

    public void second()
    {
        StartCoroutine(Second());
    }

    public void Fight()
    {
        two.SetActive(false);
        argue.SetActive(true);
        StartCoroutine(fightone());
    }

    public void fight()
    {
        EnemyNavmesh.attack = true;
        attacktext.SetActive(true);
        PlayerController.isAttackMode = true;
    }

    IEnumerator Second()
    {
        talk.SetActive(true);
        talkone.SetActive(true);
        yield return new WaitForSeconds(2);
        talkone.SetActive(false);
        talktwo.SetActive(true);
        yield return new WaitForSeconds(2);
        talktwo.SetActive(false);
        talkthree.SetActive(true);
    }

    IEnumerator Talkingone()
    {
        yield return new WaitForSeconds(2);
        one.SetActive(false);
        two.SetActive(true);
    }

    public void FinalFight()
    {
        StartCoroutine(Final());
    }

    IEnumerator Final()
    {
        finaltalkone.SetActive(true);
        yield return new WaitForSeconds(2);
        finaltalkone.SetActive(false);
        finaltalktwo.SetActive(true);
        yield return new WaitForSeconds(2);
        talk.SetActive(false);
        attacktext.SetActive(true);
        EnemyNavmesh.attack = true;
        attacktext.SetActive(true);
        PlayerController.isAttackMode = true;
        enemy.talk = false;
    }


    IEnumerator fightone()
    {
        yield return new WaitForSeconds(2);
        argue.SetActive(false);
        EnemyNavmesh.attack = true;
        attacktext.SetActive(true);
        PlayerController.isAttackMode = true;
        enemy.talk = false;
    }
}
