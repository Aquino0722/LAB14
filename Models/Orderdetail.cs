using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Models;

[Table("orderdetails")]
[Index("OrderId", Name = "OrderId")]
[Index("ProductId", Name = "ProductId")]
public partial class Orderdetail
{
    [Key]
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Orderdetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Orderdetails")]
    public virtual Product Product { get; set; } = null!;
}
