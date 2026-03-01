using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public RawImage[] images;
    [HideInInspector] public GameObject[] itemsInInventory = new GameObject[5];
    [SerializeField] public Texture2D emptyTexture;
    [HideInInspector] public int slotEquipped = 0;
    PlayerInventory playerInventory;
    private void Start()
    {
        foreach (var image in images)
        {
            image.texture = emptyTexture;
        }

        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    public void ShowEquippedItem(int slot)
    {
        slotEquipped = slot;

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
            if (itemsInInventory[i] == null) continue;

            if (i == slotEquipped)
                itemsInInventory[i].SetActive(true);
            
            else itemsInInventory[i].SetActive(false);
        }
        
    }
  

}
