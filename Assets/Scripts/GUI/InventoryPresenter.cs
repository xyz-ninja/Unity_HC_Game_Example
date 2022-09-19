using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour {

	[SerializeField] private RectTransform _blocksT;
	[SerializeField] private float _blockYOffset = 10f;
	
	private Inventory _playerInventory;
	
	private List<InventoryPresenterBlock> _blocks = new List<InventoryPresenterBlock>();
	
	private void Start() {

		_blockYOffset = SysUtils.GetFloatValueProportionatelyTargetScreenHeight(_blockYOffset);
		
		var level = Game.Instance.World.CurrentLevel;
		_playerInventory = level.Player.Inventory;

		_playerInventory.ItemAdded += UpdateView;
	}

	private void OnDisable() {
		_playerInventory.ItemAdded -= UpdateView;
	}

	private void UpdateView() {
		
		var stacks = _playerInventory.ItemsStacks;
		
		//Debug.Log("stacks count" + stacks.Count.ToString());
		
		if (stacks.Count != _blocks.Count) {
			
			foreach (var block in _blocks) {
				LeanPool.Despawn(block.gameObject);
			}
			
			_blocks.Clear();

			for (int i = 0; i < stacks.Count; i++) {

				var blockPosition = _blocksT.localPosition;
				blockPosition.y -= _blockYOffset * i;

				var blockObject = PrefabsCreator.CreatePooledPrefab(PrefabsCreator.Instance.InventoryPresenterBlock,
					blockPosition, _blocksT, true);

				var block = blockObject.GetComponent<InventoryPresenterBlock>();
				block.SetupByItemsStack(stacks[i]);
				
				_blocks.Add(block);
			}

		} else {

			for (int i = 0; i < stacks.Count; i++) {
				_blocks[i].SetupByItemsStack(stacks[i]);
			}
		}
	}
}
