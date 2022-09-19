using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour {
    
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LayerMask _immovableLayers;

    private float _collsisionCheckRange = 1.5f;
    
    private float _updateCollisionTime = 0.5f;
    private Timer _updateCollisionTimer;

    private void Awake() {
        _updateCollisionTimer = new Timer(_updateCollisionTime);
    }

    private void Update() {
        
        _updateCollisionTimer.Update(Time.deltaTime);

        if (_updateCollisionTimer.IsFinish()) {

            if (_enemy.ActionMode == Enemy.ENEMY_ACTION_MODE.MOVE_AROUND) {

                var hits = Physics.RaycastAll(transform.position, _enemy.RootT.forward, _collsisionCheckRange,
                    _immovableLayers);

                foreach (var hit in hits) {

                    if (hit.collider.gameObject == this.gameObject) {
                        continue;
                    }
                    
                    _enemy.ActionMode = Enemy.ENEMY_ACTION_MODE.IDLE;

                    var targetDirection = (transform.position - hit.point).normalized;
                    targetDirection.y = 0;
                    _enemy.AI.ChangeMoveAroundDirection(targetDirection);

                    break;
                }
            }

            _updateCollisionTimer.Reload();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _enemy.RootT.forward * _collsisionCheckRange);
    }
}
