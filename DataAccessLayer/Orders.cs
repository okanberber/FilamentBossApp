using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Orders
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public int Piece { get; set; }
        public string Diameter { get; set; }
        public string Color { get; set; }
        public string Weigth { get; set; }
    }
}
