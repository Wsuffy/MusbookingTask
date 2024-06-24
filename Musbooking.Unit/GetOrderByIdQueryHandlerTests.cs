using Moq;
using Musbooking.Domain.Entities.Order;
using Musbooking.Domain.Exceptions;
using Musbooking.Service.Abstractions.Abstractions;
using Musbooking.Service.Implementation.Commands.Order;

namespace Test.Unit;

[TestFixture]
public class GetOrderByIdQueryHandlerTests
{
    private Mock<IOrderReadRepository> _mockOrderReadRepository;
    private GetOrderByIdQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockOrderReadRepository = new Mock<IOrderReadRepository>();
        _handler = new GetOrderByIdQueryHandler(_mockOrderReadRepository.Object);
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    public void Handle_ShouldThrowException_WhenInvalidPageSizeOrPageNumber(int pageSize, int pageNumber)
    {
        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(new GetOrderByIdQuery(pageSize, pageNumber), CancellationToken.None));

        Assert.That(ex.Message, Does.Contain("Value cannot be null. (Parameter 'source')"));
    }

    [Test]
    public void Handle_ShouldReturnEmptyPagedResult_WhenNoOrders()
    {
        _mockOrderReadRepository.Setup(repo =>
                repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(new List<Order>());

        var result = _handler.Handle(new GetOrderByIdQuery(10, 1), CancellationToken.None).Result;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Empty);
            Assert.That(result.Count, Is.Zero);
        });
    }

    [Test]
    public void Handle_ShouldReturnPagedResultWithOrders_WhenValidRequest()
    {
        var orders = new List<Order>
        {
            new() { Id = 1, Description = "Order 1" },
            new() { Id = 2, Description = "Order 2" }
        };

        _mockOrderReadRepository.Setup(repo =>
                repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(orders);

        var result = _handler.Handle(new GetOrderByIdQuery(10, 1), CancellationToken.None).Result;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Has.Count.EqualTo(2));
            Assert.That(result.Count, Is.EqualTo(2));
        });
    }
}