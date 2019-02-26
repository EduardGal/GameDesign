using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform inventoryParent;
    public GameObject inventoryUI;
    Inventory inventory;

    InventorySlot[] slots;
	
	void Start ()
    {
        inventory = Inventory.instance;
        inventory.inventoryUpdated += UpdateUI;

        slots = inventoryParent.GetComponentsInChildren<InventorySlot>();
	}
	
	
	void Update ()
    {
        if (Input.GetButtonDown("Inventory"))
        {
			FindObjectOfType<AudioManager>().Play("Tab");
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if(inventoryUI.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	}

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++) //Loop through all of the Inventory slots
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
