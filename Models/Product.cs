using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Models;

[Table("products")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [Precision(10, 2)]
    public decimal Price { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
