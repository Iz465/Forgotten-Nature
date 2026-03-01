using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public RawImage[] images;
    [HideInInspector] public GameObject[] itemsHeld = new GameObject[5];
    [SerializeField] public Texture2D emptyTexture;
    [HideInInspector] public int slotEquipped = 0;

    private void Start()
    {
        foreach (var image in images)
        {
            image.texture = emptyTexture;
        }
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
        foreach (var image in images)
        {
            Outline outline = image.GetComponentInParent<Outline>();
            if (!outline) return;
            if (outline.enabled == true)
            {
                image.texture = emptyTexture;
            }
        
        }
    }
    
    public void CheckIfItemEquipped() // only shows the equipped item. items in inventory not equipped will be hidden.
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == null) continue;

            if (i == slotEquipped)
                itemsHeld[i].SetActive(true);
            
            else itemsHeld[i].SetActive(false);
        }
        
    }
  

}
