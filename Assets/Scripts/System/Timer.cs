using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple timer
/// USAGE EXAMPLE
/// at Start()
/// Timer someTimer = new Timer(1.0f); // 1 секунда
/// at Update()
/// someTimer.Update(Time.deltaTime);
/// if (someTimer.IsFinish()) {
///     DO SOMETHING;
///     someTimer.Reload();
/// }
/// </summary>
public class Timer
{
	public float initTime;
	public float currentTime;

	public Timer(float _initTime) {
		initTime = _initTime;
		Reload();
	}

	public void ChangeInitTime(float _newInitTime) {
		initTime = _newInitTime;
		Reload();
	}

	public void Update(float _deltaTime) {
		if (currentTime > 0) currentTime -= _deltaTime;
	}

	public void Reload() {
		currentTime = initTime;
	}

	public bool IsFinish() {
		return currentTime <= 0;
	}
}
