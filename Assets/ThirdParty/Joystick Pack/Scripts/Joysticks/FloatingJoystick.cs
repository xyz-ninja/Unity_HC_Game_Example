using UnityEngine;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    protected override void OnPointerDown(Vector2 pointerPos)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(pointerPos);
        background.gameObject.SetActive(true);
        base.OnPointerDown(pointerPos);
    }

    protected override void OnPointerUp(Vector2 pointerPos)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(pointerPos);
    }
}