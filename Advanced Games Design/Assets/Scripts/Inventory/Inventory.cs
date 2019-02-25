using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public List<Items> items = new List<Items>();
    public int inventorySpace = 20;

    public delegate void InventoryUpdated(); //multiple methods can be called when the delegate is triggered
    public InventoryUpdated inventoryUpdated;

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Multiple inventories found");
        }
        instance = this;
    }

    public bool AddItem(Items item)
    {
        if(!item.isDefault)
        {
            if(items.Count >= inventorySpace) //ensures no more than 20 items can be added to the inventory
            {
                Debug.Log("Not enough inventory space"); //if the item cannot be added to the inventory
                return false; //returning false does not pick up the object or destroy it from the scene
            }

            items.Add(item); //otherwise the item gets added to an inventory slot

            if(inventoryUpdated !=null)
            {
                inventoryUpdated.Invoke();
            }
        }

        return true; //object is picked up and destroyed from the scene
    }

    public void RemoveItem(Items item)
    {
        items.Remove(item);
        if (inventoryUpdated != null)
        {
            inventoryUpdated.Invoke();
        }
    }
}
