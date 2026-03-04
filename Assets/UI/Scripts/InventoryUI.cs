using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public RawImage[] images;
    [HideInInspector] public GameObject[] itemsInInventory = new GameObject[5];
    [SerializeField] public Texture2D emptyTexture;
    [HideInInspector] public int slotEquipped = 0;
    private PlayerInventory playerInventory;
    private UseItem useItem;
    private void Start()
    {
        foreach (var image in images)
        {
            image.texture = emptyTexture;
        }

        playerInventory = FindFirstObjectByType<PlayerInventory>();
        useItem = FindFirstObjectByType<UseItem>();
    }

    public void ShowEquippedItem(int slot)
    {
        slotEquipped = slot;

        if (useItem.toolUsed) return; // cant equip/switch items while a tool animation is being used.


        foreach (var image in images)
        {
    
            Outline outline = image.GetComponentInParent<Outline>();
            

            if (image == images[slot])
            {
                outline.enabled = true;
            }
               
            else 
                outline.enabled = false;
        }

        if (itemsInInventory != null) // no point checking if nothing in inventory
            CheckIfItemEquipped();
    }

    public void DropItem()
    {
      for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == null) continue;

            if (i == slotEquipped)
            {
                Item item = itemsInInventory[i].GetComponent<Item>();
                
                if (item)
                {
                    itemsInInventory[i].transform.SetParent(null);
                    Rigidbody rb = itemsInInventory[i].AddComponent<Rigidbody>();
                    rb.useGravity = true;
                    images[i].texture = emptyTexture;
                    itemsInInventory[i].layer = 7;
                    item.isPickedUp = false;
                    itemsInInventory[i] = null;

                }

            }    
        }
    }
    
    public void CheckIfItemEquipped() // only shows the equipped item. items in inventory not equipped will be hidden.
    {
        
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == null)
            {
                if (i != slotEquipped) continue;
                Debug.Log("Nothing equipped");
                useItem.itemID = 0;
              
               
            }

            if (i == slotEquipped)
            {
                if (itemsInInventory[i] == null) continue;
                itemsInInventory[i].SetActive(true);
                Item item = itemsInInventory[i].GetComponent<Item>();
                if (item)
                {
                    Debug.Log($"Item equipped is: {item.itemStats.itemName}");
                    useItem.itemID = item.itemStats.id;
                }
               
            
            }
       
            
            else 
                itemsInInventory[i].SetActive(false);
        }
        
    }
  

}
