using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {
    
    [SerializeField] private int _coins = 0;

    #region getters

    public int Coins => _coins;

    #endregion

    public event Action CoinsCountChanged;
    
    public void AddCoins(int count) {
        _coins += count;
        
        CoinsCountChanged?.Invoke();
    }
}
