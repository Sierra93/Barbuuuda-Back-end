using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using PayPalCheckoutSdk.Core;
using PayPalHttp;

namespace Barbuuuda.Commerces.Data
{
    /// <summary>
    /// Класс конфигурации PayPal SDK.
    /// </summary>
    public class ClientConfigure
    {
        /// <summary>
        /// Client ID и Secret.
        /// </summary>
        /// <returns>Client ID и Secret.</returns>
        public static PayPalEnvironment Environment()
        {
            return new SandboxEnvironment("AaT69mnC2Wl5xQ4i2vk67EscPVhnE6yNFzzwTFr8V93AVddY14Lhj29ZRyECJ_ReduhNyd6gX_AqzgR4", "EHmOOAJT93nlqMps5jKFfi8GiSD97VJlHfJSoK5p0iPc3JSqA-45O6mx4VVhM-L-ry9q-RGYzNX9aia1");
        }

        public static HttpClient Client()
        {
            return new PayPalHttpClient(Environment());
        }

        public static HttpClient Client(string refreshToken)
        {
            return new PayPalHttpClient(Environment(), refreshToken);
        }

        /// <summary>
        /// Метод сериализует объект.
        /// </summary>
        /// <param name="serializableObject">Объект, который нужно сериализовать.</param>
        /// <returns>Сериализованный объект в строке.</returns>
        public static string ObjectToJSONString(object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);

            return sr.ReadToEnd();
        }
    }
}
