using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.Serializers
{
    /// <summary>
    /// Json Serializer for saving/restoring data.
    /// </summary>
    [Serializable]
    [AddTypeMenu("Json")]
    public class JsonSerializer : ISerializer
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
                var writer = new StreamWriter(stream, encoding);
                writer.Write(JsonUtility.ToJson(obj));
                writer.Dispose();
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
                var reader = new StreamReader(stream, encoding);
                result = JsonUtility.FromJson<T>(reader.ReadToEnd());
                reader.Dispose();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return result;
        }
    }
}