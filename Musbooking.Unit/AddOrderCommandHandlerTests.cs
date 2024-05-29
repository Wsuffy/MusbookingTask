using Moq;
using Musbooking.Application.Models.DTOs.Equipment;
using Musbooking.Domain.Exceptions;
using Order.Core.Entities.Equipment;
using Order.Core.Entities.OrderEquipment;
using Order.Dal.Repositories;
using Order.Dal.SqlLite.Order;

namespace Test.Unit;

[TestFixture]
public class AddOrderCommandHandlerTests
{
    private Mock<IEquipmentRepository> _mockEquipmentRepository;
    private Mock<IOrderRepository> _mockOrderRepository;
    private AddOrderCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockEquipmentRepository = new Mock<IEquipmentRepository>();
        _mockOrderRepository = new Mock<IOrderRepository>();
        _handler = new AddOrderCommandHandler(_mockEquipmentRepository.Object, _mockOrderRepository.Object);
    }

    [Test]
    [TestCase("Order 1", 1, 10, 100)]
    [TestCase("Order 2", 2, 5, 200)]
    [TestCase("Order 3", 3, 1, 500)]
    public async Task Handle_ShouldAddOrderAndReturnDto(string description, int equipmentId, int amount, decimal price)
    {
        var command = new AddOrderCommand(description, new List<EquipmentDto>
        {
            new EquipmentDto { Id = equipmentId, Amount = amount, Price = price }
        });

        var equipment = new Equipment
        {
            Id = equipmentId,
            Name = "Test Equipment",
            Amount = amount + 1, // Ensure enough stock
            Price = price
        };

        _mockEquipmentRepository
            .Setup(repo => repo.GetByIdAsync(equipmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(equipment);

        var order = new Order.Core.Entities.Order.Order
            { Description = description, Equipments = new List<OrderEquipment>(), Price = 0 };

        _mockOrderRepository
            .Setup(repo =>
                repo.AddAndSaveAsync(It.IsAny<Order.Core.Entities.Order.Order>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Description, Is.EqualTo(command.Description));
            Assert.That(result.Price, Is.EqualTo(price * amount));
        });

        _mockEquipmentRepository.Verify(
            repo => repo.GetByIdAsync(equipmentId, It.IsAny<CancellationToken>()), Times.Once);

        _mockOrderRepository.Verify(
            repo => repo.AddAndSaveAsync(It.Is<Order.Core.Entities.Order.Order>(
                o => o.Description == command.Description &&
                     o.Equipments.Count == 1 &&
                     o.Equipments[0].EquipmentId == equipmentId &&
                     o.Equipments[0].Quantity == amount &&
                     o.Price == price * amount), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    [TestCase("Order test 1", 1, 10)]
    public void Handle_ShouldThrowException_WhenEquipmentNotFound(string description, int equipmentId, int amount)
    {
        var command = new AddOrderCommand(description, new List<EquipmentDto>
        {
            new EquipmentDto { Id = equipmentId, Amount = amount }
        });

        _mockEquipmentRepository
            .Setup(repo => repo.GetByIdAsync(equipmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Equipment)null);

        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(command, CancellationToken.None));

        Assert.That(ex.Message,
            Does.Contain("Вы пытаетесь создать заказ, но не найдет equipment или же его слишком мало на складе"));
    }
    
    [Test]
    [TestCase("Order 1", 1, 10, 5)]
    public void Handle_ShouldThrowException_WhenInsufficientEquipmentAmount(string description, int equipmentId, int requestedAmount, int availableAmount)
    {
        var command = new AddOrderCommand(description, new List<EquipmentDto>
        {
            new EquipmentDto { Id = equipmentId, Amount = requestedAmount }
        });

        var equipment = new Equipment
        {
            Id = equipmentId,
            Name = "Test Equipment",
            Amount = availableAmount, // Not enough stock
            Price = 100
        };

        _mockEquipmentRepository
            .Setup(repo => repo.GetByIdAsync(equipmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(equipment);

        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(command, CancellationToken.None));

        Assert.That(ex.Message, Does.Contain("Вы пытаетесь создать заказ, но не найдет equipment или же его слишком мало на складе"));
    }
    
    [Test]
    [TestCase("Order test 2", 2, 1, 1)]
    public async Task Handle_ShouldReduceEquipmentAmount_WhenOrderIsCreated(string description, int equipmentId, int requestedAmount, int availableAmount)
    {
        var command = new AddOrderCommand(description, new List<EquipmentDto>
        {
            new EquipmentDto { Id = equipmentId, Amount = requestedAmount }
        });

        var equipment = new Equipment
        {
            Id = equipmentId,
            Name = "Test Equipment",
            Amount = availableAmount,
            Price = 100
        };

        _mockEquipmentRepository
            .Setup(repo => repo.GetByIdAsync(equipmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(equipment);

        _mockOrderRepository
            .Setup(repo => repo.AddAndSaveAsync(It.IsAny<Order.Core.Entities.Order.Order>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _mockEquipmentRepository.Verify(repo => repo.GetByIdAsync(equipmentId, It.IsAny<CancellationToken>()), Times.Once);
        Assert.That(equipment.Amount, Is.EqualTo(availableAmount - requestedAmount));
    }
}
