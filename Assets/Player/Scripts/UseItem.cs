using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    [HideInInspector] public int itemID = 0;
    public Animator animator;
    [HideInInspector] public bool toolUsed;

    private void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Empty"))
        {
            toolUsed = false;
  
        }

        else toolUsed = true;
        

        
    }
    public void ReadItemInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        animator.SetInteger("Tool State", itemID);
       


        switch (itemID)
        {
            case 0: Debug.Log("Nothing Equipped"); break;
            case 1: Debug.Log("Using axe"); animator.SetTrigger("Axe");     break;
            case 2: Debug.Log("Using sword"); animator.SetTrigger("Sword"); break;
            case 3: Debug.Log("Using pickaxe"); animator.SetTrigger("Pickaxe"); break;
        }


    }




}
