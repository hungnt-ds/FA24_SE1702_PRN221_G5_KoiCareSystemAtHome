﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiCareSystemAtHome.Data.Models;

[Table("thresholds")]
public partial class Threshold
{
    [Key]
    [Column("parameter_id")]
    public long ParameterId { get; set; }

    [Column("parameter_name")]
    public long ParameterName { get; set; }

    [Column("min_value")]
    public long MinValue { get; set; }

    [Column("max_value")]
    public long MaxValue { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }
}