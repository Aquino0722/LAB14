using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabLINQ.Models;

[Table("orders")]
[Index("ClientId", Name = "ClientId")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int ClientId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Orders")]
    public virtual Client Client { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
