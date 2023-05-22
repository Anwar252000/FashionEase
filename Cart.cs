using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROJECT
{
    public class Cart
    {

        public long ProductId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public long Quantity { get; set; }
        public string Image { get; set; }
        public float bill { get; set; }
      
        public long UserId { get; set; }

        public string Size { get; set; }

    }
}