using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Editor
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        private struct TypePopupCache
        {
            public AdvancedTypePopup TypePopup { get; }
            public AdvancedDropdownState State { get; }

            public TypePopupCache(AdvancedTypePopup typePopup, AdvancedDropdownState state)
            {
                TypePopup = typePopup;
                State = state;
            }
        }

        private const int MaxTypePopupLineCount = 13;
        private static readonly Type UnityObjectType = typeof(Object);
        private static readonly GUIContent NullDisplayName = new GUIContent(TypeMenuUtility.NullDisplayName);

        private static readonly GUIContent IsNotManagedReferenceLabel =
            new GUIContent("The property type is not manage reference.");

        private readonly Dictionary<string, TypePopupCache> _typePopups = new Dictionary<string, TypePopupCache>();
        private readonly Dictionary<string, GUIContent> _typeNameCaches = new Dictionary<string, GUIContent>();

        private SerializedProperty _targetProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            //EditorGUI.BeginChangeCheck();
            
            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                var popup = GetTypePopup(property);

                // Draw the subclass selector popup.
                var popupPosition = new Rect(position);
                popupPosition.width -= EditorGUIUtility.labelWidth;
                popupPosition.x += EditorGUIUtility.labelWidth;
                popupPosition.height = EditorGUIUtility.singleLineHeight;

                if (EditorGUI.DropdownButton(popupPosition, GetTypeName(property), FocusType.Keyboard))
                {
                    _targetProperty = property;
                    popup.TypePopup.Show(popupPosition);
                }

                // Draw the managed reference property.
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.LabelField(position, label, IsNotManagedReferenceLabel);
            }

            EditorGUI.EndProperty();

            // if (EditorGUI.EndChangeCheck())
            // {
            //     property.serializedObject.ApplyModifiedProperties();
            // }
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return true;
        }

        private TypePopupCache GetTypePopup(SerializedProperty property)
        {
            if (_typePopups.TryGetValue(property.managedReferenceFieldTypename, out var result))
            {
                return result;
            }

            var state = new AdvancedDropdownState();

            var baseType = property.GetManagedReferenceFieldType();
            var popup = new AdvancedTypePopup(
                TypeCache.GetTypesDerivedFrom(baseType).Where(p =>
                    (p.IsPublic || p.IsNestedPublic) &&
                    p.IsAbstract == false &&
                    p.IsGenericType == false &&
                    UnityObjectType.IsAssignableFrom(p) == false &&
                    Attribute.IsDefined(p, typeof(SerializableAttribute))
                ),
                MaxTypePopupLineCount,
                state
            );

            popup.OnItemSelected += item =>
            {
                var type = item.Type;
                var obj = _targetProperty.SetManagedReference(type);
                _targetProperty.isExpanded = (obj != null);
                _targetProperty.serializedObject.ApplyModifiedProperties();
            };

            _typePopups.Add(property.managedReferenceFieldTypename, new TypePopupCache(popup, state));

            return result;
        }

        private GUIContent GetTypeName(SerializedProperty property)
        {
            if (string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                return NullDisplayName;
            }

            if (_typeNameCaches.TryGetValue(property.managedReferenceFullTypename, out GUIContent cachedTypeName))
            {
                return cachedTypeName;
            }

            var type = property.GetManagedReferenceType();

            if (TryGetTypeNameWithoutPath(type, out var typeName) == false)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    typeName = ObjectNames.NicifyVariableName(type.Name);
                }
            }

            var result = new GUIContent(typeName);
            _typeNameCaches.Add(property.managedReferenceFullTypename, result);

            return result;
        }

        private static bool TryGetTypeNameWithoutPath(Type type, out string typeName)
        {
            var typeMenu = TypeMenuUtility.GetAttribute(type);
            if (typeMenu == null)
            {
                typeName = string.Empty;
                return false;
            }

            typeName = typeMenu.GetTypeNameWithoutPath();
            if (string.IsNullOrWhiteSpace(typeName) == false)
            {
                typeName = ObjectNames.NicifyVariableName(typeName);
                return true;
            }

            return false;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}