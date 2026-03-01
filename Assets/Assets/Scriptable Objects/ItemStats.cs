using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item Stats", menuName = "Item Stats Scriptable Object")]
public class ItemStats : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public GameObject item;
    [SerializeField] public float weight;
    [SerializeField] public Texture2D icon;

}
