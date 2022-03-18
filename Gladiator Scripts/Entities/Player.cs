using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float maxHP;
    public float health;
    public int strength;
    public int stamina;
    public int points;
    public int kills;
    public int modifier;
    public int hpotions;
    public int deaths;
    public int totscore;
    bool healing;
    public bool canMove;
    Human hu;
    Player pl;
    GameObject em;
    GameObject rm;

    private void Start()
    {
        maxHP = 100;
        health = 100f;
        strength = 150;
        stamina = 100;
        points = 0;
        kills = 0;
        modifier = 0;
        hpotions = 0;

        em = GameObject.Find("EntityManager");
        rm = GameObject.Find("RoundStats");
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        canMove = true;

    }

    private void Update()
    {

        if (health <= 0)
        {
            deaths++;
            DontDestroyOnLoad(em);
            DontDestroyOnLoad(rm);
            SceneManager.LoadScene("Dead");
        }

        if (health > maxHP)
        {
            health = maxHP;
        }

        if(health < maxHP)
        {
            if(healing == false)
            {
                if(canMove == true)
                {
                StartCoroutine(heal());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            useHeal();
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (Input.GetMouseButtonDown(0))
            {
                hu = other.GetComponent<Human>();
                hu.health -= strength + modifier;
            }
        }

    }

    IEnumerator heal()
    {
        healing = true;
        yield return new WaitForSeconds(2);
        health += 10;
        StartCoroutine(healcool());
    }

    IEnumerator healcool()
    {
        yield return new WaitForSeconds(5);
        healing = false;
    }

    void useHeal()
    {
        if (hpotions > 1)
        {
            hpotions--;
            pl.health += 50;
        }
    }
}
