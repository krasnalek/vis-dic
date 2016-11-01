using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

// ReSharper disable once CheckNamespace
namespace Wordnet
{
    public sealed partial class Form1 : Form
    {
        private string _firstFile;
        private XDocument _doc = new XDocument();
        private string _secondFile;
        private XDocument _doc2 = new XDocument();
        //private string SecondFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

    
        /// <summary>
        /// Default XDocument is used for processing the document, because LINQ is available.
        /// </summary>
        private void OpenFile()
        {
            lblFileName1.Text = string.Empty;
            bdInput.ShowDialog();
            _firstFile = bdInput.FileName;
            lblFileName1.Text = _firstFile;
            _doc = XDocument.Load(_firstFile);
        }

        private void OpenSecondFile()
        {
            lblFileName2.Text = string.Empty;
            bdInput.ShowDialog();
            _secondFile = bdInput.FileName;
            lblFileName2.Text = _secondFile;
            _doc2 = XDocument.Load(_secondFile);
        }
        private void btnSearchLiteral_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLiteral.Text))
                {
                    return;
                }
                listResults.Items.Add("Searching " + txtLiteral.Text + "...");
                PlwWordBasedOnNumber(txtLiteral.Text);
            }
            catch (Exception ex)
            {
                listResults.Items.Add(Messages.ErrorFound + ex.Message);
            }
        }

        private void PlwWordBasedOnNumber(string search)
        {
            try
            {
                string number = search;
                if (string.IsNullOrEmpty(_firstFile))
                {
                    MessageBox.Show(Messages.DirectoryNotChosen);
                }
                else
                {
                    var slowa = _doc.Descendants(Nodes.ID).FirstOrDefault(x => x.Value.Equals(number));

                    if (slowa?.Parent != null)
                    {
                        var slowo = slowa.Parent.Descendants(Nodes.Literal);
                        foreach (var element in slowo)
                        {
                            var sense = element?.Descendants(Nodes.Sense).FirstOrDefault()?.Value;
                            string result = "Word: " + element?.FirstNode;
                            if (!string.IsNullOrEmpty(sense))
                            {
                                result += " in sense: " + sense;
                            }
                            listResults.Items.Add(result);
                        }
                    }

                    listResults.Items.Add("__________________________________");
                }
                listResults.Items.Add("No such number found - please provide valid number.");
            }
            catch (Exception ex)
            {
                listResults.Items.Add(Messages.ErrorFound + ex.Message);
            }

        }

        private void btnSearchNumber_Click(object sender, EventArgs e)
        {
            try
            {
            
                if (!string.IsNullOrEmpty(txtLiteral.Text))
                {
                    listResults.Items.Add("Searching " + txtLiteral.Text + "...");
                    PlwNumberBasedOnWord(txtLiteral.Text);
                }
            }
            catch (Exception ex)
            {
                listResults.Items.Add(Messages.ErrorFound + ex.Message);
            }
        }

        private void PlwNumberBasedOnWord(string search)
        {
            try
            {
                string word = search;
                if (string.IsNullOrEmpty(_firstFile))
                {
                    MessageBox.Show(Messages.DirectoryNotChosen);
                }
                else
                {
                    var slowa = _doc.Descendants(Nodes.Synset)
                            .Where(c => c
                            .Descendants(Nodes.Literal).Any(r => r.FirstNode.ToString().Trim() == word));

                    var slowo = slowa.Descendants(Nodes.ID);
                    foreach (var result in slowo.Where(element => !element.IsEmpty).Select(element => "Number: " + element.FirstNode))
                    {
                        listResults.Items.Add(result);
                    }

                    listResults.Items.Add("__________________________________");
                }
            }
            catch (Exception ex)
            {
                listResults.Items.Add(Messages.ErrorFound + ex.Message);
            }
        }

        private void btnDirectory2_Click(object sender, EventArgs e)
        {
            OpenSecondFile();
        }

        //TODO: 1. Połączenie - jeden format
        //TODO: 2. Wizualizacjia do najwyższego node'a http://plwordnet.pwr.wroc.pl/wordnet/d0540a3e-0d16-11e6-bbf0-7a5d273e87eb
        //TODO: 3. Angielski - tłumaczenia

    }
}
