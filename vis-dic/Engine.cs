using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Wordnet.Dto;

namespace Wordnet
{
    public static class Engine
    {
        public static void CombineFiles(XDocument aDoc, XDocument bDoc)
        {
            var xml = new DATA();
            foreach (var element in aDoc.Descendants(Nodes.Synset))
            {
                //TODO: tworzenie wspólnego obiektu
            }

            var list = (from word in aDoc.Descendants(Nodes.Synset)
                        select new DATA
                        {
                            SYNSET = new Synset[]
                            {
                                //TODO: tworzenie wspólnego obiektu
                            },

                        }).ToList();

            CreateFile(xml);
        }

        /// <summary>
        /// Serializer of our XML
        /// </summary>
        /// <param name="xmlData">XML to be serialized</param>
        /// <param name="name">path of the file we wanna store XML</param>
        private static void CreateFile(DATA xmlData, string name = Paths.DefaultOutputPath)
        {
            var serializer = new XmlSerializer(xmlData.GetType());
            using (var writer = XmlWriter.Create(name))
            {
                serializer.Serialize(writer, xmlData);
            }
        }
        public static List<string> SearchNumberBasedOnWord(XContainer doc, string search)
        {
            try
            {
                var result = new List<string>();
                string word = search;
                if (string.IsNullOrEmpty(search))
                {
                    MessageBox.Show(Messages.NoNumberFound);
                }
                else
                {
                    var slowa = doc.Descendants(Nodes.Synset)
                            .Where(c => c
                            .Descendants(Nodes.Literal).Any(r => r.FirstNode.ToString().Trim() == word));

                    var slowo = slowa.Descendants(Nodes.ID);
                    result.AddRange(slowo.Where(element => !element.IsEmpty).Select(element => element.FirstNode.ToString()));
                }
                if (result.Count == 0)
                { MessageBox.Show(Messages.NoNumberFound); }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.ErrorFound + ex.Message);
                return null;
            }
        }
        public static Dictionary<string, string> SearchWordBasedOnNumber(XContainer doc, string search)
        {
            try
            {
                var result = new Dictionary<string, string>();
                string number = search;
                if (string.IsNullOrEmpty(search))
                {
                    MessageBox.Show(Messages.InvalidWord);
                }
                else
                {
                    var slowa = doc.Descendants(Nodes.ID).FirstOrDefault(x => x.Value.Equals(number));

                    if (slowa?.Parent != null)
                    {
                        var slowo = slowa.Parent.Descendants(Nodes.Literal);
                        foreach (var element in slowo)
                        {
                            var sense = element?.Descendants(Nodes.Sense).FirstOrDefault()?.Value;
                            var word = element?.FirstNode.ToString();
                            if (word != null)
                            {
                                result.Add(word, !string.IsNullOrEmpty(sense) ? sense : "");

                            }
                        }
                    }
                }
                if (result.Count == 0)
                { MessageBox.Show(Messages.NoNumberFound); }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.ErrorFound + ex.Message);
                return null;
            }

        }
    }
}
