using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
	
	// TODO: здесь должна быть проверка какую именно камеру вернуть
	public CustomCamera GetCurrentCustomCamera() {
		
		var level = Game.Instance.World.CurrentLevel;

		return level.MainCamera;
	}
}
