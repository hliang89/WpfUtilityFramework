using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfUtilityFramework.Foundation;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WpfUtilityFramework.Applications
{
    public class RecentFile:Model, IXmlSerializable
    {
        private string path;
        private bool isPinned;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public RecentFile() { }

        public RecentFile(string path)
        {
            if (string.IsNullOrEmpty(path)) { throw new ArgumentException("path"); }

            this.path = path;
        }

        public bool IsPinned
        {
            get { return isPinned; }
            set
            {
                if (isPinned != value)
                {
                    isPinned = value;
                    RaisePropertyChanged("IsPinned");
                }
            }
        }

        public string FilePath
        {
            get { return path; }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader == null) { throw new ArgumentException("reader"); }

            IsPinned = bool.Parse(reader.GetAttribute("IsPinned"));
            path = reader.ReadElementContentAsString();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (writer == null) { throw new ArgumentException("writer"); }

            writer.WriteAttributeString("IsPinned", isPinned.ToString());
            writer.WriteString(path);
        }
    }
}
