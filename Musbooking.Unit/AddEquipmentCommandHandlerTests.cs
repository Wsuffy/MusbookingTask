using Moq;
using Musbooking.Domain.Exceptions;
using Order.Core.Entities.Equipment;
using Order.Dal.Repositories;
using Order.Dal.SqlLite.Equipment;

namespace Test.Unit;

[TestFixture]
public class AddEquipmentCommandHandlerTests
{
    private Mock<IEquipmentRepository> _mockEquipmentRepository;
    private AddEquipmentCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockEquipmentRepository = new Mock<IEquipmentRepository>();
        _handler = new AddEquipmentCommandHandler(_mockEquipmentRepository.Object);
    }

    [Test]
    [TestCase("", 0, 0.01)]
    [TestCase("Test Equipment", int.MaxValue, 189)]
    [TestCase("Regular Equipment", 50, 100.50)]
    public async Task Handle_ShouldAddEquipmentAndReturnDto(string name, int amount, decimal price)
    {
        var command = new AddEquipmentCommand(name, amount, price);

        _mockEquipmentRepository
            .Setup(repo => repo.AddAsyncAndSave(It.IsAny<Equipment>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(command.Name));
            Assert.That(result.Amount, Is.EqualTo(command.Amount));
            Assert.That(result.Price, Is.EqualTo(command.Price));
        });
        _mockEquipmentRepository.Verify(
            repo => repo.AddAsyncAndSave(
                It.Is<Equipment>(e =>
                    e.Name == command.Name && e.Amount == command.Amount && e.Price == command.Price),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    [TestCase("Test Equipment", 10, 99.99)]
    [TestCase("Test Equipment", int.MaxValue, 158)]
    [TestCase("Test Equipment", 25, 9841711)]
    public void Handle_ShouldThrowException_WhenRepositoryThrowsException(string name, int amount, decimal price)
    {
        var command = new AddEquipmentCommand(name, amount, price);

        _mockEquipmentRepository
            .Setup(repo => repo.AddAsyncAndSave(It.IsAny<Equipment>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test Exception"));

        var ex = Assert.ThrowsAsync<BadRequestExceptionWithLog>(async () =>
            await _handler.Handle(command, CancellationToken.None));

        Assert.That(ex.Message, Is.EqualTo("Test Exception"));
    }
}