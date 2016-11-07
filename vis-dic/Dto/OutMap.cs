// ReSharper disable InconsistentNaming

using System.Diagnostics.Contracts;

namespace Wordnet.Dto
{
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public class DATA
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Nodes.Synset)]
        public Synset[] SYNSET { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class Synset
    {
        /// <remarks/>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ID ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem(Nodes.Literal, IsNullable = false)]
        public Literal[] SYNONYM { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Nodes.ILR)]
        public ILR[] ILR { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class ID
    {
        public PlWN[] PlWN { get; set; }

        public PN[] PN { get; set; }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlAttribute()]
        //public string plwn { get; set; }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlAttribute()]
        //public string pn { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value { get; set; }
    }

    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class PlWN
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value { get; set; }
    }

    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class PN
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class Literal
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string plwnsense { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool plwnsenseSpecified { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string pnsense { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class ILR
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string type { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string source { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string merged { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value { get; set; }
    }
}