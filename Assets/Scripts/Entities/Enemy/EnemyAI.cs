using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
    
    [SerializeField] private Enemy _enemy;

    [Header("Walk Around Options")]
    [SerializeField] private float _autoChangeDirectionTime = 4f;

    private Vector3 _moveAroundDirection;
    private Timer _autoChangeDirectionTimer;

    #region getters

    public Vector3 MoveAroundDirection => _moveAroundDirection;

    #endregion

    private void Awake() {
        _autoChangeDirectionTimer = new Timer(_autoChangeDirectionTime);
    }

    private void Update() {
        switch (_enemy.ActionMode) {
            
            case Enemy.ENEMY_ACTION_MODE.IDLE:
                
                _enemy.Movement.Stop();
                _enemy.ActionMode = Enemy.ENEMY_ACTION_MODE.MOVE_AROUND;
                
                break;
            
            case Enemy.ENEMY_ACTION_MODE.MOVE_AROUND:
                
                _autoChangeDirectionTimer.Update(Time.deltaTime);
                
                if (_moveAroundDirection == default || _autoChangeDirectionTimer.IsFinish()) {
                    ChangeMoveAroundDirection(new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)));
                    
                    _autoChangeDirectionTimer.Reload();
                }
                
                _enemy.Movement.MoveInDirection(_moveAroundDirection);
                
                break;
            
            case Enemy.ENEMY_ACTION_MODE.CHASE_PLAYER:

                var player = Game.Instance.World.CurrentLevel.Player;

                var playerDirection = (player.transform.position - transform.position).normalized;
                
                _enemy.Movement.MoveInDirection(playerDirection);
                
                break;
        }
    }

    public void ChangeMoveAroundDirection(Vector3 toDirection) {
        
        _moveAroundDirection = toDirection;
        
    }
}
