using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using Microsoft.Win32;
using WpfUtilityFramework.Applications.Services;
using System.IO;

namespace WpfUtilityFramework.Presentation.Services
{
    [Export(typeof(IFileDialogService))]
    public class FileDialogService:IFileDialogService
    {
        public FileDialogResult ShowOpenFileDialog(object owner, IEnumerable<FileType> fileTypes, FileType defaultFileType, string defaultFileName)
        {
            if (fileTypes == null) { throw new ArgumentNullException("fileTypes"); }
            if (!fileTypes.Any()) { throw new ArgumentException("The fileTypes collection must contain at least one item."); }

            OpenFileDialog dialog = new OpenFileDialog();

            return ShowFileDialog(owner, dialog, fileTypes, defaultFileType, defaultFileName);
        }

        public FileDialogResult ShowSaveFileDialog(object owner, IEnumerable<FileType> fileTypes, FileType defaultFileType, string defaultFileName)
        {
            if (fileTypes == null) { throw new ArgumentNullException("fileTypes"); }
            if (!fileTypes.Any()) { throw new ArgumentException("The fileTypes collection must contain at least one item."); }

            SaveFileDialog dialog = new SaveFileDialog();

            return ShowFileDialog(owner, dialog, fileTypes, defaultFileType, defaultFileName);
        }

        private static FileDialogResult ShowFileDialog(object owner, FileDialog dialog, IEnumerable<FileType> fileTypes,
            FileType defaultFileType, string defaultFileName)
        {
            int filterIndex = fileTypes.ToList().IndexOf(defaultFileType);
            if (filterIndex >= 0) { dialog.FilterIndex = filterIndex + 1; }
            if (!string.IsNullOrEmpty(defaultFileName))
            {
                dialog.FileName = Path.GetFileName(defaultFileName);
                string directory = Path.GetDirectoryName(defaultFileName);
                if (!string.IsNullOrEmpty(directory))
                {
                    dialog.InitialDirectory = directory;
                }
            }

            dialog.Filter = CreateFilter(fileTypes);
            if (dialog.ShowDialog(owner as Window) == true)
            {
                filterIndex = dialog.FilterIndex - 1;
                if (filterIndex >= 0 && filterIndex < fileTypes.Count())
                {
                    defaultFileType = fileTypes.ElementAt(filterIndex);
                }
                else
                {
                    defaultFileType = null;
                }
                return new FileDialogResult(dialog.FileName, defaultFileType);
            }
            else
            {
                return new FileDialogResult();
            }
        }

        private static string CreateFilter(IEnumerable<FileType> fileTypes)
        {
            string filter = "";
            foreach (FileType fileType in fileTypes)
            {
                if (!String.IsNullOrEmpty(filter)) { filter += "|"; }
                filter += fileType.Description + "|*" + fileType.FileExtension;
            }
            return filter;
        }

    }
}
