namespace Launchable.NUnit.Test;

/*
This triggers a bug in NUnitXML.Logger. You get an incorrect XML report like this,
where the method name ends up including more than the method name:

  <test-suite type="TestFixture" name="Test" fullname="Launchable.NUnit.Test" classname="Launchable.NUnit.Test" total="1" passed="1" failed="0" inconclusive="0" skipped="0" result="Passed" start-time="2023-07-28T 22:25:35Z" end-time="2023-07-28T 22:25:35Z" duration="1.6E-05">
    <test-case name="TheTest" fullname="Launchable.NUnit.Test.Outer+Inner.TheTest" methodname="Outer+Inner.TheTest" classname="Test" result="Passed" start-time="2023-07-28T 22:25:35Z" end-time="2023-07-28T 22:25:35Z" duration="1.6E-05" asserts="0" seed="80945991" />
  </test-suite>

*/
public class Outer
{
  public class Inner
  {
    [Test]
    public void TheTest()
    {
    }
  }
}
