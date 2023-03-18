using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADApp.inventory
{
    class InventoryDAO
    {
        public String Name { get; set; }
        public int Count { get; set; }
        public double WeightOne { get; set; }
        public String Category { get; set; }
        public double TotalWeight { get; set; }
        public String Discription{ get; set; }

        public InventoryDAO() { }

        public InventoryDAO(String name, int count, double weightOne, String category, String discription)
        {
            this.Name = name;
            this.Count = count;
            this.WeightOne = weightOne;
            this.Category = category;
            TotalWeight = count * weightOne;
            this.Discription = discription;
        }


    }
}
