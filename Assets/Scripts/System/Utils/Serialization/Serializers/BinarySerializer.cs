using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.Serializers
{
    /// <summary>
    /// Binary Serializer for saving/restoring data.
    /// </summary>
    [Serializable]
    [AddTypeMenu("Binary")]
    public class BinarySerializer : ISerializer
    {
        /// <summary>
        /// Serialize the specified object to stream with encoding.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="stream">Stream.</param>
        /// <param name="encoding">Encoding.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void Serialize<T>(T obj, Stream stream, Encoding encoding)
        {
            try
            {
                var formatter = new BinaryFormatter();
                formatter.AddUnitySurrogates();
                formatter.Serialize(stream, obj);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Deserialize the specified object from stream using the encoding.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="encoding">Encoding.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Deserialize<T>(Stream stream, Encoding encoding)
        {
            T result = default(T);
            try
            {
                var formatter = new BinaryFormatter();
                formatter.AddUnitySurrogates();
                result = (T)formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return result;
        }
    }
}