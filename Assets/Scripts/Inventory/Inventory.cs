using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct ItemsStack {
        public InventoryItemData Data;
        public int ItemsCount;
    }
    
    private List<ItemsStack> _itemsStacks = new List<ItemsStack>();

    public void AddItem() {
        
    }
}
