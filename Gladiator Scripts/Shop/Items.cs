using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    //Creates an enumerator so that I can set the names of items for sale
    public enum Item
    {
        Mace,
        Sword,
        HealthPotion
    }

    //Gets the cost of each item using switch. Returns an int
    public static int GetCost(Item item)
    {
        switch (item)
        {
            default:
            case Item.Mace: return 10000;
            case Item.Sword: return 25000;
            case Item.HealthPotion: return 5000;    
        }
    }
}


