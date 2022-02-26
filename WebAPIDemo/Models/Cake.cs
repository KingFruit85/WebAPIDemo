using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.Models
{
    public class Cake
    {
        public string CakeType { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int StoreAvalibility { get; set; }

        public Cake(string cakeType, int cost, string description, int weight, int storeAvalibility)
        {
            CakeType = cakeType;
            Cost = cost;
            Description = description;
            Weight = weight;
            StoreAvalibility = storeAvalibility;
        }
    }
}
