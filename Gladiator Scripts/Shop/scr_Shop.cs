using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Shop : MonoBehaviour
{
    GameObject em;
    Player pl;
    Human hu;

    Text ShopText;
    GameObject canvas;
    GameObject shopOBJ;

    Transform c;
    GameObject HB;

    GameObject shopO;


    private void Awake()
    {
        //Transforming, Spawning and hidding stuff
        em = GameObject.Find("EntityManager");
        pl = em.GetComponent<Player>();
        hu = em.GetComponent<Human>();
        canvas = GameObject.Find("Canvas");
        c = canvas.GetComponent<RectTransform>();
        HB = GameObject.Find("HealthBar");

        var prefabText = Resources.Load("UI/Text");
        var prefabTitle = Resources.Load("UI/Shop/Title");
        var prefabButton = Resources.Load("UI/Button");

        shopOBJ = (GameObject)Instantiate(prefabText, new Vector3(0, 0, 0), Quaternion.identity);
        ShopText = shopOBJ.GetComponent<Text>();
        ShopText.transform.SetParent(c);
        RectTransform Shop_Trans = shopOBJ.GetComponent<RectTransform>();
        Shop_Trans.anchoredPosition = new Vector3(50, -200, 0);
        Shop_Trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);

        ShopText.text = "Press E to enter shop";
        shopOBJ.SetActive(false);

        shopO = new GameObject("Shop");
        shopO.transform.SetParent(c);
        shopO.SetActive(false);

        GameObject titleOBJ = (GameObject)Instantiate(prefabTitle, new Vector3(0, 0, 0), Quaternion.identity);
        RectTransform Title_Trans = titleOBJ.GetComponent<RectTransform>();
        Title_Trans.anchoredPosition = new Vector3(600, 490, 0);
        titleOBJ.transform.SetParent(shopO.transform);

        GameObject buttonOBJ = (GameObject)Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        buttonOBJ.transform.SetParent(shopO.transform);
        RectTransform Button_trans = buttonOBJ.GetComponent<RectTransform>();
        Button_trans.anchoredPosition = new Vector3(1085, 490, 0);
        Button_trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 90);
        Button_trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80);
        Button closbut = buttonOBJ.GetComponent<Button>();
        closbut.onClick.AddListener(CloseMenu);
        Text but_txt = buttonOBJ.GetComponentInChildren<Text>();
        but_txt.text = "Close";

        //Creates the items using the enumerator before. Easy to follow
        CreateShop(Items.Item.Mace, "Mace", Items.GetCost(Items.Item.Mace), 0);
        CreateShop(Items.Item.HealthPotion, "Health\nPotion", Items.GetCost(Items.Item.HealthPotion), 1);
        CreateShop(Items.Item.Sword, "Sword", Items.GetCost(Items.Item.Sword), 2);
    }
    private void Start()
    {
    }

    //If you are close to the box it will show "Press e to open shop"
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            shopOBJ.SetActive(true);

            //If e Is pressed opens the shop
            if (Input.GetKeyDown(KeyCode.E))
            {
                hu.paused = true;
                pl.canMove = false;
                HB.SetActive(false);
                shopO.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }

    //if you leave the trigger, hides the text to open shop
    private void OnTriggerExit(Collider other)
    {
        shopOBJ.SetActive(false);
    }

    //Creates the ui from the shop and hides it. Uses the variables you use from CreateShop
    private void CreateShop(Items.Item item, string itemname, int itemcost, int id)
    {
        var prefabItem = Resources.Load("UI/Shop/Item");
        var prefabButton = Resources.Load("UI/Button");

        GameObject shop = (GameObject)Instantiate(prefabItem, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject but = (GameObject)Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        Image shopBack = shop.GetComponent<Image>();
        Button button = but.GetComponent<Button>();
        RectTransform shop_trans = shopBack.GetComponent<RectTransform>();
        RectTransform but_trans = but.GetComponent<RectTransform>();
        shopBack.transform.SetParent(c);
        button.transform.SetParent(c);

        Text [] itemStats = shop.GetComponentsInChildren<Text>();
        itemStats[0].text = itemname;
        itemStats[1].text = itemcost.ToString();

        Text but_txt = but.GetComponentInChildren<Text>();
        but_txt.text = "Buy";

        float yval = id * 100;
        shop_trans.anchoredPosition = new Vector3(-400, (100 - yval), 0);
        but_trans.anchoredPosition = new Vector3(-400, (100 - yval), 0);

        shopBack.transform.SetParent(shopO.transform);
        button.transform.SetParent(shopO.transform);

        but.name = itemname;

        //Adds a unique listener depending on the item by simply having a fucntion that needs an Items.Item value
        button.onClick.AddListener(() => TryBuyItem(item));
    }

    //Checks if you have enough points. If you do deduct points and run a function depending what you brought
    private void TryBuyItem(Items.Item item)
    {
        if (pl.points >= Items.GetCost(item))
        {
            pl.points -= Items.GetCost(item);
            BoughtItem(item);
        }
    }

    //Simply runs a function depeding on which item you picked
    public void BoughtItem(Items.Item item)
    {
        switch (item)
        {
            case Items.Item.Mace: BuyMace(); break;
            case Items.Item.HealthPotion: BuyHealthPotion(); break;
            case Items.Item.Sword: BuySword(); break;
        }
    }

    //Closes the shop and resumes everything and hiding the cursor and locking it again
    public void CloseMenu()
    {
        shopO.SetActive(false);
        pl.canMove = true;
        hu.paused = false;
        HB.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Functions depending on what you have brought
    void BuyMace()
    {
        pl.modifier = 850;
    }


    void BuyHealthPotion()
    {
        pl.hpotions++;
    }

    void BuySword()
    {
        pl.modifier = 1350;
    }

}