using UnityEngine;

public class World : MonoBehaviour {
    
    [Header("Components")] 
    [SerializeField] private Level _currentLevel; // TODO: убрать сериализацию

    #region getters

    public Level CurrentLevel => _currentLevel;

    #endregion
}
