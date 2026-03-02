using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemStats itemStats;
    [HideInInspector]public bool isPickedUp = false; 
    private GameObject itemInstance;
    [HideInInspector] public float itemSlotNumber;
    private InventoryUI inventoryUI;
    public Vector3 itemLocation;
    public Quaternion itemRotation;
    public void PickItemUp(Transform holdTransform, Item item)
    {

        bool inventoryFull = true;
        inventoryUI = FindAnyObjectByType<InventoryUI>();



        if (inventoryUI)
        {
            int count = 0;
            foreach (var image in inventoryUI.images)
            {
                if (image.texture == inventoryUI.emptyTexture)
                {
                    EquipItemFunctionality(holdTransform, item, count);
                    image.texture = itemStats.icon;
                    inventoryFull = false;
                  
                    break;
                }
                count++;
            }

            // will drop the item the player is currently holding for the item on ground
            if (inventoryFull)
            {
                int slotNumber = inventoryUI.slotEquipped;
                DropItemFunctionality(holdTransform, slotNumber);
                // Item on ground is spawned into players hand & inventory
                EquipItemFunctionality(holdTransform, item, slotNumber);

                inventoryUI.images[slotNumber].texture = itemStats.icon;


            

            }

            Destroy(item.gameObject);
      
        }
    }


    
    private void EquipItemFunctionality(Transform holdTransform, Item item, int slotNumber)
    {
        itemInstance = Instantiate(item.itemStats.item, holdTransform.position, holdTransform.rotation);
        itemInstance.transform.SetParent(holdTransform, true);
        inventoryUI.itemsInInventory[slotNumber] = itemInstance;
        Item newItemComponent = itemInstance.GetComponent<Item>();
        newItemComponent.itemSlotNumber = slotNumber;
        newItemComponent.isPickedUp = true;
        newItemComponent.transform.localPosition = newItemComponent.itemLocation;
        newItemComponent.transform.localRotation = newItemComponent.itemRotation;
        inventoryUI.CheckIfItemEquipped();
        
    }

    public void DropItemFunctionality(Transform holdTransform, int slotNumber)
    {
        

        GameObject oldItem = inventoryUI.itemsInInventory[slotNumber]; // references the item player is currently holding

        if (oldItem != null)
        {
            // drops item player is holding and removes it as child of player
            oldItem.transform.SetParent(null);
            oldItem.transform.position = holdTransform.position + holdTransform.forward;
           


            Item oldItemComponent = oldItem.GetComponent<Item>();
            oldItemComponent.isPickedUp = false;
            oldItem.layer = 7;
            Rigidbody rb = oldItem.AddComponent<Rigidbody>();
            rb.useGravity = true;
        }

    }
}
