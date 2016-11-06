using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wordnet.Dto;

namespace Wordnet
{
    public static class Engine
    {
        private static List<string> notFoundWords = new List<string>();
        private static List<string> notFoundNumbers = new List<string>();
        public static void CombineFiles(XDocument aDoc, XDocument bDoc)
        {
            try
            {
                notFoundWords.Clear();
                notFoundNumbers.Clear();
                var xml = new DATA();
                var synsets = new List<Synset>();
                var idPlwn = string.Empty;
                var sensePlwn = string.Empty;

                foreach (var element in aDoc.Descendants(Nodes.Synset))
                {
                    string idPn = element?.Descendants(Nodes.ID).First().Value;
                    string value = "...";
                    var irlList = new List<ILR>();
                    var literals = new List<Literal>();
                    var xElementsIlr = element?.Descendants(Nodes.ILR);
                    if (xElementsIlr != null)
                    {
                        irlList.AddRange(xElementsIlr.Select(ilr => new ILR()
                        {
                            source = Types.Source_PNID,
                            type = ilr.Attribute("type")?.Value,
                            Value = ilr.FirstNode.ToString()
                        }));
                    }                    
                    var xElementsLiteral = element?.Descendants(Nodes.Literal);
                    if (xElementsLiteral == null) continue;
                    foreach (var literal in xElementsLiteral)
                    {
                        var searchingWord = literal.FirstNode.ToString();
                        List<string> numbers = SearchNumberBasedOnWord(bDoc, searchingWord);
                        if (numbers.Count > 0)
                        {
                            List<XElement> theSameWord =
                                bDoc.Descendants(Nodes.Literal).Where(x => x.FirstNode.ToString() == searchingWord).ToList();
                            idPlwn = numbers.FirstOrDefault();
                            sensePlwn = theSameWord.Descendants(Nodes.Sense)?.First().Value;
                            //TODO: Adding ILRNodes from PlWordnet
                            var xElementsIlrPlwn = theSameWord.Ancestors(Nodes.Synset)?.Descendants(Nodes.ILR);
                            if (xElementsIlrPlwn != null)
                            {
                                irlList.AddRange(xElementsIlrPlwn.Select(ilr => new ILR()
                                {
                                    source = Types.Source_PLWNID,
                                    type = ilr.Descendants(Nodes.ILRType).First().Value,
                                    Value = ilr.FirstNode.ToString()
                                }));
                            }
                        }
                        string sensePn = literal.Attribute("sense")?.Value;
                        literals.Add(new Literal() { plwnsense = sensePlwn, plwnsenseSpecified = !string.IsNullOrEmpty(sensePlwn), pnsense = sensePn, Value = searchingWord });
                    }
                    synsets.Add(
                        new Synset
                        {
                            ID = new ID() { plwn = idPlwn, pn = idPn, Value = value },
                            ILR = irlList.ToArray(),
                            SYNONYM = literals.ToArray()
                        });
                }
                xml.SYNSET = synsets.ToArray();
                CreateFile(xml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.ErrorFound + ex.Message);
            }
            finally
            {
                try
                {

                    File.WriteAllLines(@"C:\t\notFoundWords.txt", notFoundWords);
                    MessageBox.Show("Not found words: " + notFoundWords.Count);
                    File.WriteAllLines(@"C:\t\notFoundNubers.txt", notFoundNumbers);
                    MessageBox.Show("Not found numbers: " + notFoundNumbers.Count);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot store data in files.");
                }
            }
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
                {
                    notFoundWords.Add(search);
                }
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
