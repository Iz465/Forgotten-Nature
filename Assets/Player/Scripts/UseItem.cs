using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    [HideInInspector] public int itemID = 0;
    public Animator animator;
    [HideInInspector] public bool toolUsed;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            toolUsed = false;
            Debug.Log("Moving");
        }

        else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Empty"))
        {
            Debug.Log("Tool layer is empty");
            animator.SetBool("ToolUsed", false);
        }
            
      
        else Debug.Log("Using tool");

        
    }
    public void ReadItemInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        animator.SetInteger("Tool State", itemID);
        if (itemID != 0) animator.SetBool("ToolUsed", true);
        else animator.SetBool("ToolUsed", false);

        toolUsed = animator.GetBool("ToolUsed");

        switch (itemID)
        {
            case 0: Debug.Log("Nothing Equipped"); break;
            case 1: Debug.Log("Using axe"); animator.SetTrigger("Axe");     break;
            case 2: Debug.Log("Using sword"); animator.SetTrigger("Sword"); break;
            case 3: Debug.Log("Using pickaxe"); animator.SetTrigger("Pickaxe"); break;
        }


    }


}
