using System.Runtime.Serialization;
using UnityEngine;
using Object = System.Object;

namespace NavySpade.Modules.Utils.Serialization.Surrogates
{
    internal sealed class Vector4SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(Object obj,
            SerializationInfo info, StreamingContext context)
        {
            Vector4 v4 = (Vector4)obj;
            info.AddValue("x", v4.x);
            info.AddValue("y", v4.y);
            info.AddValue("z", v4.z);
            info.AddValue("w", v4.w);
        }

        public Object SetObjectData(Object obj,
            SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            Vector4 v4 = (Vector4)obj;
            v4.x = (float)info.GetValue("x", typeof(float));
            v4.y = (float)info.GetValue("y", typeof(float));
            v4.z = (float)info.GetValue("z", typeof(float));
            v4.w = (float)info.GetValue("w", typeof(float));
            obj = v4;
            return obj;
        }
    }
}