using NUnit.Framework;

namespace movieDatabaseAPI_Tests
{
[TestFixture]
public class PresentationFormatterTests
{
    private PresentationFormatter _formatter;

    [SetUp]
    public void Setup()
    {
        _formatter = new PresentationFormatter();
    }

    [Test]
    [TestCase(88, "1 hour 28 minutes")]
    [TestCase(200, "3 hours 20 minutes")]
    [TestCase(49, "49 minutes")]

    public void TransformRuntime_ShouldReturnCorrectFormat(int runtime, string expected)
    {
        var result = _formatter.TransformRuntime(runtime);
        Assert.AreEqual(expected, result);
    }

    
}
}