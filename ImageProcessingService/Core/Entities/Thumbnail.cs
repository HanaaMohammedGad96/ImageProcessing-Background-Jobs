using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ImageProcessingService.Core.Entities;
public class Thumbnail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Please enter the path of the image")]
    public string Path { get; set; }

    [Required(ErrorMessage = "Please enter the width of the image")]
    [DefaultValue(Constants.Constants.DefaultWidth)]
    public int Width { get; set; }

    [Required(ErrorMessage = "Please enter the height of the image")]
    [DefaultValue(Constants.Constants.DefaultHeight)]
    public int Height { get; set; }
    public DateTime CreatedAt { get; set; }

}
