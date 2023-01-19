using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMIP
{
    class ImageInfo
    {
        private int id;
        private String name;
       // private String description;
        private byte[] imageData;
        System.Drawing.Image image1;


        public ImageInfo(String name)
        {
            this.id = id;
            this.name = name;
            //this.description= description;
            
           
            image1 = Image.FromFile(name);
            imageData = imageToByteArray(image1);
        }
        public ImageInfo(int id,String name)
        {
            this.id = id;
            this.name = name;
            image1 = Image.FromFile(name);
        }
        //public ImageInfo(String name)
        //{
            
        //    this.name = name;
        //    image1 = Image.FromFile(name);
        //}
        public ImageInfo(String name,String descripton,byte[] image)
        {
           
            this.name = name;
            //this.description= description;
            imageData = image;
            image1 = Image.FromFile(name);
        }
        public ImageInfo(String name,String descripton)
        {
           
            this.name = name;
          //  this.description= description;
           
        }

        public String getName()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        //public String getDescription()
        //{
        //    return description;
        //}
        //public void setDescription(String description)
        //{
        //    this.description = description;
        //}
        //public ImageInfo transform()
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            System.IO.MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
           
        }
        //public static System.Drawing.Image FromStream(System.IO.Stream stream, bool useEmbeddedColorManagement, bool validateImageData);
        //public ImageInfo byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    ImageInfo returnImage = ImageInfo.FromStream(ms);
        //    return returnImage;
        //}
    }
}
