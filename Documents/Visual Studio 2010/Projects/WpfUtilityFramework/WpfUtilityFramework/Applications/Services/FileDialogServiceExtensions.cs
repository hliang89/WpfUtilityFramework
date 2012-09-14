using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications.Services
{
    public static class FileDialogServiceExtensions
    {
        public static FileDialogResult ShowOpenDialog(this IFileDialogService service, FileType fileType)
        {
            if (service == null) { throw new ArgumentException("service"); }
            if (fileType == null) { throw new ArgumentException("fileType"); }

            return service.ShowOpenFileDialog(null, new FileType[] { fileType }, fileType, null);
        }

        public static FileDialogResult ShowOpenDialog(this IFileDialogService service, object owner, FileType fileType)
        {
            if (service == null) { throw new ArgumentException("service"); }
            if (fileType == null) { throw new ArgumentException("fileType"); }
            if (owner == null) { throw new ArgumentException("owner"); }

            return service.ShowOpenFileDialog(owner, new FileType[] { fileType }, fileType, null);
        }

        public static FileDialogResult ShowSaveDialog(this IFileDialogService service, FileType fileType)
        {
            if (service == null) { throw new ArgumentException("service"); }
            if (fileType == null) { throw new ArgumentException("fileType"); }

            return service.ShowSaveFileDialog(null, new FileType[] { fileType }, fileType, null);
        }   
    }
}
