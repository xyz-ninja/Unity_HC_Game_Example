using UnityEngine.UIElements;

namespace NavySpade.Modules.Utils.Serialization.Interfaces.Editor.UIElements
{
	internal class Tab : Toggle
	{
		public Tab(string text) : base()
		{
			base.text = text;
			RemoveFromClassList(Toggle.ussClassName);
			AddToClassList(ussClassName);
		}
	}
}