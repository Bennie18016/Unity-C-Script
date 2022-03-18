using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class Ent_Behaviour : MonoBehaviour
{
    GameObject Player;
    public NavMeshAgent Enemy;
    GameObject entitymanager;
    Human hu;

    private void Awake()
    {
        //Gets the script "Human"
        entitymanager = GameObject.Find("EntityManager");
        hu = entitymanager.GetComponent<Human>();

        //Gets the navmeshagent and also finds the player
        Enemy = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        //Sets how fast they can move depending on the human script, also how close they can get to eachother and the player
        Enemy.stoppingDistance = 2.8f;
        Enemy.speed = hu.speed;
    }
    private void Update()
    {
        //Always moving for the player, unless paused
        Enemy.SetDestination(Player.transform.position);

        //Sets their destination to their curren tone if they are paused
        if(hu.paused == true)
        {
            Enemy.SetDestination(gameObject.transform.position);
        }
    }



    

}
