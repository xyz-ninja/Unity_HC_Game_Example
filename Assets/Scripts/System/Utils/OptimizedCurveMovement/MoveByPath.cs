using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPath : MonoBehaviour
{
	[Header("Move Path")]
	public Transform movePathTrans = default;

	[Header("Parametres")]
	public float moveSpeed = 3f;

	public bool isMoveX = true;
	public bool isMoveY = false;
	public bool isMoveZ = true;

	public bool isMoveFromStart = false;

	public bool isRotateToTarget = true;

	public float collideDistance = 0.5f;

	float bufferZone = 0.2f;

	int curMoveIndex = 0;

	bool isNeedMove = false;
	bool isMoveLooped = true;
    [HideInInspector] bool isFinishAtLeastOnce = false;

	Action onCompleteAction = null;

	void Start() {
		if (isMoveFromStart) {
			StartMove();
		}
	}

	void Update() {

		float dx = 0, dy = 0, dz = 0;
		
		if (isNeedMove) {

			var curPos = transform.position;

			var targetTrans = GetTargetTransform();

			if (isMoveX) {
				if (curPos.x < targetTrans.position.x - bufferZone) {
					dx = moveSpeed * Time.deltaTime;
				} else if (curPos.x > targetTrans.position.x + bufferZone) {
					dx = -moveSpeed * Time.deltaTime;
				}
			}

			if (isMoveY) {
				if (curPos.y < targetTrans.position.y - bufferZone) {
					dy = moveSpeed * Time.deltaTime;
				} else if (curPos.y > targetTrans.position.y + bufferZone) {
					dy = -moveSpeed * Time.deltaTime;
				}
			}

			if (isMoveZ) {
				if (curPos.z < targetTrans.position.z - bufferZone) {
					dz = moveSpeed * Time.deltaTime;
				} else if (curPos.z > targetTrans.position.z + bufferZone) {
					dz = -moveSpeed * Time.deltaTime;
				}
			}

			transform.position += new Vector3(dx, dy, dz);

			if (isRotateToTarget) {

				Vector3 correctedTargetPos = targetTrans.position;
				correctedTargetPos.y = transform.position.y;

				Vector3 relativePos = correctedTargetPos - transform.position;
				Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

				transform.rotation = targetRotation;
			}

			// check is target transform reached
			float distToTargetTrans = Vector3.Distance(transform.position, targetTrans.position);
			
			if (distToTargetTrans < collideDistance) {
				
				SwitchTargetMoveTransformToNext();

                if (targetTrans == GetLastTransform()) {

                    /*if (isMoveLooped == false) {
						StopMove();
					}*/
                }	
			}
		}
	}

	public void StartMove(Action _onComplete = null, bool _isLooped = true) {
        if (isNeedMove == false) {
            if (onCompleteAction != _onComplete) {
                onCompleteAction = _onComplete;
            }

            SwitchTargetMoveTransformToNext();

            isMoveLooped = _isLooped;
            isNeedMove = true;
        }
	}

	public void StopMove() {
		isNeedMove = false;
	}

    void SwitchTargetMoveTransformToNext() {
        int newMoveIndex = GetCurrentMoveIndex();

        newMoveIndex += 1;

        if (newMoveIndex > movePathTrans.childCount - 1) {
            if (isMoveLooped) {

                newMoveIndex = 0;

            } else {

                isFinishAtLeastOnce = true;

                StopMove();

                onCompleteAction?.Invoke();
            }
        } else {
            curMoveIndex = newMoveIndex;
        }
    }

	public void SetMovePathTransform(Transform _movePathTrans) {
		if (movePathTrans != _movePathTrans) {
			movePathTrans = _movePathTrans;

			curMoveIndex = 0;
		}
	}

    public void SetCustomBufferZone(float _bufferZone) {
        bufferZone = _bufferZone;
    }

	public Transform GetFirstTransform() {
		return movePathTrans.GetChild(0);
	}

	public Transform GetTargetTransform() {
		return movePathTrans.GetChild(GetCurrentMoveIndex());
	}

	public Transform GetLastTransform() {
		return movePathTrans.GetChild(movePathTrans.childCount - 1);
	}

	int GetCurrentMoveIndex() {
		return curMoveIndex;
	}

	public bool GetIsMoving() {
		return isNeedMove;
	}

    public bool GetIsFinishAtLeastOnce() {
        return isFinishAtLeastOnce;
    }
}
