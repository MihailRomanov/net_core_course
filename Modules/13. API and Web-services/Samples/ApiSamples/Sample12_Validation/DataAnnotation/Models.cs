using System.ComponentModel.DataAnnotations;

namespace Sample12_Validation.DataAnnotation
{
    public class Person
    {
        [MinLength(3)]
        [Required()]
        public string Name { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public string Postcode { get; set; }
    }


}
