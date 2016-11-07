using System;
using System.Collections.Generic;
using System.IO;
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
        private static List<string> notFoundWords = new List<string>();
        private static List<string> notFoundNumbers = new List<string>();
        private static int _id;
        public static void CombineFiles(XDocument aDoc, XDocument bDoc)
        {
            try
            {
                notFoundWords.Clear();
                notFoundNumbers.Clear();
                var xml = new DATA();
                var synsets = new List<Synset>();
               
                var sensePlwn = string.Empty;

                foreach (var element in aDoc.Descendants(Nodes.Synset))
                {
                    _id++;
                    string value = "ID_" + _id;
                    var irlList = new List<ILR>();
                    var literals = new List<Literal>();
                    var plwnIDs = new List<PlWN>();
                    var idPns = new List<PN>();
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
                        var plWnNumbers = SearchNumberBasedOnWord(bDoc, searchingWord);
                        var polNumbers = SearchNumberBasedOnWord(aDoc, searchingWord);
                        foreach (var a in polNumbers.Where(a => idPns.All(x => x.Value != a)))
                        {
                            idPns.Add(new PN() {Value = a});
                        }
                        if (plWnNumbers.Count > 0)
                        {
                            foreach (var b in plWnNumbers.Where(b => plwnIDs.All(y => y.Value != b)))
                            {
                                plwnIDs.Add(new PlWN() {Value = b});
                            }
                            
                            List<XElement> theSameWord =
                                bDoc.Descendants(Nodes.Literal).Where(x => x.FirstNode.ToString() == searchingWord).ToList();

                            sensePlwn = theSameWord.Descendants(Nodes.Sense)?.First().Value;

                            var xElementsIlrPlwn = theSameWord.Ancestors(Nodes.Synset)?.Descendants(Nodes.ILR);
                            if (xElementsIlrPlwn != null)
                            {
                                foreach (var ilr in xElementsIlrPlwn)
                                {
                                    var forMerged = new Synset();
                                    if (synsets.Count > 0)
                                    {
                                        forMerged =
                                            synsets.Find(
                                                item =>
                                                    item?.ID?.PlWN?.ToString().Contains(ilr.FirstNode.ToString()) ??
                                                    false);

                                            //Where(s => s?.ID?.PlWN?.Any(x => x.Value == ilr.FirstNode.ToString() ?? true))
                                    }
                                    irlList.Add(new ILR()
                                    {
                                        source = Types.Source_PLWNID,
                                        type = ilr.Descendants(Nodes.ILRType).First().Value,
                                        merged = forMerged?.ID?.Value,
                                        Value = ilr.FirstNode.ToString()
                                    });
                                 }
                                
                                //irlList.AddRange(xElementsIlrPlwn.Select(ilr => new ILR()
                                //{
                                //    source = Types.Source_PLWNID,
                                //    type = ilr.Descendants(Nodes.ILRType).First().Value,
                                //    merged = synsets.Count > 0 ? synsets.SingleOrDefault(s => s?.ID?.PlWN?.ToString().Contains(ilr.FirstNode.ToString()) ?? false)?.ID?.Value : null,
                                //    Value = ilr.FirstNode.ToString()
                                //}));
                            }
                        }
                        string sensePn = literal.Attribute("sense")?.Value;
                        literals.Add(new Literal() { plwnsense = sensePlwn, plwnsenseSpecified = !string.IsNullOrEmpty(sensePlwn), pnsense = sensePn, Value = searchingWord });
                    }
                    synsets.Add(
                        new Synset
                        {
                            ID = new ID()
                            {
                                Value = value,
                                PlWN = plwnIDs.ToArray(),
                                PN = idPns.ToArray()
                            },
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
                    File.WriteAllLines(@"C:\t\notFoundNumbers.txt", notFoundNumbers);
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
            try
            {
                var serializer = new XmlSerializer(xmlData.GetType());
                using (var writer = XmlWriter.Create(name))
                {
                    serializer.Serialize(writer, xmlData);
                }

                MessageBox.Show("Your file has been created: " + name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serialization cannot be done. Error: " + ex);
            }
        }
        public static HashSet<string> SearchNumberBasedOnWord(XContainer doc, string search)
        {
            try
            {
                var result = new HashSet<string>();
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
                    result.UnionWith(slowo.Where(element => !element.IsEmpty).Select(element => element.FirstNode.ToString()));
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
