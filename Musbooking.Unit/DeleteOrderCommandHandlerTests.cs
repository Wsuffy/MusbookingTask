using Moq;
using Musbooking.Application.Commands.Order;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Domain.Exceptions;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Test.Unit;

[TestFixture]
public class DeleteOrderCommandHandlerTests
{
    private Mock<IOrderRepository> _mockOrderRepository;
    private Mock<IEquipmentRepository> _mockEquipmentRepository;
    private Mock<IOrderEquipmentRepository> _mockOrderEquipmentRepository;
    private DeleteOrderCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockEquipmentRepository = new Mock<IEquipmentRepository>();
        _mockOrderEquipmentRepository = new Mock<IOrderEquipmentRepository>();
        _handler = new DeleteOrderCommandHandler(_mockOrderRepository.Object, _mockEquipmentRepository.Object,
            _mockOrderEquipmentRepository.Object);
    }

    [Test]
    [TestCase(1)]
    [TestCase(int.MaxValue)]
    [TestCase(int.MinValue)]
    public async Task Handle_ShouldDeleteOrderAndRelatedEquipment(int orderId)
    {
        var order = new Musbooking.Domain.Entities.Order.Order { Id = orderId };
        var orderEquipments = new List<OrderEquipment>
        {
            new OrderEquipment { OrderId = orderId, EquipmentId = 1, Quantity = 2 },
            new OrderEquipment { OrderId = orderId, EquipmentId = 2, Quantity = 3 }
        };

        _mockOrderRepository
            .Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        _mockOrderEquipmentRepository
            .Setup(repo => repo.GetAllByOrderId(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orderEquipments);

        var result = await _handler.Handle(new DeleteOrderCommand(orderId), CancellationToken.None);

        Assert.That(result, Is.EqualTo(orderId));
        _mockOrderRepository.Verify(repo => repo.DeleteAndSaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        _mockOrderEquipmentRepository.Verify(
            repo => repo.DeleteRangeAsync(orderEquipments, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    [TestCase(1)]
    [TestCase(int.MaxValue)]
    [TestCase(int.MinValue)]
    public void Handle_ShouldThrowException_WhenOrderNotFound(int orderId)
    {
        _mockOrderRepository
            .Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Musbooking.Domain.Entities.Order.Order)null);

        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(new DeleteOrderCommand(orderId), CancellationToken.None));

        Assert.That(ex.Message, Does.Contain("Заказ с таким Id не найден"));
        _mockOrderRepository.Verify(
            repo => repo.DeleteAndSaveAsync(It.IsAny<Musbooking.Domain.Entities.Order.Order>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
        _mockOrderEquipmentRepository.Verify(
            repo => repo.DeleteRangeAsync(It.IsAny<List<OrderEquipment>>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}