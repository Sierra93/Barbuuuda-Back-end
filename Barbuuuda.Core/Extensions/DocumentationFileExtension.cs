using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Barbuuuda.Core.Extensions
{
    /// <summary>
    /// Класс расширения динамической записи xml-файла при запуске приложения.
    /// </summary>
    public static class DocumentationFileExtension
    {
        /// <summary>
        /// Метод запишет путь для xml-файла swagger при запуске приложения.
        /// </summary>
        public static void OnApplicationStarted()
        {
            string xmlFile = "Barbuuuda.csproj";
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xmlFile);
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);
            XDocument xml = XDocument.Load(xmlPath);
            XElement node = xml.Descendants("DocumentationFile").FirstOrDefault();
            node.SetValue(xmlPath.Replace("Barbuuuda.csproj", "Barbuuuda.xml"));
            xml.Save(xmlPath);
        }
    }
}
