using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    InventoryUI inventoryUI;
    private void Start()
    {
        inventoryUI = FindAnyObjectByType<InventoryUI>();
    }

    public void ReadEquipItem1Input(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        inventoryUI.ShowEquippedItem(0);
    }

    public void ReadEquipItem2Input(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        inventoryUI.ShowEquippedItem(1);
    }

    public void ReadEquipItem3Input(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        inventoryUI.ShowEquippedItem(2);
    }

    public void ReadEquipItem4Input(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        inventoryUI.ShowEquippedItem(3);
    }

    public void ReadEquipItem5Input(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        inventoryUI.ShowEquippedItem(4);
    }

    public void ReadDropItemInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        inventoryUI.DropItem();
    }



}
