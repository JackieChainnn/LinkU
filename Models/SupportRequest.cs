using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("SupportRequest")]
public partial class SupportRequest
{
    [Column("ID")]
    public string Id { get; private set; }

    [Required]
    [Column("CustomerID")]
    [StringLength(450)]
    public string CustomerId { get; set; } = null!;

    [Required]
    [StringLength(450)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(450)]
    public string? Description { get; set; }

    [Display(Name = "Status")]
    [EnumDataType(typeof(ApplicationStatus))]
    public SupportRequestStatus Status { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Created At")]
    public DateOnly? CreatedAt { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Updated At")]
    public DateOnly? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("SupportRequests")]
    public virtual Applicant Customer { get; set; } = null!;

    [InverseProperty("Request")]
    public virtual ICollection<SupportResponse> SupportResponses { get; set; } = new List<SupportResponse>();

    public SupportRequest()
    {
        Id = Guid.NewGuid().ToString()[..6];
    }
}

public enum SupportRequestStatus
{
    Open,
    Closed,
    Pending
}