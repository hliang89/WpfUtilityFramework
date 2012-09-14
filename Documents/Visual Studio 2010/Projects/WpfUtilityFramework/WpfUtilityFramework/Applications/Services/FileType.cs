using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications.Services
{
    public class FileType
    {
        private readonly string description;
        private readonly string fileExtension;

        public FileType(string description, string fileExtension)
        {
            if (string.IsNullOrEmpty(description)) { throw new ArgumentException("The file description must not be null or empty"); }
            if (string.IsNullOrEmpty(fileExtension)) { throw new ArgumentException("The file extension must not be null or empty"); }
            if (fileExtension[0] != '.') { throw new ArgumentException("The file extension must begin with a '.' character"); }

            this.description = description;
            this.fileExtension = fileExtension;
        }

        public string Description { get { return description; } }
        public string FileExtension { get { return fileExtension; } }
    }
}
