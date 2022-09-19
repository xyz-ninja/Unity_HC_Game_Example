using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPresenterBlock : MonoBehaviour {
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _label;

    public void SetupByItemsStack(Inventory.ItemsStack stack) {
        _image.sprite = stack.Data.ItemSprite;
        _label.text = "x" + stack.ItemsCount;
    }
}
