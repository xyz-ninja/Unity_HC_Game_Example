using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisions : MonoBehaviour {
    
    [SerializeField] private Projectile _projectile;

    private Stack<string> _targetTags = new Stack<string>();

    private bool _isCollided = false;
    
    private void OnTriggerEnter(Collider other) {

        if (_isCollided) {
            Debug.Log("Already collided");
            return;
        }
        
        var otherRigidbody = other.attachedRigidbody;

        if (otherRigidbody != null) {
            var entity = otherRigidbody.gameObject.GetComponent<Entity>();
            
            if (entity != null && _targetTags.Contains(entity.gameObject.tag)) {
                Hit();
            }
            
        } else {
            
            if (other.gameObject.layer == LayerMask.GetMask("Solid")) {
                Hit();
            }
        }
    }

    private void Hit() {
        
        Debug.Log("Hit!");
        
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
