using UnityEngine;

using static World;

public class EntityZoneObserver : MonoBehaviour {
    
    [SerializeField] private ZONE_TYPE _zoneType = ZONE_TYPE.PLAYER_BASE;

    #region getters/setters

    public ZONE_TYPE ZoneType {
        get => _zoneType;
        set => _zoneType = value;
    }

    #endregion
}
