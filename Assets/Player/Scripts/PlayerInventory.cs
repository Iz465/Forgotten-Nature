using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private InventoryUI inventoryUI;
    [SerializeField] private LayerMask interactableLayer;
    public Transform holdTransform;
    [SerializeField] private Camera playerCamera;
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




    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector3 endLocation = playerCamera.transform.position + playerCamera.transform.forward * 5;
        Vector3 boxSize = new Vector3(2, 2, 5);
        Vector3 boxCenter = (playerCamera.transform.position + endLocation) / 2;
        Quaternion boxRotation = playerCamera.transform.rotation;

        Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, boxRotation, interactableLayer);

        if (hits.Length > 0)
        {
            Item item = null;
            foreach (var hit in hits)
            {
                item = hit.gameObject.GetComponent<Item>();
                if (item.isPickedUp == false)
                    break;
            }



            Player playerCheck = item.GetComponentInParent<Player>();
            if (playerCheck == null)
                item.PickItemUp(holdTransform, item);

            else
                Debug.Log($"Not interactable. Reason: item picked up = {item.isPickedUp} item is: {hits[0].gameObject.name}");

        }


    }

 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Vector3 endLocation = playerCamera.transform.position + playerCamera.transform.forward * 5;
        Vector3 boxSize = new Vector3(2, 2, 5);
        Vector3 boxCenter = (playerCamera.transform.position + endLocation) / 2;
        Quaternion boxRotation = playerCamera.transform.rotation;

        Gizmos.matrix = Matrix4x4.TRS(boxCenter, boxRotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

        Gizmos.matrix = Matrix4x4.identity;



    }




}
