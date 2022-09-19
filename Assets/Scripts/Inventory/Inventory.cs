using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public class ItemsStack {
        public InventoryItemData Data;
        public int ItemsCount;
    }
    
    private List<ItemsStack> _itemsStacks = new List<ItemsStack>();

    #region getters

    public List<ItemsStack> ItemsStacks => _itemsStacks;

    #endregion
    
    public event Action ItemStackAdded;
    public event Action ItemAdded;
    
    public void AddItem(InventoryItemData itemData) {
        
        var targetStack = GetStackWithData(itemData);

        if (targetStack == null) {
            
            CreateStack(itemData);
            
        } else {
            
            Debug.Log(targetStack.Data.ItemName + " count " + targetStack.ItemsCount);
            targetStack.ItemsCount += 1;
        }
        
        ItemAdded?.Invoke();
    }

    private void CreateStack(InventoryItemData itemData) {
        
        var stack = new ItemsStack() {
            Data = itemData,
            ItemsCount = 1
        };
        
        _itemsStacks.Add(stack);
        
        ItemStackAdded?.Invoke();
    }

    [CanBeNull]
    private ItemsStack GetStackWithData(InventoryItemData data) {
        
        foreach (var stack in _itemsStacks) {
            if (stack.Data.ItemName == data.ItemName) {
                return stack;
            }
        }

        return null;
    }
}
