using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_GUI : MonoBehaviour
{
    Text RoundText;
    Text CountText;
    Text PointText;
    Text PotionText;
    GameObject roundstats;
    GameObject entitymanager;
    RoundManager rm;
    Player en;

    private void Start()
    {
        //Transforming and Spawning the text
        roundstats = GameObject.Find("RoundStats");
        rm = roundstats.GetComponent<RoundManager>();
        entitymanager = GameObject.Find("EntityManager");
        en = entitymanager.GetComponent<Player>();


        var prefabText = Resources.Load("UI/Text");
        var prefabImage = Resources.Load("UI/Image");

        GameObject RoundObj = (GameObject)Instantiate(prefabText, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject CountObj = (GameObject)Instantiate(prefabText, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject PointObj = (GameObject)Instantiate(prefabText, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject PotionOBJ = (GameObject)Instantiate(prefabText, new Vector3(0,0,0), Quaternion.identity);

        RoundText = RoundObj.GetComponent<Text>();
        CountText = CountObj.GetComponent<Text>();
        PointText = PointObj.GetComponent<Text>();
        PotionText = PotionOBJ.GetComponent<Text>();

        RoundText.transform.SetParent(gameObject.GetComponent<RectTransform>());
        CountText.transform.SetParent(gameObject.GetComponent<RectTransform>());
        PointText.transform.SetParent(gameObject.GetComponent<RectTransform>());
        PotionText.transform.SetParent(gameObject.GetComponent<RectTransform>());

        RectTransform Count_Trans = CountObj.GetComponent<RectTransform>();
        RectTransform Round_Trans = RoundObj.GetComponent<RectTransform>();
        RectTransform Point_Trans = PointObj.GetComponent<RectTransform>();
        RectTransform Potion_Trans = PotionOBJ.GetComponent<RectTransform>();

        Count_Trans.anchoredPosition = new Vector3(-440, 240, 0);
        Round_Trans.anchoredPosition = new Vector3(-485, -250, 0);
        Point_Trans.anchoredPosition = new Vector3(360, -250, 0);
        Potion_Trans.anchoredPosition = new Vector3(480, -215, 0);

        Potion_Trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
        Count_Trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250f);

    }

    private void Update()
    {
        //Keeps each text updated to what we need
        CountText.text = "Enemies Left: " + rm.EnemyCount;
        RoundText.text = "Round: " + rm.Round;
        //ToString turns a number value to a string so that it can be written
        PointText.text = en.points.ToString();
        PotionText.text = "Health Potions: " + en.hpotions;
    }
}
