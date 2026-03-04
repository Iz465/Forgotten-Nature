using UnityEngine;

public class Gatherables : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private int resourceAmount;
    [SerializeField] private GameObject resource;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Destroyed();
    }

    private void Destroyed()
    {
        for (int i = 0; i < resourceAmount; i++)
        {
            Instantiate(resource, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
