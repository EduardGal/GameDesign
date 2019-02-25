using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactables
{
    public float radius;
    public GameObject pickupPanel;
    private Transform player;
    public Items item;
    private AnimationClip animationClip;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerOne").transform;
        animationClip = item.animation;

        var canvas = GameObject.Find("Canvas");
        pickupPanel = canvas.transform.Find("PickupPanel").gameObject;
    }
    
    public void PickUpItem()
    {
        bool ItemPickedUp = Inventory.instance.AddItem(item);

        //Add item to inventory, remove from scene
        if (ItemPickedUp)
        {
            Destroy(gameObject);
            pickupPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerOne")
        {
            Debug.Log("Player one has entered");
            if (pickupPanel != null && Player.instance.itemInHand.name != "Rock(Clone)")
            {
                pickupPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (pickupPanel != null)
        {
            pickupPanel.SetActive(false);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
