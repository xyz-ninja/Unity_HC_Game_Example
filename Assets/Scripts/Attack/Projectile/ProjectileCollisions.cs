using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisions : MonoBehaviour {
    
    [SerializeField] private Projectile _projectile;
    [SerializeField] private LayerMask _solidLayers;
    
    private Stack<string> _targetTags = new Stack<string>();

    private bool _isCollided = false;
    
    private void OnTriggerEnter(Collider other) {

        if (_isCollided) {
            return;
        }
        
        var otherRigidbody = other.attachedRigidbody;

        if (otherRigidbody != null) {
            
            var entity = otherRigidbody.gameObject.GetComponent<Entity>();
            
            if (entity != null && _targetTags.Contains(entity.gameObject.tag)) {
                
                entity.Health.DealDamage(_projectile.Damage);
                
                entity.Rb.AddForce(_projectile.MoveInDirection.TargetMoveDirection * _projectile.PunchForce, ForceMode.Impulse);
                
                Hit();
            }
            
        } else {
            
            if ((_solidLayers.value & (1 << other.transform.gameObject.layer)) > 0) {
                Hit();
            }
        }
    }

    private void Hit() {

        _projectile.DestroyProjectile();
        
        _isCollided = true;
    }

    public void Clear() {
        _targetTags.Clear();

        _isCollided = false;

    }
    
    public void AddTargetEntityTag(string tag) {
        if (_targetTags.Contains(tag)) {
            return;
        }
        
        _targetTags.Push(tag);
    }
}
