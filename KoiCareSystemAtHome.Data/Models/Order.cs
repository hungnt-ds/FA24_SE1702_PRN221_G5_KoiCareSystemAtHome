﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiCareSystemAtHome.Data.Models;

[Table("orders")]
public partial class Order
{
    [Key]
    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("order_date")]
    public long? OrderDate { get; set; }

    [Column("quantity")]
    public long? Quantity { get; set; }

    [Column("total_price", TypeName = "decimal(10, 2)")]
    public decimal? TotalPrice { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Order")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; }
}