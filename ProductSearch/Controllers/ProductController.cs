using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetProduct(ulong uPC)
        {
            try
            {
                string file = @"Files\3305.dat";

                if (System.IO.File.Exists(file))
                {
                    string[] lines = System.IO.File.ReadAllLines(file);

                    List<ProductDetails> productDetails = new();
                    foreach (string ln in lines)
                    {
                        ProductDetails prodDetails = new();
                        prodDetails.StoreNumber = uint.Parse(ln.Substring(0, 4));
                        prodDetails.UPC = ulong.Parse(ln.Substring(4, 14));
                        prodDetails.IsWeightOrPriceRequired = ln.Substring(18, 1) == "Y";
                        prodDetails.IsQuantityAllowed = ln.Substring(19, 1) == "Y";
                        prodDetails.IsPriceRequired = ln.Substring(20, 1) == "Y";
                        prodDetails.IsNotForSale = ln.Substring(21, 1) == "Y";
                        prodDetails.IsTax1Rate = ln.Substring(22, 1) == "Y";
                        prodDetails.IsTax2Rate = ln.Substring(23, 1) == "Y";
                        prodDetails.IsFoodStamp = ln.Substring(24, 1) == "Y";
                        prodDetails.IsEMPoints = ln.Substring(25, 1) == "Y";
                        prodDetails.ItemType = uint.Parse(ln.Substring(26, 1));
                        prodDetails.PricingMethod = uint.Parse(ln.Substring(27, 1));
                        prodDetails.Department = uint.Parse(ln.Substring(28, 3));
                        prodDetails.FamilyNumbers = uint.Parse(ln.Substring(31, 6));
                        prodDetails.MPGroup = ln.Substring(37, 2);
                        prodDetails.SaleQuantity = uint.Parse(ln.Substring(39, 2));
                        prodDetails.Price = int.Parse(ln.Substring(41, 10));
                        prodDetails.LinkTo = uint.Parse(ln.Substring(51, 4));
                        prodDetails.ItemName = ln.Substring(55, 18).Trim();
                        prodDetails.RestrictedSale = uint.Parse(ln.Substring(73, 2));
                        prodDetails.IsWIC = ln.Substring(75, 1) == "Y";
                        prodDetails.IsItemAdded = ln.Substring(76, 1) == "Y";
                        prodDetails.Tare = uint.Parse(ln.Substring(77, 5));
                        prodDetails.IsFoodPerks = ln.Substring(82, 1) == "Y";
                        prodDetails.IsQHP = ln.Substring(83, 1) == "Y";
                        prodDetails.IsRx = ln.Substring(84, 1) == "Y";
                        prodDetails.LargeLinkedTo = ulong.Parse(ln.Substring(85, 14));
                        prodDetails.IsProductRecall = ln.Substring(99, 1) == "Y";
                        productDetails.Add(prodDetails);
                    }

                    ProductDetails result = productDetails.Where(x => x.UPC == uPC).FirstOrDefault();

                    if (result == null)
                    {
                        return NotFound(uPC);
                    }
                    else
                    {
                        return Json(productDetails.Where(x => x.UPC == uPC).FirstOrDefault());
                    }
                }
                return NotFound("Data file missing.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Get ProductDetails", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
