using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Tests.Domain.ValueObjects;

[TestClass]
public sealed class IdentifierUnitTests
{
    [TestMethod]
    [DataRow("A")]
    [DataRow("Table-1")]
    [DataRow("Order#123")]
    [DataRow("1")]
    [DataRow("100")]
    [DataRow("5550")]
    public void Constructor_Allows_ValidIdentifiers(string value)
    {
        var identifier = new Identifier(value);
        Assert.AreEqual(value, identifier.Id);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Constructor_Throws_On_NullOrWhitespace(string value)
    {
        Assert.Throws<OrderException>(() => new Identifier(value));
    }

    [TestMethod]
    public void Constructor_Allows_MaxLengthIdentifier()
    {
        var max = new string('x', 50);
        var identifier = new Identifier(max);
        Assert.AreEqual(max, identifier.Id);
    }

    [TestMethod]
    public void Constructor_Throws_When_Identifier_Too_Long()
    {
        var tooLong = new string('x', 51);
        Assert.Throws<OrderException>(() => new Identifier(tooLong));
    }
}