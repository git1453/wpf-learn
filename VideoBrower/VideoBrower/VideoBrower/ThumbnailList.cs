using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoBrower
{
    public class ThumbnailList:ObservableCollection<string>
    {
        String _folderName;

        public string FolderName
        {
            get
            {
                return _folderName;
            }

            set
            {
                _folderName = value;

                // Now fill in the collection of 
                // file names:
                this.Clear();
                foreach (string fileName in
                         Directory.GetFiles(this.FolderName, "*.jpg"))
                {
                    this.Add(fileName);
                }
            }
        }
    }
}
