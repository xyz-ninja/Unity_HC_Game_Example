using System;
using System.Collections.Generic;
using System.Reflection;
using SerializationUtils.SR;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Editor.Obsolete
{
    public class SRAction
    {
        public readonly SerializedProperty Property;
        public readonly string Command;

        public SRAction(SerializedProperty p, string c)
        {
            Property = p;
            Command = c;
        }
    }

    [CustomPropertyDrawer(typeof(SRAttribute), false)]
    public class SRDrawer : PropertyDrawer
    {
        #if UNITY_EDITOR
        private static string EmptyTypeName => "(empty)";

        private SRAttribute _attr;
        private SerializedProperty _array;

        private readonly Dictionary<SerializedProperty, int>
            _elementIndexes = new Dictionary<SerializedProperty, int>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int index;
            if (_array == null)
            {
                _array = GetParentArray(property, out index);
            }
            else
            {
                index = GetArrayIndex(property);
            }

            _elementIndexes[property] = index;

            _attr ??= attribute as SRAttribute;

            var typeName = GetTypeName(property.managedReferenceFullTypename,
                SRAttribute.GetTypeByName(property.managedReferenceFullTypename));
            var typeNameContent = new GUIContent(typeName + (_array != null ? ("[" + index + "]") : ""));

            var buttonWidth = 10f + GUI.skin.button.CalcSize(typeNameContent).x;
            var buttonHeight = EditorGUI.GetPropertyHeight(property, label, false);

            EditorGUI.BeginChangeCheck();

            var previousColor = GUI.backgroundColor;
            ValidateType(typeName);

            var buttonRect = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth, buttonHeight);

            if (EditorGUI.DropdownButton(buttonRect, typeNameContent, FocusType.Passive))
            {
                ShowMenu(property, true);
                Event.current.Use();
            }

            GUI.backgroundColor = previousColor;

            EditorGUI.PropertyField(position, property, label, true);

            if (EditorGUI.EndChangeCheck() && _attr != null)
            {
                // TODO: _attr.OnChange(_element.managedReferenceValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private void ShowMenu(SerializedProperty property, bool applyArray)
        {
            GenericMenu context = new GenericMenu();

            if (_array != null && applyArray)
            {
                var index = _elementIndexes[property];
                context.AddItem(new GUIContent("Delete"), false, OnMenuItemClick, new SRAction(property, "Delete"));
                context.AddItem(new GUIContent("Insert"), false, OnMenuItemClick, new SRAction(property, "Insert"));
                context.AddItem(new GUIContent("Add"), false, OnMenuItemClick, new SRAction(property, "Add"));
                context.AddSeparator("");
            }

            if (_attr.Types == null)
                _attr.SetTypeByName(property.managedReferenceFieldTypename);

            var types = _attr.Types;
            if (types != null)
            {
                context.AddItem(new GUIContent("Erase"), false, OnMenuItemClick, new SRAction(property, "Erase"));
                context.AddSeparator("");

                foreach (var type in types)
                {
                    var name = string.IsNullOrEmpty(type.CustomName) ? type.Path : type.CustomName;
                    context.AddItem(new GUIContent(name), false, OnMenuItemClick,
                        new SRAction(property, type.Path));
                }
            }

            context.ShowAsContext();
        }

        public void OnMenuItemClick(object userData)
        {
            var action = (SRAction)userData;
            var cmd = action.Command;
            var element = action.Property;
            var index = -1;

            if (_array != null)
                index = _elementIndexes[element];

            element.serializedObject.UpdateIfRequiredOrScript();

            if (_array != null && index >= 0 && index < _array.arraySize)
            {
                _array.serializedObject.UpdateIfRequiredOrScript();

                if (cmd == "Delete")
                {
                    Undo.RegisterCompleteObjectUndo(_array.serializedObject.targetObject, "Delete element at " + index);
                    Undo.FlushUndoRecordObjects();

                    element.managedReferenceValue = null;

                    _array.DeleteArrayElementAtIndex(index);
                    _array.serializedObject.ApplyModifiedProperties();

                    _array = null;

                    return;
                }

                if (cmd == "Insert")
                {
                    Undo.RegisterCompleteObjectUndo(_array.serializedObject.targetObject, "Insert element at " + index);
                    Undo.FlushUndoRecordObjects();

                    _array.InsertArrayElementAtIndex(index);
                    _array.serializedObject.ApplyModifiedProperties();

                    var newElement = _array.GetArrayElementAtIndex(index);
                    newElement.managedReferenceValue = null;
                    newElement.serializedObject.ApplyModifiedProperties();

                    return;
                }

                if (cmd == "Add")
                {
                    Undo.RegisterCompleteObjectUndo(_array.serializedObject.targetObject,
                        "Add element at " + (index + 1));
                    Undo.FlushUndoRecordObjects();

                    _array.InsertArrayElementAtIndex(index + 1);
                    _array.serializedObject.ApplyModifiedProperties();

                    var newElement = _array.GetArrayElementAtIndex(index + 1);
                    newElement.managedReferenceValue = null;
                    newElement.serializedObject.ApplyModifiedProperties();

                    return;
                }
            }

            if (cmd == "Erase")
            {
                Undo.RegisterCompleteObjectUndo(element.serializedObject.targetObject, "Erase element");
                Undo.FlushUndoRecordObjects();

                element.managedReferenceValue = null;
                element.serializedObject.ApplyModifiedProperties();

                return;
            }

            var typeInfo = _attr.TypeInfoByPath(cmd);
            if (typeInfo == null)
            {
                Debug.LogError("Type '" + cmd + "' not found.");
                return;
            }

            Undo.RegisterCompleteObjectUndo(element.serializedObject.targetObject,
                "Create instance of " + typeInfo.Type);
            Undo.FlushUndoRecordObjects();

            if (!typeInfo.Type.IsSubclassOf(typeof(Object)))
            {
                var instance = Activator.CreateInstance(typeInfo.Type);
                _attr.OnCreate(instance);

                element.managedReferenceValue = instance;
            }

            element.serializedObject.ApplyModifiedProperties();
        }

        private static SerializedProperty GetParentArray(SerializedProperty element, out int index)
        {
            index = GetArrayIndex(element);
            if (index < 0)
                return null;

            string propertyPath = element.propertyPath;

            string[] fullPathSplit = propertyPath.Split('.');

            string pathToArray = string.Empty;
            for (var i = 0; i < fullPathSplit.Length - 2; i++)
            {
                if (i < fullPathSplit.Length - 3)
                {
                    pathToArray = string.Concat(pathToArray, fullPathSplit[i], ".");
                }
                else
                {
                    pathToArray = string.Concat(pathToArray, fullPathSplit[i]);
                }
            }

            var targetObject = element.serializedObject.targetObject;
            SerializedObject serializedTargetObject = new SerializedObject(targetObject);

            return serializedTargetObject.FindProperty(pathToArray);
        }

        private static int GetArrayIndex(SerializedProperty element)
        {
            var propertyPath = element.propertyPath;
            if (!propertyPath.Contains(".Array.data[") || !propertyPath.EndsWith("]"))
                return -1;

            var start = propertyPath.LastIndexOf("[");
            var str = propertyPath.Substring(start + 1, propertyPath.Length - start - 2);
            int.TryParse(str, out var index);

            return index;
        }

        private static string GetTypeName(string typeName, MemberInfo type)
        {
            if (string.IsNullOrEmpty(typeName))
                return EmptyTypeName;

            var attribute = type.GetCustomAttribute<CustomSerializeReferenceName>();

            if (attribute != null)
                return attribute.Name;

            var index = typeName.LastIndexOf(' ');
            if (index >= 0)
                return typeName.Substring(index + 1);

            index = typeName.LastIndexOf('.');
            if (index >= 0)
                return typeName.Substring(index + 1);

            return typeName;
        }

        private static void ValidateType(string typeName)
        {
            var color = typeName == EmptyTypeName ? Color.red : Color.green;
            GUI.backgroundColor = color;
        }
        #endif
    }
}