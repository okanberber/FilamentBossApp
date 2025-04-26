using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Products
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public  string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ProductName { get; set; }
        public int Piece { get; set; }
        public decimal Price { get; set; }
        public string Diameter { get; set; }
        public string Color { get; set; }
        public byte image { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
