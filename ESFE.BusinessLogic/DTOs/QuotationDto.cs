using ESFE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.DTOs;


public class CreateQuotationRequest
{
    public string ClientName { get; set; } = null!;

    public string? ClientPhone { get; set; }

    public string SellerName { get; set; } = null!;

    public int UserId { get; set; }

    public string? PaymentMethodName { get; set; }

    public string QuotationNumber { get; set; } = null!;

    public int ValidityDays { get; set; }

    public DateTime QuotationRegistration { get; set; }

    public decimal Total { get; set; }

    public bool QuotationStatus { get; set; }

    public virtual ICollection<CreateQuotationDetailRequest> QuotationDetails { get; set; } = new List<CreateQuotationDetailRequest>();

}


public class CreateQuotationDetailRequest
{
  

    public int QuotationId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal Subtotal { get; set; }

}

public class QuotationResponse
{
    public int QuotationId { get; set; }

    public string ClientName { get; set; } = null!;

    public string? ClientPhone { get; set; }

    public string SellerName { get; set; } = null!;

    public int UserId { get; set; }

    public string? PaymentMethodName { get; set; }

    public string QuotationNumber { get; set; } = null!;

    public int ValidityDays { get; set; }

    public DateTime QuotationRegistration { get; set; }

    public decimal Total { get; set; }

    public bool QuotationStatus { get; set; }

    public virtual ICollection<QuotationDetailResponse> QuotationDetails { get; set; } = new List<QuotationDetailResponse>();

    public virtual UserResponse? User { get; set; } = null!;
}

public class QuotationDetailResponse
{
    public int QuotationDetailId { get; set; }

    public int QuotationId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal Subtotal { get; set; }

    public virtual ProductResponse? Product { get; set; } = null!; 
}


