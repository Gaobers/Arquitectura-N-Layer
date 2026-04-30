using ESFE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.DTOs;

public partial class Product
{

    public int BrandId { get; set; }

    public string? SupplierName { get; set; }

    public string ProductCode { get; set; } 

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public decimal PriceUnitPurchase { get; set; }

    public decimal PriceUnitSale { get; set; }

    public int Stock { get; set; }

    public bool ProductStatus { get; set; }
}  
public class UpdateProductRequest
{
    public long ProductId { get; set; }

    public int BrandId { get; set; }

    public string? SupplierName { get; set; }

    public string ProductCode { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public decimal PriceUnitPurchase { get; set; }

    public decimal PriceUnitSale { get; set; }

    public int Stock { get; set; }

    public bool ProductStatus { get; set; }
}  

public class productResponse
{
    public long ProductId { get; set; }

    public string? SupplierName { get; set; }

    public string ProductCode { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductImage { get; set; }

    public decimal PriceUnitSale { get; set; }

    public int Stock { get; set; }

    public string BrandName { get; set; } = null!;
    }



   


