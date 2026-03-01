using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemStats itemStats;
    [HideInInspector] public bool isPickedUp = false;
    private GameObject itemInstance;
    [HideInInspector] public float itemSlotNumber; 
    public void PickItemUp(Transform holdTransform, Item item)
    {

        bool inventoryFull = true;
        InventoryUI inventoryUI = FindAnyObjectByType<InventoryUI>();



        if (inventoryUI)
        {
            int count = 0;
            foreach (var image in inventoryUI.images)
            {
                if (image.texture == inventoryUI.emptyTexture)
                {
                    itemInstance = Instantiate(item.itemStats.item, holdTransform.position, holdTransform.rotation);
                    itemInstance.transform.SetParent(holdTransform, true);
                    inventoryUI.itemsHeld[count] = itemInstance;
                    Item specificItem = itemInstance.GetComponent<Item>();
                    specificItem.itemSlotNumber = count;
                    specificItem.isPickedUp = true;
                    inventoryUI.CheckIfItemEquipped();
                    image.texture = itemStats.icon;
                    inventoryFull = false;
                    Debug.Log($"{itemStats.itemName} picked up");
                    break;
                }
                count++;
            }



        
         

            if (inventoryFull)
            {
                int slot = inventoryUI.slotEquipped;

                GameObject oldItem = inventoryUI.itemsHeld[slot];

                if (oldItem != null)
                {
                    // Drop the old item
                    oldItem.transform.SetParent(null);
                    oldItem.transform.position = holdTransform.position + holdTransform.forward;
                    oldItem.SetActive(true);
                    
                    Item oldItemComponent = oldItem.GetComponent<Item>();
                    oldItemComponent.isPickedUp = false;
                    oldItem.layer = 7;
                }

                // Spawn the new item into that slot
                itemInstance = Instantiate(item.itemStats.item, holdTransform.position, holdTransform.rotation);
                itemInstance.transform.SetParent(holdTransform, true);

                inventoryUI.itemsHeld[slot] = itemInstance;

                Item newItemComponent = itemInstance.GetComponent<Item>();
                newItemComponent.itemSlotNumber = slot;
                newItemComponent.isPickedUp = true;

                inventoryUI.images[slot].texture = itemStats.icon;

                inventoryUI.CheckIfItemEquipped();

                Destroy(item.gameObject); // remove pickup object from world

            }

          
            Destroy(item.gameObject);
      
        }
    }
}
