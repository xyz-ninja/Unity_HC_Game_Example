using UnityEngine;

public class World : MonoBehaviour {

    public enum ZONE_TYPE { PLAYER_BASE, DANGER }

    [Header("Components")] 
    [SerializeField] private Level _currentLevel; // TODO: убрать сериализацию

    #region getters

    public Level CurrentLevel => _currentLevel;

    #endregion
}
