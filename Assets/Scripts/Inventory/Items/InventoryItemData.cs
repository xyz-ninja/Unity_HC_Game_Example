using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item Data", order = 52)]
public class InventoryItemData : ScriptableObject {

    public string ItemName = "Unknown Item";
    [SerializeField] private Sprite _itemSprite;
    [SerializeField] private int _price = 1;

    public Sprite ItemSprite => _itemSprite;
    public int Price => _price;
}
