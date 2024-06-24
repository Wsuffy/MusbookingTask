using FluentValidation;
using Musbooking.Application.Models.Requests.Order;

namespace Musbooking.Application.Requests.Validators;

public class CreateOrderValidator : AbstractValidator<OrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleForEach(x => x.EquipmentInOrders).ChildRules(equipments =>
        {
            equipments.RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            equipments.RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
        });
    }
}