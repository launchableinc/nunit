using System;

using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Launchable.NUnit {

    /// <summary>
    /// Subset tests within this assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LaunchableAttribute : Attribute, IApplyToTest
    {
        public void ApplyToTest(Test test)
        {
            Walk(test);
        }

        private void Walk(Test t)
        {
            Process(t);
            foreach (Test c in t.Tests)
            {
                Walk(c);
            }
        }
        
        // Example of inspecting a test and applying a custom filtering mechanism
        // see https://github.com/nunit/nunit/blob/master/src/NUnitFramework/framework/Internal/Tests/Test.cs
        private void Process(Test t)
        {
            Console.WriteLine("Examining: "+t.FullName);
            if (t.FullName.EndsWith("Test2"))
            {
                t.RunState = RunState.Skipped;
            }
        }
    }
}