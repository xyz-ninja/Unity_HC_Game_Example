using System.Runtime.Serialization;
using UnityEngine;
using Object = System.Object;

namespace NavySpade.Modules.Utils.Serialization.Surrogates
{
    internal sealed class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(Object obj,
            SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3)obj;
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
        }

        public Object SetObjectData(Object obj,
            SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3)obj;
            v3.x = (float)info.GetValue("x", typeof(float));
            v3.y = (float)info.GetValue("y", typeof(float));
            v3.z = (float)info.GetValue("z", typeof(float));
            obj = v3;
            return obj;
        }
    }
}