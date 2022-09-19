using System;
using AYellowpaper;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.Interfaces.Editor
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceAttributeDrawer : PropertyDrawer
    {
        private RequireInterfaceAttribute RequireInterfaceAttribute => (RequireInterfaceAttribute)attribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var args = new InterfaceObjectArguments(GetTypeOrElementType(fieldInfo.FieldType),
                RequireInterfaceAttribute.InterfaceType);
            InterfaceReferenceUtility.OnGUI(position, property, label, args);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var args = new InterfaceObjectArguments(GetTypeOrElementType(fieldInfo.FieldType),
                RequireInterfaceAttribute.InterfaceType);
            return InterfaceReferenceUtility.GetPropertyHeight(property, label, args);
        }

        /// <summary>
        /// returns the type, or if it's a container, returns the type of the element.
        /// </summary>
        private Type GetTypeOrElementType(Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            return type.IsGenericType ? type.GetGenericArguments()[0] : type;
        }
    }
}