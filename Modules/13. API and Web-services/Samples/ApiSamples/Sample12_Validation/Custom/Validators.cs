using FluentValidation;

namespace Sample12_Validation.Custom
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Postcode).NotEmpty();
            RuleFor(a => a.Country).NotEmpty();
            RuleFor(a => a.Town).NotEmpty();
        }
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MinimumLength(3);
            RuleFor(p => p.Age).InclusiveBetween(3, 120);
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.Address).SetValidator(new AddressValidator());
        }
    }

}
