using DG.Tweening;
using UnityEngine;

public class PhysicsSwitcher : MonoBehaviour {

	[Header("Components")]
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Collider col;

	[Header("Debug")]
	public bool isDebugEnabled = false;

	public Transform debugHolderTrans = default;
	public SpriteRenderer debugSprRenderer;

	[Header("Parametres")]
	[SerializeField] private bool isPhysicsEnabled = true;
	[SerializeField] private bool isSolid = true;
    [SerializeField] private bool isAutodisableAtStart = false;
    [SerializeField] private float autodisableTime = 1f;

	[Header("Rotate Options")]
	[SerializeField] float rotateDuration = 0.7f;
	public bool isRotateX = false, isRotateY = true, isRotateZ = false;

	private Vector3 initRotationEuler;
	private float initAngularDrag;
	
	private bool isNeedAutodisable = false;
	private Timer autodisableTimer;

	Tweener rbRotateTween = null;

	void Awake() {

        autodisableTimer = new Timer(autodisableTime);
        
		if (debugHolderTrans != default) {
			if (isDebugEnabled) {
				debugHolderTrans.gameObject.SetActive(true);

			} else {
				debugHolderTrans.gameObject.SetActive(false);
			}
		}

		initRotationEuler = transform.rotation.eulerAngles;

		SetPhysicsEnabled(isPhysicsEnabled);
		SetIsSolid(isSolid);
	}

	void Start() {
		initAngularDrag = rb.angularDrag;
	}

	private void OnEnable() {

        if (isAutodisableAtStart) {
			SetAutodisableEnabled(true);
		}

        autodisableTimer.Reload();
    }

	private void OnDestroy() {
		rbRotateTween.Kill();
		rbRotateTween = null;
	}

	private void Update() {

		if (isNeedAutodisable) {
			
			autodisableTimer.Update(Time.deltaTime);

			if (autodisableTimer.IsFinish()) {

				SetPhysicsEnabled(false);

				isNeedAutodisable = false;
				autodisableTimer.Reload();
			}
		}
	}

	public void SetPhysicsEnabled(bool _b) {
        isPhysicsEnabled = _b;

		Color debugColor;

		if (isPhysicsEnabled) {

			rb.isKinematic = false;

			debugColor = Color.green;

		} else {

            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            debugColor = Color.red;
		}

		if (isDebugEnabled) {
			debugSprRenderer.color = debugColor;
		}
    }

	public void SetIsSolid(bool _b) {
		
		isSolid = _b;

		if (isSolid) {
			col.isTrigger = false;
		} else {
			col.isTrigger = true;
		}
		
	}

	public void SetAutodisableEnabled(bool _b) {
		if (_b) {
			isNeedAutodisable = true;
			autodisableTimer.Reload();
		} else {
			isNeedAutodisable = false;
			autodisableTimer.Reload();
		}
	}

    public void DisableCollidersCollisions(Collider _col1, Collider _col2) {
        Physics.IgnoreCollision(_col1, _col2, true);
    }

    #region getters/setters

    public void SetRotationEuler(Vector3 _rotEuler) {
		//transform.rotation = Quaternion.Euler(_rotEuler);

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		if (rbRotateTween.IsActive() == false) {
			rbRotateTween = transform.DORotate(_rotEuler, rotateDuration).OnComplete(() => {

				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;

				DisableRotateTween();
			});
		}
	}

	public void DisableRotateTween() {
		if (rbRotateTween.IsActive()) {
			rbRotateTween.Kill();
		}
	}

	public void RotateToPosition(Vector3 _pos) {

		Vector3 correctedTargetPos = _pos;

		correctedTargetPos.y = transform.position.y;

		Vector3 relativePos = correctedTargetPos - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

		targetRotation.x = isRotateX ? targetRotation.x : transform.rotation.eulerAngles.x;
		targetRotation.y = isRotateY ? targetRotation.y : transform.rotation.eulerAngles.y;
		targetRotation.z = isRotateZ ? targetRotation.z : transform.rotation.eulerAngles.z;

		//transform.rotation = targetRotation;

		SetRotationEuler(targetRotation.eulerAngles);
	}

	public Vector3 GetInitRotationEuler() {
		return initRotationEuler;
	}

	public Vector3 GetRotationEuler() {
		return rb.rotation.eulerAngles;
	}

	public Rigidbody GetRigidbody() {
		return rb;
	}

	public Collider GetCollider() {
		return col;
	}

	public bool GetIsPhysicsEnabled() {
		return isPhysicsEnabled;
	}

	public bool GetIsSolid() {
		return isSolid;
	}

	#endregion
}
