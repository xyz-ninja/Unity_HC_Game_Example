using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour {

    [Header("Main")]
    [SerializeField] private WeaponData _weaponData;

    [Header("Transforms")] 
    [SerializeField] private Transform _shootPointT;
    
    [Header("Options")]
    [ShowIf("_isFirearmsMode"), SerializeField] private GameObject _projectile;
    [SerializeField] private bool _attackClosestEnemy = true;
    [SerializeField] private bool _autoSearchEnemies = true; // TODO: думаю можно это сделать абстрактным
    [ShowIf("_autoSearchEnemies"), SerializeField] private LayerMask _enemiesLayer;

    private bool _attackEnabled = true;
    
    private Timer _autoAttackTimer = new Timer(1);

    public event Action OnAttack;
    
    private bool _isMeleeMode;
    private bool _isFirearmsMode;

    #region getters/setters

    public bool AttackEnabled {
        get => _attackEnabled;
        set => _attackEnabled = value;
    }

    public WeaponData WeaponData {
        get => _weaponData;
        set {
            _weaponData = value;
            
            _autoAttackTimer.ChangeInitTime(_weaponData.AttackDelay);
        }
    }

    #endregion
    
    private void Awake() {
        if (_weaponData != null) {
            WeaponData = _weaponData;
        }
    }

    private void Update() {

        if (_attackEnabled == false) {
            return;
        }
        
        if (_autoSearchEnemies) {
            
            _autoAttackTimer.Update(Time.deltaTime);

            if (_autoAttackTimer.IsFinish()) {
                
                SearchEnemies(_attackClosestEnemy);
                
                _autoAttackTimer.Reload();
            }
        }
    }
    
    // метод некорректно имеет метку Perfomance Critical Context в райдере потому что он вызывается из Update внутри таймера
    private List<Entity> _foundedEnemies = new List<Entity>();
    private void SearchEnemies(bool attackClosest = true) {
        
        _foundedEnemies.Clear(); // это гораздо оптимизированнее, чем создавать новый список внутри метода
        
        var hits = Physics.SphereCastAll(
            transform.position, _weaponData.AttackRange, transform.forward,
            _weaponData.AttackRange, _enemiesLayer);

        foreach (var hit in hits) {
            // не переживайте, GetComponent с недавних пор кэшируется и он довольно быстрый
            var entity = hit.collider.attachedRigidbody.gameObject.GetComponent<Entity>();

            if (entity == null) {
                Debug.Log("Entity strangely missed!");
                continue;
            }
            
            _foundedEnemies.Add(entity);
        }

        if (_foundedEnemies.Count > 0) {
            if (attackClosest) {
                // а вот это не очень оптимизированно, майскрософт обещает разобраться с аллоками в будущих версиях LINQ
                // но в целом они не такие страшные что бы отказаться от использования
                _foundedEnemies = _foundedEnemies.OrderBy(
                    x => Vector3.Distance(this.transform.position, x.transform.position)
                ).ToList();

                Attack(_foundedEnemies[0]);

            } else {
                
                // атакует случайного противника
                Attack(_foundedEnemies[Random.Range(0, _foundedEnemies.Count)]);
            }
        }
    }
    
    public void Attack(Entity entity) {

        OnAttack?.Invoke();

        switch (_weaponData.AttackMode) {
            case WeaponData.ATTACK_MODE.FIREARMS:

                var shootPoint = _shootPointT.position;
                
                var projectileObject = PrefabsCreator.CreatePooledPrefab(_projectile, shootPoint);
                var projectile = projectileObject.GetComponent<Projectile>();

                var accuracySpread = _weaponData.AccuracySpread;

                var targetPoint = entity.transform.position;
                targetPoint.x += Random.Range(accuracySpread.x, accuracySpread.y);
                targetPoint.z += Random.Range(accuracySpread.x, accuracySpread.y);
                
                var direction = (targetPoint - shootPoint).normalized;
                
                projectile.Init(direction, entity.gameObject.tag);
                
                break;
        }
    }
    
    private void OnValidate() {
        if (_weaponData == null) {
            return;
        }
        
        _isMeleeMode = _weaponData.AttackMode == WeaponData.ATTACK_MODE.MELEE;
        _isFirearmsMode = _weaponData.AttackMode == WeaponData.ATTACK_MODE.FIREARMS;
    }

    private void OnDrawGizmos() {
        if (_weaponData == null) {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _weaponData.AttackRange);
    }
}
