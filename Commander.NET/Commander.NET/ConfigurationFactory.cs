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
                Port p = PortBuilder.buildPort(n.Attributes["id"].Value, n.InnerText);
                string[] vlan_ids = n.Attributes["vlans"].Value.Split(',');

                foreach (string vid in vlan_ids)
                {
                    int vlan_id = int.Parse(vid);
                    p.Vlans.Add(config.Vlans.Find(x => x.Id == vlan_id));
                }

                config.Ports.Add(p);
            }

            return config;
        }

        public static void saveConfigurationToFile(string fileName, Configuration config)
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("switch");

            XmlAttribute attr = doc.CreateAttribute("name");
            attr.Value = config.Name;
            rootNode.Attributes.Append(attr);

            attr = doc.CreateAttribute("serial");
            attr.Value = config.Serial;
            rootNode.Attributes.Append(attr);

            attr = doc.CreateAttribute("hostname");
            attr.Value = config.Hostname;
            rootNode.Attributes.Append(attr);

            XmlNode opNode = doc.CreateElement("open_port");

            attr = doc.CreateAttribute("name");
            attr.Value = config.OpenPortName;
            opNode.Attributes.Append(attr);

            attr = doc.CreateAttribute("color");
            attr.Value = config.OpenPortColor.ToString();
            opNode.Attributes.Append(attr);

            rootNode.AppendChild(opNode);

            XmlNode vlansNode = doc.CreateElement("vlans");

            foreach (Vlan vlan in config.Vlans)
            {
                XmlNode vlanNode = doc.CreateElement("vlan");

                attr = doc.CreateAttribute("name");
                attr.Value = vlan.Name;
                vlanNode.Attributes.Append(attr);

                attr = doc.CreateAttribute("id");
                attr.Value = vlan.Id.ToString();
                vlanNode.Attributes.Append(attr);

                attr = doc.CreateAttribute("color");
                attr.Value = vlan.Color.ToString();
                vlanNode.Attributes.Append(attr);

                vlansNode.AppendChild(vlanNode);
            }

            rootNode.AppendChild(vlansNode);

            XmlNode portsNode = doc.CreateElement("ports");
            // TODO: Handle rows, cols, and groups for UI
            foreach (Port port in config.Ports)
            {
                XmlNode portNode = doc.CreateElement("port");

                attr = doc.CreateAttribute("id");
                attr.Value = port.Id.ToString();
                portNode.Attributes.Append(attr);

                attr = doc.CreateAttribute("vlans");
                List<string> vlans = new List<string>();
                foreach (Vlan v in port.Vlans)
                {
                    vlans.Add(v.Id.ToString());
                }
                attr.Value = string.Join(",", vlans);
                portNode.Attributes.Append(attr);

                portNode.InnerText = port.Details;

                portsNode.AppendChild(portNode);
            }

            rootNode.AppendChild(portsNode);

            doc.AppendChild(rootNode);

            doc.Save(fileName);
        }
    }
}
