using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications.Services
{
    public class FileDialogResult
    {
        private readonly string fileName;
        private readonly FileType selectedFileType;

        public FileDialogResult()
            : this(null, null)
        {
        }

        public FileDialogResult(string fileName, FileType fileType)
        {
            this.fileName = fileName;
            this.selectedFileType = fileType;
        }

        public string FileName { get {return fileName;} }
        public FileType SelectedFileType { get { return selectedFileType; } }
    }
}
