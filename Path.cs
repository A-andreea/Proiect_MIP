using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMIP
{
    class Path
    {

        private String path;
        private List<ImageInfo> imageInfo;

        public Path()
        {
            path = @"C:\Users\andre\OneDrive\Desktop\MIP\ProiectMIP\images";
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles(" *.jpg"); 
            string str = "";
            foreach (FileInfo i in Files)
            {
                str=i.Name;
                
                
            }
        }
        public Path(String pathDet)
        {
            this.path = pathDet;
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles(" *.jpg"); //Getting Text files
            string str = "";
            foreach (FileInfo i in Files)
            {
                str = i.Name;
            }
        }

        public String getPath()
        {
            return path;
        }
        public void setDescription(String path)
        {
            this.path = path;
        }


    }

}
