using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 movement2D;
    private Vector3 movement3D;
    private Vector2 mouse2D;
    private CharacterController controller;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Animator animator;

    private float gravity = -9.82f;
    [SerializeField] private float jumpForce;
    private float verticalVelocity = -2f;
    private bool canJump = true;

    private void Update()
    {
        ApplyMovement();
        ApplyLooking();
        ApplyGravity();
       
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void ReadMovementInput(InputAction.CallbackContext context) // reads players movement direction input
    {
        if (context.performed) 
            movement2D = context.ReadValue<Vector2>();

        if (context.canceled)
            movement2D = Vector2.zero;
    }


    private void ApplyMovement()
    {
        movement3D  = new Vector3(movement2D.x, 0, movement2D.y);
        Vector3 movementRotation = transform.TransformDirection(movement3D);
        movementRotation.y = verticalVelocity;

        if (animator)
        {
            if (movement2D != Vector2.zero) animator.SetBool("canRun", true);
            else animator.SetBool("canRun", false);
        }
  
            
       
        if (controller)
            controller.Move(movementRotation * movementSpeed * Time.deltaTime); 

    }



    public void ReadMouseInput(InputAction.CallbackContext context) // reads players mouse movement input
    {
        mouse2D = context.ReadValue<Vector2>();


    }

    private float yaw = 0; // y
    private float pitch = 0;  // x
    private void ApplyLooking()
    {

        yaw += mouse2D.x * mouseSensitivity;
        pitch -= mouse2D.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, -10, 60); // stops unrealistic pitch rotation

        if (playerCamera)
            playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        transform.rotation = Quaternion.Euler(0, yaw, 0);
    }

    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    
        if (canJump)
        {
            verticalVelocity = jumpForce;
            canJump = false;
        }
        
    }

    [SerializeField] private Transform footLocation;
    [SerializeField] private LayerMask groundLayer;
    private void ApplyGravity()
    {
        Vector3 endLocation = footLocation.position + Vector3.down * .2f;
        if (Physics.Linecast(footLocation.position, endLocation, groundLayer))
        {
            canJump = true;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    [SerializeField] private LayerMask interactableLayer;

    [SerializeField] private Transform holdTransform;

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
            Item item = hits[0].gameObject.GetComponent<Item>();

            Player playerCheck = item.GetComponentInParent<Player>();
            if (playerCheck == null)
                item.PickItemUp(holdTransform, item);

            else
                Debug.Log($"Not interactable. Reason: item picked up = {item.isPickedUp}");





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
//  Gizmos.DrawLine(playerCamera.transform.position, endLocation);