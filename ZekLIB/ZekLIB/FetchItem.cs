using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZekLIB
{
    internal class FetchItem
    {
        private static FetchItem instance;
        private static FetchItem nama_file;
        private static FetchItem lastpageviewed;
        public string Data { get; set; }
        public string NamaFile { get; set; }
        public int LastPageViewed { get; set; }

        private static FetchItem myurl;

        private static FetchItem query;

        private static FetchItem addeddate_;
        private static FetchItem title_;
        private static FetchItem author_;
        private static FetchItem description_;
        private static FetchItem publication_date;
        private static FetchItem language;
        private static FetchItem source;
        private static FetchItem link_download;
        private static FetchItem img_url;

        public string Myurl { get; set; }
        public string Query { get; set; }
        public string Addeddate { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Publication_date { get; set; }
        public string Language_ { get; set; }
        public string Sources { get; set; }
        public string Link_Download { get; set; }
        public string Image_url { get; set; }


        private FetchItem()
        {
            
        }
        //this for move from MenuForm to BookPreview Form
        public static FetchItem GetInstance()
        {
            if (instance == null)
            {
                instance = new FetchItem();
            }
            return instance;
        }

       
    }
   

}
