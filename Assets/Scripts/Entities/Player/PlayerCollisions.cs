using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {

    [SerializeField] private Player _player;
    
    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case "PickableItem":

                var pickableItem = other.gameObject.GetComponent<PickableItem>();

                if (pickableItem.AvailableToPick) {
                    pickableItem.Pick(_player.Inventory);
                }
                
                break;
        }
    }
}
