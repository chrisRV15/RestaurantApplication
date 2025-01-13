using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenApplicationAddition.Classes
{
    public class FoodOrder
    {
        public List<OrderItem> Order = new List<OrderItem>();

        public int TableNumber = -1;

        public FoodOrder(List<OrderItem> InOrder, int TableNum)
        {
            TableNumber = TableNum;
            Order = InOrder;
        }

        public override string ToString()
        {
            return $"Table: {TableNumber} ";
        }
    }
}
