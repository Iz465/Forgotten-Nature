using UnityEngine;

public class Pickaxe : Item
{
    [SerializeField] private LayerMask rockLayer;
    public void MineOre()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, rockLayer);

        if (colliders.Length == 0)
        {
            Debug.Log("No ore to mine");
            return;
        }

        Debug.Log("Mining ore!");

        Rock rock = colliders[0].GetComponent<Rock>();

        if (rock)
            rock.TakeDamage(20);


    }
}
