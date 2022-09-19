using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPositionsPath : MonoBehaviour {
    [Header("Transforms")] public Transform checkGroundPosTrans;

    [Header("Components")] public PhysicsSwitcher physicsSwitcher;

    [Header("Parametres")] public float moveSpeed = 3f;

    public bool isMoveX = true;
    public bool isMoveY = false;
    public bool isMoveZ = true;

    public bool isMoveFromStart = false;
    public bool isRotateToTarget = true;

    public float collideDistance = 0.5f;

    private List<Vector3> movePath = default;

    float bufferZone = 0.1f;

    int curMoveIndex = 0;

    bool isNeedMove = false;
    bool isMoveLooped = true;
    [HideInInspector] bool isFinishAtLeastOnce = false;

    float checkIsOnGroundTime = 0.2f;
    Timer checkIsOnGroundTimer;

    [SerializeField] bool isOnGround = false;

    Action onCompleteAction = null;

    void Start() {
        checkIsOnGroundTimer = new Timer(checkIsOnGroundTime);

        if (isMoveFromStart) {
            StartMove();
        }
    }

    void Update() {
        CheckIsOnGround();

        float dx = 0, dy = 0, dz = 0;

        if (isNeedMove) {
            if (movePath == default || movePath == null || movePath.Count == 0) {
                StopMove();
            }

            var curPos = transform.position;

            var targetPos = GetTargetPosition();

            if (isMoveX) {
                if (curPos.x < targetPos.x - bufferZone) {
                    dx = moveSpeed * Time.deltaTime;
                } else if (curPos.x > targetPos.x + bufferZone) {
                    dx = -moveSpeed * Time.deltaTime;
                }
            }

            if (isMoveY) {
                if (curPos.y < targetPos.y - bufferZone) {
                    dy = moveSpeed * Time.deltaTime;
                } else if (curPos.y > targetPos.y + bufferZone) {
                    dy = -moveSpeed * Time.deltaTime;
                }
            }

            if (isMoveZ) {
                if (curPos.z < targetPos.z - bufferZone) {
                    dz = moveSpeed * Time.deltaTime;
                } else if (curPos.z > targetPos.z + bufferZone) {
                    dz = -moveSpeed * Time.deltaTime;
                }
            }

            transform.position += new Vector3(dx, dy, dz);

            if (isRotateToTarget) {
                physicsSwitcher.RotateToPosition(targetPos);
            }

            // check is target transform reached
            float distToTargetTrans = Vector3.Distance(transform.position, targetPos);

            if (distToTargetTrans < collideDistance) {
                SwitchTargetMovePosToNext();

                if (targetPos == GetLastPosition()) {
                    /*if (isMoveLooped == false) {
                        StopMove();
                    }*/
                }
            }
        }
    }

    void CheckIsOnGround() {
        checkIsOnGroundTimer.Update(Time.deltaTime);

        if (checkIsOnGroundTimer.IsFinish()) {
            RaycastHit hit;

            if (Physics.Raycast(new Ray(checkGroundPosTrans.position, Vector3.down), out hit, 1.5f,
                LayerMask.GetMask("Floor"))) {
                isOnGround = true;
            } else {
                isOnGround = false;
            }

            checkIsOnGroundTimer.Reload();
        }
    }

    public void StartMove(Action _onComplete = null, bool _isLooped = true) {
        if (isNeedMove == false) {
            if (onCompleteAction != _onComplete) {
                onCompleteAction = _onComplete;
            }

            SwitchTargetMovePosToNext();

            isMoveLooped = _isLooped;
            isNeedMove = true;
        }
    }

    public void StopMove() {
        if (isNeedMove) {
            isNeedMove = false;
        }
    }

    void SwitchTargetMovePosToNext() {
        if (movePath == null) {
            Debug.Log("MovePath = null");

            return;
        }

        int newMoveIndex = GetCurrentMoveIndex();

        newMoveIndex += 1;

        if (newMoveIndex > movePath.Count - 1) {
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

    public void SetMovePath(List<Vector3> _movePath) {
        if (_movePath.Count == 0) {
            Debug.Log("No path found!");
            return;
        }

        if (movePath != _movePath) {
            movePath = _movePath;

            curMoveIndex = 0;
        }
    }

    public void SetIsRotateToTarget(bool _b) {
        isRotateToTarget = _b;
    }

    public Vector3 GetFirstPos() {
        return movePath[0];
    }

    public Vector3 GetTargetPosition() {
        var curMoveIndex = GetCurrentMoveIndex();

        if (movePath == default || movePath == null || movePath.Count == 0) {
            return Vector3.zero;
        }

        if (curMoveIndex > movePath.Count - 1) {
            curMoveIndex = movePath.Count - 1;
        }

        return movePath[curMoveIndex];
    }

    public Vector3 GetLastPosition() {
        if (movePath == null || movePath.Count == 0) {
            return Vector3.zero;
        }

        return movePath[movePath.Count - 1];
    }

    int GetCurrentMoveIndex() {
        return curMoveIndex;
    }

    public bool GetIsOnGround() {
        return isOnGround;
    }

    public bool GetIsMoving() {
        return isNeedMove;
    }

    public bool GetIsFinishAtLeastOnce() {
        return isFinishAtLeastOnce;
    }
}