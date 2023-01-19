using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lib;

namespace ProiectMIP
{
    class Images: lib.ISortable
    {
        public List<ImageInfo> imageInfos { get; set; }
        //public int An { get; set; }

        public void Sort()
        {
            imageInfos.Sort();
        }
    }
}

