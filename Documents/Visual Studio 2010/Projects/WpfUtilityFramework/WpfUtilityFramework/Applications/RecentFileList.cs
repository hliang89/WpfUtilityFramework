using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfUtilityFramework.Foundation;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.ComponentModel;

namespace WpfUtilityFramework.Applications
{
    public sealed class RecentFileList:IXmlSerializable
    {
        private readonly ObservableCollection<RecentFile> recentFiles;
        private readonly ReadOnlyObservableCollection<RecentFile> readonlyRecentFiles;
        private int maxFilesNumber = 8;

        public RecentFileList()
        {
            recentFiles = new ObservableCollection<RecentFile>();
            readonlyRecentFiles = new ReadOnlyObservableCollection<RecentFile>(recentFiles);
        }

        public ObservableCollection<RecentFile> RecentFiles
        {
            get { return recentFiles; }
        }

        public ReadOnlyObservableCollection<RecentFile> ReadonlyRecentFiles
        {
            get { return readonlyRecentFiles; }
        }

        public int MaxFilesNumber
        {
            get { return maxFilesNumber; }
            set
            {
                if (maxFilesNumber != value)
                {
                    if (maxFilesNumber < 0) {throw new ArgumentException("The value must be greater or equal to 1");}

                    maxFilesNumber = value;

                    if (recentFiles.Count - maxFilesNumber >= 1)
                    {
                        removeRange(maxFilesNumber, recentFiles.Count - maxFilesNumber);
                    }
                }
            }
        }

        public void Load(IEnumerable<RecentFile> recentFiles)
        {
            if (recentFiles == null) { throw new ArgumentException("recentFiles"); }

            Clear();
            AddRange(recentFiles.Take(maxFilesNumber));
        }

        public void AddFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) { throw new ArgumentException("fileName"); }

            RecentFile recentFile = recentFiles.FirstOrDefault(c => c.FilePath == filePath);

            if (recentFile != null)
            {
                int oldIndex = recentFiles.IndexOf(recentFile);
                int newIndex = recentFile.IsPinned ? 0 : PinCount;

                if (oldIndex != newIndex)
                {
                    recentFiles.Move(oldIndex, newIndex);
                }
            }
            else
            {
                if (PinCount < maxFilesNumber)
                {
                    if (recentFiles.Count >= maxFilesNumber)
                    {
                        RemoveAt(recentFiles.Count - 1);
                    }
                    Insert(PinCount, new RecentFile(filePath));
                }
            }
        }

        XmlSchema IXmlSerializable.GetSchema() { return null; }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader==null){throw new ArgumentException("reader");}

            reader.ReadToDescendant("RecentFile");
            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "RecentFile")
            {
                RecentFile recentFile = new RecentFile();
                ((IXmlSerializable)recentFile).ReadXml(reader);
                Add(recentFile);
            }
            if (!reader.IsEmptyElement) { reader.ReadEndElement(); }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (writer == null) { throw new ArgumentException("writer"); }

            foreach (RecentFile recentFile in recentFiles)
            {
                writer.WriteStartElement("RecentFiles");
                ((IXmlSerializable)recentFile).WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        private int PinCount { get { return recentFiles.Count(c => c.IsPinned == true); } }

        private void Insert(int index, RecentFile file)
        {
            file.PropertyChanged += RecentFilePropertyChanged;
            recentFiles.Insert(index, file);
        }

        private void Add(RecentFile recentFile)
        {
            recentFile.PropertyChanged += RecentFilePropertyChanged;
            recentFiles.Add(recentFile);
        }

        private void AddRange(IEnumerable<RecentFile> range)
        {
            foreach (RecentFile file in range)
            {
                Add(file);
            }
        }

        private void RemoveAt(int index)
        {
            recentFiles[index].PropertyChanged -= RecentFilePropertyChanged;
            recentFiles.RemoveAt(index);
        }

        private void removeRange(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }

        private void Clear()
        {
            foreach (RecentFile file in recentFiles)
            {
                file.PropertyChanged -= RecentFilePropertyChanged;
            }
            recentFiles.Clear();
        }

        private void RecentFilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsPinned")
            {
                RecentFile recentFile = (RecentFile)sender;

                int oldIndex = recentFiles.IndexOf(recentFile);

                if (recentFile.IsPinned)
                {
                    recentFiles.Move(oldIndex, 0);
                }
                else
                {
                    int newIndex = PinCount;
                    if (oldIndex != newIndex)
                    {
                        recentFiles.Move(oldIndex, newIndex);
                    }
                }
            }
        }
    }

}
