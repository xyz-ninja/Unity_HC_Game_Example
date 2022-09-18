using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SysUtils : MonoBehaviour
{
	public static SysUtils Instance;

	static int targetWidth = 1080;
	static int targetHeight = 1920;

	private void Awake() {
		Instance = this;
	}

    private void OnDestroy() {
        Instance = null;
    }

    // если значение должно всегда быть пропорциональным разрешению экрана под которое оно вводилось
    // то метод снизу вернет исправленное значение при неправильном соотношении сторон
    // по ширине
    public static float GetFloatValueProportionatelyTargetScreenWidth(float _curVal) {
		float v = _curVal;
        
        float s = GUI.Instance.Canvas.scaleFactor;
        
        return v * ((float) Screen.width / targetWidth) / s;
	}
    
	// по высоте
	public static float GetFloatValueProportionatelyTargetScreenHeight(float _curVal) {
		float v = _curVal;

        float s = GUI.Instance.Canvas.scaleFactor;
        
        return v * ((float) Screen.height / targetHeight) / s;
    }

    public static Vector2 GetCorrectMousePosition() {
        Vector2 mousePos = Input.mousePosition;
        Vector2 correctedMousePos = mousePos - new Vector2(Screen.width / 2, Screen.height / 2);
        float s = GUI.Instance.Canvas.scaleFactor;
        
        return correctedMousePos / s;
    }
    
    // этот легендарный метод позволяет получить правильный AnchoredPosition объекта в мире
    public static Vector2 GetWorldPositionOnScreen(Vector3 _position) {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_position);
        float h = Screen.height;
        float w = Screen.width;
        float x = screenPos.x - w / 2;
        float y = screenPos.y - h / 2;
        float s = GUI.Instance.Canvas.scaleFactor;

        return new Vector2(x, y) / s;
    }

    public static Vector3 GetMousePositionInWorld() {
        
        Plane mouseWorldPlane = new Plane(Vector3.up, 0);
        
        float distance;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (mouseWorldPlane.Raycast(ray, out distance)) {
            return ray.GetPoint(distance);
        } else {
            return default;
        }
    }

    public static float ClampAngle180(float _angle) {
        if (_angle > 180) {
            return _angle - 360;
        } else {
            return _angle;
        }
    }

    public static Vector3 ClampVector3Angles180(Vector3 _vec) {
        return new Vector3(ClampAngle180(_vec.x), ClampAngle180(_vec.y), ClampAngle180(_vec.z));
    }
    
    // эта дичь позволяет получить правильную позицию трансформа на экране
    // с помощью загадочного шаманства с другим трансформом
    // подходит если нужно переместить объект на экране из точки A в точку B (на экране)
    // если использовать anchoredPosition, то финальная позиция собъется с 99% вероятностью
     public static Vector2 GetTransformScreenPositionRelateToOther(Transform _fromT, Transform _toT) {
        
        var toDestinationInWorldSpace = _toT.position - _fromT.position;
        var toDestinationInLocalSpace = _fromT.InverseTransformVector(toDestinationInWorldSpace);
        
        return toDestinationInLocalSpace;
    }
}
