using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> myCollectedItems = new();
    
    private void Start()
    {
        var items = FindObjectsOfType<Item>();
        foreach (var item in items)
        {
            item.onItemPickup += HandleItemPickedUp;
        }
    }

    private void HandleItemPickedUp(Item item)
    {
        myCollectedItems.Add(item);
    }
}
