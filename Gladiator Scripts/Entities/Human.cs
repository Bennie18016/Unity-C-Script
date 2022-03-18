using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float health;
    public int strength;
    public float speed;
    GameObject roundstats;
    RoundManager rm;
    GameObject player;
    GameObject em;
    Human hu;
    Player pl;
    Player em_pl;
    Animator animator;
    bool inside;
    bool akcool;
    public bool paused;

    private void Start()
    {
        health = 150f;
        strength = 49;
        speed = 4f;
        roundstats = GameObject.Find("RoundStats");
        rm = roundstats.GetComponent<RoundManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        em = GameObject.Find("EntityManager");
        hu = em.GetComponent<Human>();
        pl = player.GetComponent<Player>();
        em_pl = em.GetComponent<Player>();
        animator = GetComponent<Animator>();

        health = hu.health;

    }

    private void Update()
    {
        if(health <= 0)
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
        if(other.tag == "Player")
        {
            animator.Play("Attack");
            StartCoroutine(attk());
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inside = false;
        }
    }

    IEnumerator attk()
    {
        yield return new WaitForSeconds(2);
        if(inside == true)
        {
            if(akcool == false)
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