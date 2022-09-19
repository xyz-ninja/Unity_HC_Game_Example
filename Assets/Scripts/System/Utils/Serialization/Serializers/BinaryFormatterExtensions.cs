using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NavySpade.Modules.Utils.Serialization.Surrogates;

namespace NavySpade.Modules.Utils.Serialization.Serializers
{
    public static class BinaryFormatterExtensions
    {
        public static void AddUnitySurrogates(this BinaryFormatter formatter)
        {
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddAllUnitySurrogate();
            formatter.SurrogateSelector = surrogateSelector;
        }
    }
}