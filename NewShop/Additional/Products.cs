using System;
using System.Collections.Generic;

namespace NewShop.Additional
{

    public class Products : IComparable<Products>
    {
        public string Name { get; }

        //public string Model { get; }

        public Products(string name)
        {
            Name = name;
            // Model = model;
        }

        public int CompareTo(Products other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Products p = (Products) obj;
            return (Name == p.Name);
        }
    }

}