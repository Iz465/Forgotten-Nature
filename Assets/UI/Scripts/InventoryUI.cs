using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public RawImage[] images;
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
        foreach (var image in images)
        {
            Outline outline = image.GetComponentInParent<Outline>();
            if (!outline) return;

            if (image == images[slot])
                outline.enabled = true;
            else 
                outline.enabled = false;
        }

        slotEquipped = slot;
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
    

}
