using DADApp.inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DADApp.forms
{
    class XMLService
    {
        public static ArrayList ParseXMLToInventDAO()
        {
            ArrayList fullInventDAOList = new ArrayList();
            Boolean isWithoutProfile = false;
            XmlDocument xDoc = new XmlDocument();
            if (!File.Exists(DADConstants.XML_FILE_INVENT))
            {
                //Создать файл, если нет
                File.Create(DADConstants.XML_FILE_INVENT).Close();
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode xmlN = xmlDoc.CreateElement(DADConstants.INVENTORY_ROOT_ROOT_ATTR);
                xmlDoc.AppendChild(xmlN);
                xmlDoc.Save(DADConstants.XML_FILE_INVENT);
            }

            Dictionary<String, int> coinsMap = new Dictionary<string, int>();
            ArrayList listDAO = new ArrayList();
            int strenght = 0;
            String size = DADConstants.SIZE_NORMAL_DEFAULT;

            xDoc.Load(DADConstants.XML_FILE_INVENT);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            String profileName = "Задайте имя";
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {

                    if (xnode.Name.Equals(DADConstants.INVENTORY_ROOT_PROFILE_ATTR))
                    {
                        XmlNode profileAttr = xnode.Attributes.GetNamedItem(DADConstants.INVENTORY_ROOT_PROFILE_NAME_ATTR);
                        profileName = profileAttr?.Value;
                        coinsMap = new Dictionary<string, int>();
                        listDAO = new ArrayList();
                        foreach (XmlNode profilenode in xnode.ChildNodes)
                        {
                            if (profilenode.Name.Equals(DADConstants.INVENTORY_ROOT_ATTR))
                            {

                                InventoryDAO inventoryDAO = new InventoryDAO();
                                String Name = String.Empty;
                                int Count = 0;
                                double WeightOne = 0D;
                                String Category = String.Empty;
                                String Discription = String.Empty;

                                // получаем атрибут name
                                XmlNode attr = profilenode.Attributes.GetNamedItem(DADConstants.NAME_ATTR);
                                Name = attr?.Value;

                                // обходим все дочерние узлы элемента user
                                foreach (XmlNode childnode in profilenode.ChildNodes)
                                {
                                    switch (childnode.Name)
                                    {
                                        case DADConstants.COUNT_ATTR:
                                            Count = Convert.ToInt32(childnode.InnerText);
                                            break;
                                        case DADConstants.WEIGHT_ATTR:
                                            WeightOne = Convert.ToDouble(childnode.InnerText);
                                            break;
                                        case DADConstants.CATEGORY_ATTR:
                                            Category = childnode.InnerText;
                                            break;
                                        case DADConstants.DISKRIPTION_ATTR:
                                            Discription = childnode.InnerText;
                                            break;
                                    }
                                }
                                listDAO.Add(new InventoryDAO(Name, Count, WeightOne, Category, Discription));
                            }
                            if (profilenode.Name.Equals(DADConstants.COINS_ROOT))
                            {
                                foreach (XmlNode childnode in profilenode)
                                {
                                    coinsMap.Add(childnode.Name, (int.Parse(childnode.InnerText)));
                                }
                            }

                            if (profilenode.Name.Equals(DADConstants.WEIGHT_ROOT))
                            {
                                foreach (XmlNode childnode in profilenode)
                                {
                                    switch (childnode.Name)
                                    {
                                        case DADConstants.STRENGHT_ATTR:
                                            strenght = int.Parse(childnode.InnerText);
                                            break;
                                        case DADConstants.SIZE_ATTR:
                                            size = childnode.InnerText;
                                            break;
                                    }
                                }
                            }
                        }
                        fullInventDAOList.Add(new FullInventDAO(listDAO, coinsMap, strenght, size, profileName));
                    } else
                    {
                        isWithoutProfile = true;
                        if (xnode.Name.Equals(DADConstants.INVENTORY_ROOT_ATTR))
                        {

                            InventoryDAO inventoryDAO = new InventoryDAO();
                            String Name = String.Empty;
                            int Count = 0;
                            double WeightOne = 0D;
                            String Category = String.Empty;
                            String Discription = String.Empty;

                            // получаем атрибут name
                            XmlNode attr = xnode.Attributes.GetNamedItem(DADConstants.NAME_ATTR);
                            Name = attr?.Value;

                            // обходим все дочерние узлы элемента user
                            foreach (XmlNode childnode in xnode.ChildNodes)
                            {
                                switch (childnode.Name)
                                {
                                    case DADConstants.COUNT_ATTR:
                                        Count = Convert.ToInt32(childnode.InnerText);
                                        break;
                                    case DADConstants.WEIGHT_ATTR:
                                        WeightOne = Convert.ToDouble(childnode.InnerText);
                                        break;
                                    case DADConstants.CATEGORY_ATTR:
                                        Category = childnode.InnerText;
                                        break;
                                    case DADConstants.DISKRIPTION_ATTR:
                                        Discription = childnode.InnerText;
                                        break;
                                }
                            }
                            listDAO.Add(new InventoryDAO(Name, Count, WeightOne, Category, Discription));
                        }
                        if (xnode.Name.Equals(DADConstants.COINS_ROOT))
                        {
                            foreach (XmlNode childnode in xnode)
                            {
                                coinsMap.Add(childnode.Name, (int.Parse(childnode.InnerText)));
                            }
                        }
                        if (xnode.Name.Equals(DADConstants.WEIGHT_ROOT))
                        {
                            foreach (XmlNode childnode in xnode)
                            {
                                switch (childnode.Name)
                                {
                                    case DADConstants.STRENGHT_ATTR:
                                        strenght = int.Parse(childnode.InnerText);
                                        break;
                                    case DADConstants.SIZE_ATTR:
                                        size = childnode.InnerText;
                                        break;
                                }
                            }
                        }

                    }
                }
                if (isWithoutProfile)
                {
                    fullInventDAOList.Add(new FullInventDAO(listDAO, coinsMap, strenght, size, "Дайте имя"));
                }
            }
            return fullInventDAOList;
        }

        public static void ParseInventDAOToXML(ArrayList fullInventDAOList)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };

            using (var writer = XmlWriter.Create(DADConstants.XML_FILE_INVENT, settings))
            {
                writer.WriteStartElement(DADConstants.INVENTORY_ROOT_ROOT_ATTR);

                foreach (FullInventDAO fullInventDAO in fullInventDAOList)
                {
                    writer.WriteStartElement(DADConstants.INVENTORY_ROOT_PROFILE_ATTR);
                    writer.WriteAttributeString(DADConstants.INVENTORY_ROOT_PROFILE_NAME_ATTR, fullInventDAO.profileName);

                    foreach (InventoryDAO inv in fullInventDAO.invent)
                    {
                        writer.WriteStartElement(DADConstants.INVENTORY_ROOT_ATTR);
                        writer.WriteAttributeString(DADConstants.NAME_ATTR, inv.Name);
                        writer.WriteElementString(DADConstants.COUNT_ATTR, inv.Count.ToString());
                        writer.WriteElementString(DADConstants.WEIGHT_ATTR, inv.WeightOne.ToString());
                        writer.WriteElementString(DADConstants.CATEGORY_ATTR, inv.Category);
                        writer.WriteElementString(DADConstants.DISKRIPTION_ATTR, inv.Discription);
                        writer.WriteEndElement(); // end InventoryDAO
                    }

                    writer.WriteStartElement(DADConstants.COINS_ROOT);

                    int platinaValue = 0;
                    int goldValue = 0;
                    int electrumValue = 0;
                    int silverValue = 0;
                    int copperValue = 0;

                    foreach (KeyValuePair<string, int> a in fullInventDAO.coins)
                    {
                        switch (a.Key)
                        {
                            case DADConstants.PLATINA_ATTR:
                                platinaValue = a.Value;
                                break;

                            case DADConstants.GOLD_ATTR:
                                goldValue = a.Value;
                                break;

                            case DADConstants.ELECTRUM_ATTR:
                                electrumValue = a.Value;
                                break;

                            case DADConstants.SILVER_ATTR:
                                silverValue = a.Value;
                                break;

                            case DADConstants.COPPER_ATTR:
                                copperValue = a.Value;
                                break;
                        }
                    }

                    writer.WriteElementString(DADConstants.PLATINA_ATTR, platinaValue.ToString());
                    writer.WriteElementString(DADConstants.GOLD_ATTR, goldValue.ToString());
                    writer.WriteElementString(DADConstants.ELECTRUM_ATTR, electrumValue.ToString());
                    writer.WriteElementString(DADConstants.SILVER_ATTR, silverValue.ToString());
                    writer.WriteElementString(DADConstants.COPPER_ATTR, copperValue.ToString());

                    writer.WriteEndElement(); // end Coins

                    writer.WriteStartElement(DADConstants.WEIGHT_ROOT);
                    writer.WriteElementString(DADConstants.STRENGHT_ATTR, fullInventDAO.strenghtValue.ToString());
                    writer.WriteElementString(DADConstants.SIZE_ATTR, fullInventDAO.size);
                    writer.WriteEndElement(); // end Weight

                    writer.WriteEndElement(); // end FullInventDAO
                }

                writer.WriteEndElement(); // end Root
            }
        }

        public static Dictionary<String, int> getEmptyCoins()
        {
            return new Dictionary<string, int>()
            {
                [DADConstants.PLATINA_ATTR] = 0,
                [DADConstants.GOLD_ATTR] = 0,
                [DADConstants.ELECTRUM_ATTR] = 0,
                [DADConstants.SILVER_ATTR] = 0,
                [DADConstants.COPPER_ATTR] = 0
            };
        }

        public static Dictionary<int, ArrayList> ParseXMLToBestDTO()
        {
            Dictionary<int, ArrayList> allMobs = new Dictionary<int, ArrayList>();
            for (int i = 1; i <= 20; i++)
            {
                allMobs.Add(i, new ArrayList());
            }
            return allMobs;
        }
    }
}
