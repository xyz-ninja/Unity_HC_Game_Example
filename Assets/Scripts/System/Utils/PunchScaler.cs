using DG.Tweening;
using UnityEngine;

public class PunchScaler : MonoBehaviour
{
	[SerializeField] private Transform _target = default;
	[SerializeField] private Vector3 _strength = new Vector3(0.5f, 0.5f, 0f);
    [Range(0f, 2f)]
	[SerializeField] private float _animationDuration = 0.2f;

    private void OnDestroy()
	{
		_target.transform.DOKill();
	}

    private Tweener scaleTween;

    public void Scale() {
        if (scaleTween.IsActive() == false) {
            scaleTween = _target.transform.DOPunchScale(_strength, _animationDuration, 0).OnComplete(() => {
                scaleTween.Kill();
            });
        }
    }
}