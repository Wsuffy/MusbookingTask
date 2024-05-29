using FluentValidation;
using FluentValidation.Results;
using Musbooking.Application.Requests.Equipment;

namespace Musbooking.Application.Requests.Validators;

public class CreateEquipmentValidator : AbstractValidator<EquipmentRequest>
{
    public CreateEquipmentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
    
}