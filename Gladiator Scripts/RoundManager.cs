using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    //Setting the variables up for what I need
    GameObject entitymanager;
    Human hu;
    public int Round;
    public int EnemyCount;
    public int[] PreRound12 = { 6, 8, 13, 18, 24, 27, 28, 28, 29, 33, 34 };
    int ran;
    int ran2;
    public Vector3[] Spawn_Locations = new[] { new Vector3(25, 1.540848f, 5), new Vector3(0, 1.540848f, 5), new Vector3(18.28f, 1.540848f, 11.23f), new Vector3(5.83f, 1.540848f, 18.58f) };

    //When the script has loaded, runs the function "newRoundEnemy"
    private void Start()
    {
        Round = 0;
        newRoundIncrease(ref Round);
    }

    //Checks to see if there are 0 enemies left, if there is no, does function "newRoundEnemy"
    private void Update()
    {
        if(EnemyCount == 0)
        {
            newRoundIncrease(ref Round);
        }
    }

    //Increases the round number by 1
    void newRoundIncrease(ref int round)
    {
        round++;
        newRoundEnemy();
    }

    //Private function that will return an int for how many enemies there are
    int newRoundEnemy()
    {

        //Checks to see if round is less than 12 because formula doesnt work pre round 12
        if (Round < 12)
        {
            EnemyCount = PreRound12[Round - 1];
        }
        //If its after round 12, enemies = formula result
        else
        {
            EnemyCount = Mathf.RoundToInt(0.0842f * (Mathf.Pow(Round, 2)) + 0.1954f * Round + 22.05f);
        }

        //Does function "newRoundHealth" and "newRoundSpawn"
        newRoundHealth();
        newRoundSpawn();

        //Returns the int to EnemyCount
        return EnemyCount;
    }

    //private function that spawns the enemies
    void newRoundSpawn()
    {
        //Gets the prefab
        var prefabHuman = Resources.Load("Models/Exported Enemy (fixed)");
        //Do this is i < enemy count from "newRoundEnemy"
        for (int i = 0; i < EnemyCount; i++)
        {
            //Picks a random Vector3 from Spawn_Locations
            ran = Random.Range(0, Spawn_Locations.Length);
            //Spawn location for each enemy is picked from random result
            Vector3 zomSpawn = Spawn_Locations[ran];
            //Instaniates the enemy at the random spawn
            GameObject HumanObj = (GameObject)Instantiate(prefabHuman, zomSpawn, Quaternion.identity);

            //10% chance to get 0
            ran2 = Random.Range(0, 10);

            if(ran2 == 0)
            {
                //Adds slow human script
                HumanObj.AddComponent<slow_Human>();
            } else
            {
                //Adds the human script to the enemies
                HumanObj.AddComponent<Human>();
            }

        }
    }

    //function that increases enemy health
    void newRoundHealth()
    {
        //gets the script from EntityManager
        entitymanager = GameObject.Find("EntityManager");
        hu = entitymanager.GetComponent<Human>();

        //Increases their health by 100
        hu.health += 100;

        //If the round is equal or greater than 10, alos multiples by 1.1
        if(Round >= 10)
        {
            hu.health *= 1.1f;
        }
    }
}
