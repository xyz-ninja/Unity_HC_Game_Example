﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYellowpaper.Samples
{
	public class InteractableTest : MonoBehaviour
	{
		[RequireInterface(typeof(IInteractable))]
		public MonoBehaviour ReferenceWithAttribute;
		public InterfaceReference<IInteractable> InterfaceReference;
		public InterfaceReference<IInteractable, ScriptableObject> InterfaceReferenceWithConstraint;

		[RequireInterface(typeof(IInteractable))]
		public MonoBehaviour[] ReferenceWithAttributeArray;
		[RequireInterface(typeof(IInteractable))]
		public List<UnityEngine.Object> ReferenceWithAttributeList;

		void Awake()
		{
			(ReferenceWithAttribute as IInteractable).Interact();
			InterfaceReference.Value.Interact();
			InterfaceReferenceWithConstraint.Value.Interact();
		}
	}
}
