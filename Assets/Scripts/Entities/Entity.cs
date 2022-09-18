using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    
    [Header("Entity Transforms")] 
    [SerializeField] protected Transform _visualT;
    
    [Header("Entity Components")]
    [SerializeField] protected Rigidbody _rb;
}
