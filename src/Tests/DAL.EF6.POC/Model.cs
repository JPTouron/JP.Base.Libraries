using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF6.POC
{
    class Model
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime  Date { get; set; }
        public IList<Item> Items { get; set; }
    }


    class Item
    {

        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public Model Model { get; set; }
    }
}
