using Moq;
using Musbooking.Application.Models.DTOs.Equipment;
using Musbooking.Domain.Exceptions;
using Order.Core.Entities.Equipment;
using Order.Dal.Repositories;
using Order.Dal.SqlLite.Order;

namespace Test.Unit;

[TestFixture]
public class UpdateOrderCommandHandlerTests
{
    private Mock<IOrderRepository> _mockOrderRepository;
    private Mock<IEquipmentRepository> _mockEquipmentRepository;
    private UpdateOrderCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockEquipmentRepository = new Mock<IEquipmentRepository>();
        _handler = new UpdateOrderCommandHandler(_mockOrderRepository.Object, _mockEquipmentRepository.Object);
    }

    [Test]
    [TestCase(1, "Updated description")]
    [TestCase(2, "Updated description")]
    [TestCase(2, "Updated description")]
    public async Task Handle_ValidUpdate_ShouldReturnOrderDto(int orderId, string description)
    {
        var equipmentDtos = new List<EquipmentDto>
        {
            new EquipmentDto { Id = 1, Amount = 5 },
            new EquipmentDto { Id = 2, Amount = 3 }
        };
        var command = new UpdateOrderCommand(orderId, description, equipmentDtos);
        var order = new Order.Core.Entities.Order.Order { Id = orderId };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync(orderId, CancellationToken.None)).ReturnsAsync(order);
        _mockEquipmentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(new Equipment { Id = 1, Amount = 10, Price = 100 });

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(orderId));
            Assert.That(result.Description, Is.EqualTo(description));
            Assert.That(result.Price, Is.GreaterThan(0));
            Assert.That(result.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    [TestCase(0, "Updated description")]
    [TestCase(-1, "Updated description")]
    [TestCase(int.MinValue, "Updated description")]
    public void Handle_InvalidId_ShouldThrowBadRequestException(int orderId, string description)
    {
        var equipmentDtos = new List<EquipmentDto>();
        var command = new UpdateOrderCommand(orderId, description, equipmentDtos);


        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Вы попытались ввести некорректный Id"));
    }

    [Test]
    [TestCase(1, "Updated description")]
    public void Handle_OrderNotFound_ShouldThrowBadRequestException(int orderId, string description)
    {
        var equipmentDtos = new List<EquipmentDto>();
        var command = new UpdateOrderCommand(orderId, description, equipmentDtos);

        _mockOrderRepository.Setup(repo => repo.GetByIdAsync(orderId, CancellationToken.None))
            .ReturnsAsync((Order.Core.Entities.Order.Order)null);

        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Заказ с таким Id не найден"));
    }

    [Test]
    [TestCase(50, "Updated description")]
    [TestCase(15, "Updated description")]
    [TestCase(1, "Updated description")]
    public void Handle_InsufficientEquipmentAmount_ShouldThrowBadRequestException(int orderId, string description)
    {
        var equipmentDtos = new List<EquipmentDto>
        {
            new EquipmentDto { Id = 1, Amount = 5 },
            new EquipmentDto { Id = 2, Amount = 3 }
        };
        var command = new UpdateOrderCommand(orderId, description, equipmentDtos);
        var order = new Order.Core.Entities.Order.Order { Id = orderId };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync(orderId, CancellationToken.None)).ReturnsAsync(order);
        _mockEquipmentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(new Equipment { Id = 1, Amount = 3, Price = 100 });

        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message,
            Is.EqualTo("Вы пытаетесь создать заказ, но не найдет equipment или же его слишком мало на складе"));
    }
}