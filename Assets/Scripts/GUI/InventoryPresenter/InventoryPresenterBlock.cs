using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPresenterBlock : MonoBehaviour {
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private PunchScaler _punchScaler;

    private int itemsCount = -1;
    
    public void SetupByItemsStack(Inventory.ItemsStack stack) {
        _image.sprite = stack.Data.ItemSprite;

        if (itemsCount != stack.ItemsCount) {
            _label.text = "x" + stack.ItemsCount;

            _punchScaler.Scale();

            itemsCount = stack.ItemsCount;
        }
    }
}
