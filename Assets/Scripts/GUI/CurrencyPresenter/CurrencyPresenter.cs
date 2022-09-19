using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPresenter : MonoBehaviour {
	
	[SerializeField] private TextMeshProUGUI _label;
	[SerializeField] private PunchScaler _punchScaler;
	
	private void Start() {
		Game.Instance.GlobalData.CoinsCountChanged += UpdateCurrency;
	}

	private void OnApplicationQuit() {
		Game.Instance.GlobalData.CoinsCountChanged -= UpdateCurrency;
	}
	
	public void UpdateCurrency() {
		_label.text = Game.Instance.GlobalData.Coins.ToString();
		_punchScaler.Scale();
	}
}
