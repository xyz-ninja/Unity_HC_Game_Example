using UnityEngine;

public class Game : MonoBehaviour {
    
    public static Game Instance;

    [Header("Main")]
    [SerializeField] private CamerasManager _camerasManager;
    [SerializeField] private World _world;
    [SerializeField] private GlobalData _globalData;
    
    #region getters

    public CamerasManager CamerasManager => _camerasManager;
    public World World => _world;
    public GlobalData GlobalData => _globalData;

    #endregion
    
    private void Awake() {
        Instance = this;
    }

    private void OnDestroy() {
        Instance = null;
    }
}
