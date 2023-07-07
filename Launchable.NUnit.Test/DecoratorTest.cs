namespace Launchable.NUnit.Test;

using System.IO;

public class DecoratorTest
{
    [Test]
    public void LoadTest()
    {
        var tmpFile = Path.GetTempFileName();
        try {
            File.WriteAllLines(tmpFile, new string[]{"123", "456"});
            var l = new LaunchableAttribute().Load(tmpFile, "ABC", "abc");
            CollectionAssert.AreEqual(new string[] { "123", "456" }, l);

        } finally {
            File.Delete(tmpFile);
        }
    }
}