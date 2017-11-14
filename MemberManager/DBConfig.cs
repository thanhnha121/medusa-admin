using System.Linq;
using System.Xml.Linq;

namespace Medusa.Business
{
    public class MedusaTestConfiguration
    {
        public static XDocument ConfigurationDoc;
        public string ConString { get; set; }

        public string GetConnString(string fileName)
        {
            ConfigurationDoc = XDocument.Load(fileName);
            XElement appElement = ConfigurationDoc.Descendants("database").FirstOrDefault();
            if (appElement != null)
            {
                return appElement.Element("connectionString").Value;
            }
            return "";
        }
    }
}
