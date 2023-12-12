using System.ComponentModel.DataAnnotations;

namespace Countries.DataLayer.Entities;

public abstract class BaseEntity
{
    [Key, Required]
    public int Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set;}
    [Required]
    public bool IsDeleted { get; set; }

}
