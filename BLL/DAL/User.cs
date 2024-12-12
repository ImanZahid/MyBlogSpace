﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Index("UserName", Name = "UQ__Users__C9F284562E8C76D7", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string UserName { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; }

    public bool IsActive { get; set; }

    public int? RoleId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; }
}