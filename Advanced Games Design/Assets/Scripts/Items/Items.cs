using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Items : ScriptableObject {

    [SerializeField] string itemName = "New Item";
    [SerializeField] string itemType = "Item type";

    public Sprite itemIcon = null;
    public AnimationClip animation;
    public AudioClip animationSound;
    public bool isDefault = false;

    public GameObject EquippedItem { get; set; }
    private Items previousItem;

    public virtual void Use()
    {
        if(itemType == "Item")
        {
            EquipItem();
        }
    }

    public void EquipItem()
    {
        if(Player.instance.itemInHand == null)
        {
            EquippedItem = Instantiate(Resources.Load<GameObject>("Items/" + itemName), Player.instance.playersHand.transform);
        }
        else
        {
            previousItem = Player.instance.itemInHand.GetComponent<ItemPickup>().item;
            OldItem();

            if(Player.instance.itemInHand.GetComponent<ItemPickup>().item.itemName != "Unarmed")
            {
                Destroy(Player.instance.itemInHand);
            }
            
            EquippedItem = Instantiate(Resources.Load<GameObject>("Items/" + itemName), Player.instance.playersHand.transform);
        }

        RemoveFromInventory();
        Player.instance.itemInHand = EquippedItem;
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }

    public void OldItem()
    {
        Inventory.instance.AddItem(previousItem);    
    }
}
