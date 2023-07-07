namespace Launchable.NUnit.Test;

/// <summary>
/// Verify that one line integration in AssemblyInfo.cs works
/// </summary>
public class OneLineIntegrationTest
{
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void Test2()
    {
        Assert.Fail("this test is supposed to be skipped");
    }
}

[TestFixture]
public class ParameterizedTests
{
    [TestCaseSource(nameof(TEST_CASES))]
    public void DivideTest(int n)
    {
        Assert.IsTrue(n != 2, "n==2 should have been skipped");
    }

    public static readonly int[] TEST_CASES = { 1, 2, 3 };
}
