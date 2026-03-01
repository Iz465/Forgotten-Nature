using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemStats itemStats;

    public void PickItemUp(Transform holdTransform, Item item)
    {

        bool inventoryFull = true;
        InventoryUI inventoryUI = FindAnyObjectByType<InventoryUI>();
        if (inventoryUI)
        {
            foreach (var image in inventoryUI.images)
            {
                if (image.texture == inventoryUI.emptyTexture)
                {
                    image.texture = itemStats.icon;
                    inventoryFull = false;
                    Debug.Log($"{itemStats.itemName} picked up");
                    break;
                }
            }



            Instantiate(item.itemStats.item, holdTransform.position, holdTransform.rotation);
            Destroy(item.gameObject);

            if (inventoryFull)
            {
                inventoryUI.images[inventoryUI.slotEquipped].texture = itemStats.icon;
            }
              

        }
    }
}
