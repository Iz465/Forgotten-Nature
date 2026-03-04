using UnityEngine;

public class RelayAnimationEvents : MonoBehaviour
{
    [SerializeField] private Pickaxe pickaxe;

    private void PickaxeHit()
    {
        if (pickaxe) pickaxe.MineOre();
    }

}
