using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TestProject2
{
    public class TestData
    {
        
        public static IEnumerable<TestCaseData> TypeLogin
        {
            get
            {

                yield return new TestCaseData("LiudaVahrameyeva")
                    .SetName("Login as User");
                yield return new TestCaseData("test.sel.trainingselenium")
                    .SetName("Login as Test");
            }
        }

        
    }

   
}