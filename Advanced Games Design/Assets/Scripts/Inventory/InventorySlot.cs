using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeItem;
    Items item;

    public void AddItem(Items newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeItem.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeItem.interactable = false;
    }

    public void RemoveItemButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
