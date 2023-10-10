using System;
using System.IO;
using System.Text;
using System.Collections;

using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Launchable.NUnit {

    /// <summary>
    /// Subset tests within this assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LaunchableAttribute : Attribute, IApplyToTest
    {
        private static readonly String LAUNCHABLE_SUBSET_FILE = "LAUNCHABLE_SUBSET_FILE_PATH";

        private static readonly String LAUNCHABLE_REST_FILE = "LAUNCHABLE_REST_FILE_PATH";

        private IList<string>? subset = null;

        private IList<string>? rest = null;

        private bool debug = Environment.GetEnvironmentVariable("LAUNCHABLE_DEBUG")!=null;


        private void Warn(string msg, params object[] args)
        {
            // Does NUnit provide a context object to write messages to?
            // stderr feels like too strong an assumption
            Console.Error.WriteLine(msg,args);
        }

        internal IList<string>? Load(string? fileName, string envVar, string fileType)
        {
            IList<string>? list = null;

            if (fileName !=null)
            {
                try
                {
                    list = File.ReadAllLines(fileName, Encoding.UTF8);
                }
                catch (IOException e)
                {
                    throw new IOException(String.Format("Cannot read {0} file {1}. Make sure to set {0} result file path to {2}", fileType, fileName, envVar), e);
                }
                Console.WriteLine("{0} file ({1}) is loaded", fileType, fileName);
                if (list.Count == 0)
                {
                    Warn("{0} file {1} is empty. Please check your configuration", fileType, fileName);
                }
            }

            return list;
        }


        public void ApplyToTest(Test test)
        {
            var subsetFile = Environment.GetEnvironmentVariable(LAUNCHABLE_SUBSET_FILE);
            var restFile   = Environment.GetEnvironmentVariable(LAUNCHABLE_REST_FILE);
            if (subsetFile != null && restFile != null)
            {
                Warn("ERROR: Cannot set subset file ({0}) and rest file ({1}) both. Make sure set only one side.", subsetFile, restFile);
                return;
            }

            subset = Load(subsetFile, LAUNCHABLE_SUBSET_FILE, "subset");
            rest   = Load(restFile, LAUNCHABLE_REST_FILE, "rest");

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

        internal void Process(Test t)
        {
            var n = t.FullName;

            if (debug)
            {
                Console.WriteLine("Examining: "+n);
            }

            if (subset!=null && !subset.Contains(n))
            {
                t.RunState = RunState.Skipped;
            }
            if (rest!=null && rest.Contains(n))
            {
                t.RunState = RunState.Skipped;
            }
            if (n=="Launchable.NUnit.Test.OneLineIntegrationTest.Test2"
            ||  n=="Launchable.NUnit.Test.ParameterizedTests.DivideTest(2)"
            ||  n=="Launchable.NUnit.Test.SetupAndTeardownTest.Foo")
            {// unit test probe
                t.RunState = RunState.Skipped;
            }
        }
    }
}