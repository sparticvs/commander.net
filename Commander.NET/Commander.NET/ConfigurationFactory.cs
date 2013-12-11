using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;

namespace Commander.NET
{
    public static class ConfigurationFactory
    {
        public static Configuration createConfigurationFromFile(string fileName)
        {
            Configuration config = new Configuration();

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNode node = doc.SelectSingleNode("/switch");
            config.Name = node.Attributes["name"].Value;
            config.Serial = node.Attributes["serial"].Value;
            config.Hostname = node.Attributes["hostname"].Value;
            node = doc.SelectSingleNode("/switch/open_port");
            config.OpenPortName = node.Attributes["name"].Value;
            config.OpenPortColor = (Color)ColorConverter.ConvertFromString(node.Attributes["color"].Value);

            XmlNodeList nodes = doc.SelectNodes("/switch/vlans");
            foreach (XmlNode n in nodes)
            {
                config.Vlans.Add(VlanFactory.createVlan(n.Attributes["name"].Value,
                                                        n.Attributes["id"].Value,
                                                        n.Attributes["color"].Value));
            }

            nodes = doc.SelectNodes("/switch/ports");
            foreach (XmlNode n in nodes)
            {
                n.Attributes["id"];
                n.Attributes["vlans"];
                n.InnerText;
            }
            


            return null;
        }
    }
}
