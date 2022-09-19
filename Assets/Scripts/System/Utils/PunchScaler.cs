using DG.Tweening;
using UnityEngine;

public class PunchScaler : MonoBehaviour {
    
	[SerializeField] private Transform _target = default;
	[SerializeField] private Vector2 _strength = new Vector2(0.5f, 0.5f);
	[Range(0f, 2f)] [SerializeField] private float _animationDuration = 0.2f;

	private Vector3 _initialScale;

	private void Awake() {
		_initialScale = _target.transform.localScale;
	}

	private void OnDestroy() {
		_scaleTween.Kill();
	}

	private Tweener _scaleTween;

	public void Scale() {
		if (_scaleTween.IsActive() == false) {
			_target.transform.localScale = _initialScale;

			_scaleTween = _target.transform.DOPunchScale(_strength, _animationDuration, 0).OnComplete(() => {
				_scaleTween.Kill();
			});
		}
	}
}