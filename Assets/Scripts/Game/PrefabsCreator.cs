using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PrefabsCreator : MonoBehaviour
{
    public static PrefabsCreator Instance;

    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public GameObject PickableItemPrefab { get; private set; }

    [field: SerializeField] public GameObject InventoryPresenterBlock { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void OnDestroy() {
        Instance = null;
    }

    public static GameObject CreatePooledPrefab(GameObject prefab, Vector3 position = default, Transform parentT = default, bool useLocalPosition = false) {
        var obj = LeanPool.Spawn(prefab, parentT);
        
        if (useLocalPosition) {
            obj.transform.localPosition = position;
        } else {
            obj.transform.position = position;
        }
        
        return obj;
    }
    
    public static GameObject CreatePrefab(GameObject prefab, Vector3 position = default, Transform parentT = default, bool useLocalPosition = false) {
        var obj = Instantiate(prefab, parentT);
        
        if (useLocalPosition) {
            obj.transform.localPosition = position;
        } else {
            obj.transform.position = position;
        }

        return obj;
    } 
}
