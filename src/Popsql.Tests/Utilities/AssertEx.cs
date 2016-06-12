using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests.Utilities
{
    /// <summary>
    /// Provides the 'forgotten' assertions for <b>MSTest</b>.
    /// </summary>
    public static class AssertEx
    {
        public static TException Throws<TException>(Action testCode)
            where TException : Exception
        {
            if (testCode == null) throw new ArgumentNullException("testCode");

            try
            {
                testCode();
            }
            catch (TException ex)
            {
                return ex;
            }

            throw new AssertFailedException("An expected exception of type " + typeof(TException) + " was not thrown.");
        }
    }
}
