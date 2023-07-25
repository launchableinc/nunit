namespace Launchable.NUnit.Test;

public class BaseClass
{
  public virtual int Add(int a, int b)
  {
    return a + b;
  }
}

public class SubAClass : BaseClass
{
  public override int Add(int a, int b)
  {
    return base.Add(a, b) + 1;
  }
}

public class SubBClass : BaseClass
{
  public override int Add(int a, int b)
  {
    return base.Add(a, b) + 2;
  }
}

[TestFixture]
public class BaseClassTest
{
  public class SubAClassTest : BaseClassTest
  {
    [SetUp]
    public void Setup()
    {
      // Use the test subclass in our test.
      this.Instance = new SubAClass();
    }

    [TestCase(2, 2, 5)] // Because the SubAClass adds an extra 1.
    [TestCase(3, 3, 7)]
    public void AddTest(int a, int b, int expected)
    {
      int result = this.Instance.Add(a, b);
      Assert.AreEqual(expected, result);
    }
  }

  public class SubBClassTest : BaseClassTest
  {
    [SetUp]
    public void Setup()
    {
      // Use the test subclass in our test.
      this.Instance = new SubBClass();
    }

    [TestCase(2, 2, 6)] // Because the SubBClass adds an extra 2.
    [TestCase(3, 3, 8)]
    public void AddTest(int a, int b, int expected)
    {
      int result = this.Instance.Add(a, b);
      Assert.AreEqual(expected, result);
    }
  }

  protected BaseClass Instance { get; set; }
}
