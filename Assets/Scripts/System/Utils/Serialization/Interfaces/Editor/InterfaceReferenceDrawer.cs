using System;
using System.Reflection;
using AYellowpaper;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.Interfaces.Editor
{
	[CustomPropertyDrawer(typeof(InterfaceReference<>))]
	[CustomPropertyDrawer(typeof(InterfaceReference<,>))]
	public class InterfaceReferenceDrawer : PropertyDrawer
	{
		private const string FieldName = "_underlyingValue";

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var prop = property.FindPropertyRelative(FieldName);
			InterfaceReferenceUtility.OnGUI(position, prop, label, GetArguments(fieldInfo));
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var prop = property.FindPropertyRelative(FieldName);
			return InterfaceReferenceUtility.GetPropertyHeight(prop, label, GetArguments(fieldInfo));
		}

		private static void GetObjectAndInterfaceType(Type fieldType, out Type objectType, out Type interfaceType)
		{
			var genericType = fieldType.GetGenericTypeDefinition();
			if (genericType == typeof(InterfaceReference<,>))
			{
				var types = fieldType.GetGenericArguments();
				interfaceType = types[0];
				objectType = types[1];
				return;
			}
			objectType = typeof(UnityEngine.Object);
			interfaceType = fieldType.GetGenericArguments()[0];
		}

		private static InterfaceObjectArguments GetArguments(FieldInfo fieldInfo)
		{
			GetObjectAndInterfaceType(fieldInfo.FieldType, out var objectType, out var interfaceType);
			return new InterfaceObjectArguments(objectType, interfaceType);
		}
	}
}
