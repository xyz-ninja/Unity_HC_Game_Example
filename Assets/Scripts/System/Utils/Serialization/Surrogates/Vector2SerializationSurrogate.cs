using System.Runtime.Serialization;
using UnityEngine;
using Object = System.Object;

namespace NavySpade.Modules.Utils.Serialization.Surrogates
{
    internal sealed class Vector2SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(Object obj,
            SerializationInfo info, StreamingContext context)
        {
            Vector2 v2 = (Vector2)obj;
            info.AddValue("x", v2.x);
            info.AddValue("y", v2.y);
        }

        public Object SetObjectData(Object obj,
            SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            Vector2 v2 = (Vector2)obj;
            v2.x = (float)info.GetValue("x", typeof(float));
            v2.y = (float)info.GetValue("y", typeof(float));
            obj = v2;
            return obj;
        }
    }
}