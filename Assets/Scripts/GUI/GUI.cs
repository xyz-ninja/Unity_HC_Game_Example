using HCExample.GUI;
using UnityEngine;

public class GUI : MonoBehaviour {
    
    public static GUI Instance;

    [Header("Components")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PanelsManager _panelsManager;
    [SerializeField] private VariableJoystick _joystick;

    #region getters

    public Canvas Canvas => _canvas;
    public VariableJoystick Joystick => _joystick;

    #endregion
    
    private void Awake() {
        Instance = this;
    }

    private void OnDestroy() {
        Instance = null;
    }
}
