using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Entities.Order
{
    public class OrderItem : BaseEntity<int>
    {
        #region Constructor

        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        #endregion
        #region Property

        public ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        #endregion



    }


}
