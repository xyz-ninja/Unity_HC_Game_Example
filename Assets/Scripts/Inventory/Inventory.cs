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

    public void SellItems() {
        if (_itemsStacks.Count > 0) {
            StartCoroutine(CoroSellItems());
        }
    }

    IEnumerator CoroSellItems() {
        
        while (_itemsStacks.Count > 0) {
            var stack = _itemsStacks[0];
            Game.Instance.GlobalData.AddCoins(stack.ItemsCount * stack.Data.Price);

            _itemsStacks.Remove(stack);
            
            ItemStackAdded?.Invoke();
            
            yield return new WaitForSeconds(1);
        }
        
        _itemsStacks.Clear();
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
