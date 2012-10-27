﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popsql.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests
{
    [TestClass]
    public class SqlUpdateTests
    {
        [TestMethod]
        public void Ctor_WithNullTable_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlUpdate(null));
        }

        [TestMethod]
        public void Ctor_WithSqlTable_SetsTargetProperty()
        {
            var update = new SqlUpdate("Users");
            Assert.IsNotNull(update.Target);
            Assert.AreEqual("Users", update.Target.TableName);
        }
    }
}
