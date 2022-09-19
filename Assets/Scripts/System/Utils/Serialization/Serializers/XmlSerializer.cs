using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.Serializers
{
    /// <summary>
    /// Xml Serializer for saving/restoring data.
    /// </summary>
    [Serializable]
    [AddTypeMenu("XML")]
    public class XmlSerializer : ISerializer
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
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                serializer.Serialize(stream, obj);
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
            var result = default(T);
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return result;
        }
    }
}