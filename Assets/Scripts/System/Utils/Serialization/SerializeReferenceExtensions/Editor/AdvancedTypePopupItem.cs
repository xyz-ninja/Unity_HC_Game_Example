using System;
using UnityEditor.IMGUI.Controls;

namespace NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Editor
{
    public class AdvancedTypePopupItem : AdvancedDropdownItem
    {
        public Type Type { get; }

        public AdvancedTypePopupItem(Type type, string name) : base(name)
        {
            Type = type;
        }
    }
}