using FluentValidation.TestHelper;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Musbooking.Application.Models.Requests.Equipment;
using Musbooking.Application.Models.Requests.Order;
using Musbooking.Application.Requests.Validators;

namespace Test.Unit;

[TestFixture]
public class ValidatorsTests
{
    private CreateEquipmentValidator _equipmentValidator;
    private CreateOrderValidator _orderValidator;

    [SetUp]
    public void Setup()
    {
        _equipmentValidator = new CreateEquipmentValidator();
        _orderValidator = new CreateOrderValidator();
    }

    [Test]
    [TestCase("", 10, 100)]
    [TestCase("", int.MaxValue, 123)]
    [TestCase("", int.MinValue, 100)]
    public void EquipmentValidator_WhenNameIsEmpty_ShouldHaveValidationError(string description, int amount,
        decimal price)
    {
        // Arrange
        var equipmentRequest = new EquipmentRequest(description, amount, price);

        // Act
        var result = _equipmentValidator.TestValidate(equipmentRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Test]
    [TestCase("Equipment", 0, 100)]
    public void EquipmentValidator_WhenAmountIsZero_ShouldHaveValidationError(string description, int amount,
        decimal price)
    {
        // Arrange
        var equipmentRequest = new EquipmentRequest(description, amount, price);

        // Act
        var result = _equipmentValidator.TestValidate(equipmentRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Amount);
    }

    [Test]
    [TestCase("Equipment", 10, 0)]
    public void EquipmentValidator_WhenPriceIsZero_ShouldHaveValidationError(string description, int amount,
        decimal price)
    {
        // Arrange
        var equipmentRequest = new EquipmentRequest(description, amount, price);

        // Act
        var result = _equipmentValidator.TestValidate(equipmentRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Test]
    [TestCase("")]
    public void OrderValidator_WhenDescriptionIsEmpty_ShouldHaveValidationError(string description)
    {
        // Arrange
        var sourceList = new List<EquipmentRequestWithIdAndQuantity>();
        var orderRequest = new OrderRequest(description, sourceList);

        // Act
        var result = _orderValidator.TestValidate(orderRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Test]
    [TestCase("Order description")]
    public void OrderValidator_WhenEquipmentIdIsZero_ShouldHaveValidationError(string description)
    {
        // Arrange
        var sourceList = new List<EquipmentRequestWithIdAndQuantity>
        {
            new(0, 10),
            new(5, 15)
        };
        var orderRequest = new OrderRequest(description, sourceList);

        // Act
        var result = _orderValidator.TestValidate(orderRequest);

        // Assert
        result.ShouldHaveAnyValidationError();
    }

    [Test]
    [TestCase("Order description")]
    public void OrderValidator_WhenEquipmentQuantityIsZero_ShouldHaveValidationError(string description)
    {
        // Arrange
        var sourceList = new List<EquipmentRequestWithIdAndQuantity>
        {
            new(1, 0),
            new(2, 5)
        };
        var orderRequest = new OrderRequest(description, sourceList);

        // Act
        var result = _orderValidator.TestValidate(orderRequest);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}