using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Editor
{
    /// <summary>
    /// A type popup with a fuzzy finder.
    /// </summary>
    public class AdvancedTypePopup : AdvancedDropdown
    {
        private static readonly float HeaderHeight = EditorGUIUtility.singleLineHeight * 2f;

        private Type[] _types;

        public event Action<AdvancedTypePopupItem> OnItemSelected;
        
        public static void AddTo(AdvancedDropdownItem root, IEnumerable<Type> types)
        {
            var itemCount = 0;

            // Add null item.
            var nullItem = new AdvancedTypePopupItem(null, TypeMenuUtility.NullDisplayName)
            {
                id = itemCount++
            };
            root.AddChild(nullItem);

            // Add type items.
            foreach (Type type in types.OrderByType())
            {
                var splitTypePath = TypeMenuUtility.GetSplitTypePath(type);
                if (splitTypePath.Length == 0)
                {
                    continue;
                }

                var parent = root;

                // Add namespace items.
                for (var k = 0; (splitTypePath.Length - 1) > k; k++)
                {
                    var foundItem = GetItem(parent, splitTypePath[k]);
                    if (foundItem != null)
                    {
                        parent = foundItem;
                    }
                    else
                    {
                        var newItem = new AdvancedDropdownItem(splitTypePath[k])
                        {
                            id = itemCount++,
                        };
                        parent.AddChild(newItem);
                        parent = newItem;
                    }
                }

                // Add type item.
                var item = new AdvancedTypePopupItem(type,
                    ObjectNames.NicifyVariableName(splitTypePath[splitTypePath.Length - 1]))
                {
                    id = itemCount++
                };
                parent.AddChild(item);
            }
        }

        private static AdvancedDropdownItem GetItem(AdvancedDropdownItem parent, string name)
        {
            return parent.children.FirstOrDefault(item => item.name == name);
        }

        public AdvancedTypePopup(IEnumerable<Type> types, int maxLineCount, AdvancedDropdownState state) : base(state)
        {
            SetTypes(types);
            minimumSize = new Vector2(minimumSize.x, EditorGUIUtility.singleLineHeight * maxLineCount + HeaderHeight);
        }

        public void SetTypes(IEnumerable<Type> types)
        {
            _types = types.ToArray();
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Select Type");
            AddTo(root, _types);
            
            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);
            if (item is AdvancedTypePopupItem typePopupItem)
            {
                OnItemSelected?.Invoke(typePopupItem);
            }
        }
    }
}