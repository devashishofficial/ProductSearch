using System;

namespace ProductSearch
{
    public class ProductDetails
    {
        public uint StoreNumber { get; set; }
        public ulong UPC { get; set; }
        public bool IsWeightOrPriceRequired { get; set; }
        public bool IsQuantityAllowed { get; set; }
        public bool IsPriceRequired { get; set; }
        public bool IsNotForSale { get; set; }
        public bool IsTax1Rate { get; set; }
        public bool IsTax2Rate { get; set; }
        public bool IsFoodStamp { get; set; }
        public bool IsEMPoints { get; set; }
        public uint ItemType { get; set; }
        public uint PricingMethod { get; set; }
        public uint Department { get; set; }
        public uint FamilyNumbers { get; set; }
        public string MPGroup { get; set; }
        public uint SaleQuantity { get; set; }
        public int Price { get; set; }
        public uint LinkTo { get; set; }
        public string ItemName { get; set; }
        public uint RestrictedSale { get; set; }
        public bool IsWIC { get; set; }
        public bool IsItemAdded { get; set; }
        public uint Tare { get; set; }
        public bool IsFoodPerks { get; set; }
        public bool IsQHP { get; set; }
        public bool IsRx { get; set; }
        public ulong LargeLinkedTo { get; set; }
        public bool IsProductRecall { get; set; }
    }
}