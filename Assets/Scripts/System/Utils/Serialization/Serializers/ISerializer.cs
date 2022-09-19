using System.IO;
using System.Text;

namespace NavySpade.Modules.Utils.Serialization.Serializers
{
    /// <summary>
    /// Interface for all serializers for saving/restoring data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize the specified object to stream with encoding.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="stream">Stream.</param>
        /// <param name="encoding">Encoding.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void Serialize<T>(T obj, Stream stream, Encoding encoding);

        /// <summary>
        /// Deserialize the specified object from stream using the encoding.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="encoding">Encoding.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T Deserialize<T>(Stream stream, Encoding encoding);
    }
}