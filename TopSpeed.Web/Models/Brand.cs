using System.ComponentModel.DataAnnotations;

namespace TopSpeed.Web.Models
{
    public class Brand
    {
        [Key]                                            // Key is used to set the id has the primary key
        public Guid Id { get; set; }                    // Id for the primary key that is the reason using the GUID

        [Required]                                     // Required is used for Non-nullable purpose of the Name
        public string Name { get; set; }              // Product names needs to be store that is the reason using the string
                                                      // Name varibale is getting error means then give this = string.Empty or use this project name click that to edit the <Nullable>disable<Nullable>
        [Display(Name = "Established Year")]
        public int EstablishedYear { get; set; }        // Product establishedyear

        [Display(Name = "Brand Logo")]
        public string BrandLogo { get; set; }           // Product of the brandlogo


    
    
    }
}
