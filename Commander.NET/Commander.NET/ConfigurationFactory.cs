﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;

namespace Commander.NET
{
    public static class ConfigurationFactory
    {
        public static Configuration createDefaultConfiguration()
        {
            Configuration config = new Configuration();


            config.Vlans.Add(VlanFactory.createVlan("WAN", "0", "#FFC000"));
            config.Vlans.Add(VlanFactory.createVlan("Default", "1", "#92D050"));
            config.Vlans.Add(VlanFactory.createVlan("Internal", "2", "#00B0F0"));
            config.Vlans.Add(VlanFactory.createVlan("External", "3", "brown"));
            config.Vlans.Add(VlanFactory.createVlan("DMZ", "4", "#FF0000"));
            config.Vlans.Add(VlanFactory.createVlan("Management", "254", "#E26B0A"));


            return config;
        }

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

            XmlNodeList nodes = doc.SelectNodes("/switch/vlans/vlan");
            foreach (XmlNode n in nodes)
            {
                config.Vlans.Add(VlanFactory.createVlan(n.Attributes["name"].Value,
                                                        n.Attributes["id"].Value,
                                                        n.Attributes["color"].Value));
            }

            nodes = doc.SelectNodes("/switch/ports/port");
            foreach (XmlNode n in nodes)
            {
                Port p = PortFactory.createPort(n.Attributes["id"].Value, n.InnerText);
                string[] vlan_ids = n.Attributes["vlans"].Value.Split(',');

                foreach (string vid in vlan_ids)
                {
                    if (vid == string.Empty)
                    {
                        continue;
                    }
                    int vlan_id = int.Parse(vid);
                    p.Vlans.Add(config.Vlans.Find(x => x.Id == vlan_id));
                }

                config.Ports.Add(p.Id, p);
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
            foreach (int portId in config.Ports.Keys)
            {
                XmlNode portNode = doc.CreateElement("port");

                attr = doc.CreateAttribute("id");
                attr.Value = portId.ToString();
                portNode.Attributes.Append(attr);

                attr = doc.CreateAttribute("vlans");
                List<string> vlans = new List<string>();
                foreach (Vlan v in config.Ports[portId].Vlans)
                {
                    vlans.Add(v.Id.ToString());
                }
                attr.Value = string.Join(",", vlans);
                portNode.Attributes.Append(attr);

                portNode.InnerText = config.Ports[portId].Details;

                portsNode.AppendChild(portNode);
            }

            rootNode.AppendChild(portsNode);

            doc.AppendChild(rootNode);

            doc.Save(fileName);
        }
    }
}
