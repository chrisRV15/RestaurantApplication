using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenApplicationAddition.Classes
{
    public class OrderItem
    { 
        public string ItemName { get; private set; }

        protected bool IsVoid = false;

        public OrderItem(string Name)
        {
            ItemName = Name;

        }
        public void VoidItem()
        {
            IsVoid = true;
        }

        public bool IsItemVoid()
        {
            return IsVoid;
        }

        public override string ToString()
        {
            return ItemName;
        }
    }
}
