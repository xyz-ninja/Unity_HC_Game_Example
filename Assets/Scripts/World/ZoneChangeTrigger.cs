using UnityEngine;

using static World;

public class ZoneChangeTrigger : MonoBehaviour {
    
    [SerializeField] private ZONE_TYPE _zoneType = ZONE_TYPE.DANGER; 
    
    private void OnTriggerEnter(Collider other) {

        var attachedRigidbody = other.attachedRigidbody;

        if (attachedRigidbody != null) {
            var entity = attachedRigidbody.gameObject.GetComponent<Entity>();

            if (entity != null) {
                entity.ZoneObserver.SetZoneType(_zoneType);
            }
        }
    }
}
