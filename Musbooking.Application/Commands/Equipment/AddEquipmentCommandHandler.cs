using MediatR;
using Musbooking.Application.Models.DTOs.Equipment;
using Musbooking.Domain.Exceptions;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Musbooking.Application.Commands.Equipment;

public record AddEquipmentCommand(string Name, int Amount, decimal Price) : IRequest<EquipmentDto>;

public class AddEquipmentCommandHandler : IRequestHandler<AddEquipmentCommand, EquipmentDto>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public AddEquipmentCommandHandler(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }


    public async Task<EquipmentDto> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new global::Musbooking.Domain.Entities.Equipment.Equipment()
            {
                Name = request.Name,
                Amount = request.Amount,
                Price = request.Price
            };
            await _equipmentRepository.AddAsyncAndSave(entity, cancellationToken);

            return new EquipmentDto(entity);
        }
        catch (Exception e)
        {
            throw new BadRequestExceptionWithLog(e.Message, "Возникла при создании Equipment",
                "Exception while user trying login \n Message: {e}", e);
        }
    }
}