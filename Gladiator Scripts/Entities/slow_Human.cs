using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow_Human : Human
{

    Human hu;
    Ent_Behaviour enemy;
    Animator animator;
    Player pl;
    Player em_pl;
    RoundManager rm;
    bool inside;
    bool akcool;
    private void Start()
    {
        hu = GameObject.Find("EntityManager").GetComponent<Human>();

        health = hu.health;
        strength = 49;
        speed = 2f;

        enemy = gameObject.GetComponent<Ent_Behaviour>();
        animator = gameObject.GetComponent<Animator>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        em_pl = GameObject.Find("EntityManager").GetComponent<Player>();
        rm = GameObject.Find("RoundStats").GetComponent<RoundManager>();

        enemy.Enemy.speed = speed;
    }

    private void Update()
    {
        if (health <= 0)
        {
            em_pl.points += 300;
            em_pl.totscore += 300;
            em_pl.kills++;

            Destroy(gameObject);
            rm.EnemyCount--;

            pl.health += 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.Play("Attack");
            StartCoroutine(attk());
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inside = false;
        }
    }

    IEnumerator attk()
    {
        yield return new WaitForSeconds(2);
        if (inside == true)
        {
            if (akcool == false)
            {
                pl.health -= strength;
                akcool = true;
                StartCoroutine(attkcool());
            }

        }
    }

    IEnumerator attkcool()
    {
        yield return new WaitForSeconds(3);
        akcool = false;
    }
}

