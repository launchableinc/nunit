namespace Launchable.NUnit.Test;

/// <summary>
/// If all tests are skipped, does setup/teardown still happen?
/// </summary>
[TestFixture]
public class SetupAndTeardownTest
{
    public SetupAndTeardownTest()
    {
        // even when tests are skipped, apparently instances are still constructed.
        // if the following line is commented out, the test will fail
        // throw new AssertionException("object shouldn't be constructed");
    }

    [SetUp]
    public void Before()
    {
        Console.WriteLine("before");
        throw new AssertionException("setup should have been skipped");
    }

    [TearDown]
    public void After()
    {
        Console.WriteLine("after");
        throw new AssertionException("teardown should have been skipped");
    }

    [Test]
    public void Foo()
    {
        Console.WriteLine("foo");
        throw new AssertionException("this test should have been skipped");
    }
}