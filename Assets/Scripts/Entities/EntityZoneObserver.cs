using System;
using UnityEngine;

using static World;

public class EntityZoneObserver : MonoBehaviour {
    
    [SerializeField] private ZONE_TYPE _zoneType = ZONE_TYPE.PLAYER_BASE;

    public event Action ZoneChanged;
    
    #region getters/setters

    public ZONE_TYPE ZoneType => _zoneType;

    #endregion

    public void SetZoneType(ZONE_TYPE zoneType) {
        
        if (_zoneType == zoneType) {
            return;
        }
        
        _zoneType = zoneType;
        
        ZoneChanged?.Invoke();
    }
}
