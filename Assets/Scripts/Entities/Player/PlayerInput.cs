using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [SerializeField] private Player _player;

    private bool _isEnabled = true;
    
    private VariableJoystick _joystick;
    
    private void Start() {
        _joystick = GUI.Instance.Joystick;
    }

    private void Update() {
        
        if (_isEnabled == false) {
            return;
        }

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0) {
            
            var targetMoveDirection = new Vector3(_joystick.Horizontal, 0 , _joystick.Vertical);
            _player.Movement.MoveInDirection(targetMoveDirection);
            
        } else {
            
            _player.Movement.Stop();
        }
    }
}
