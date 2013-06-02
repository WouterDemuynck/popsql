using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popsql.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests
{
    [TestClass]
    public class SqlTests
    {
        [TestMethod]
        public void Select_WithNullColumns_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => Sql.Select(null));
        }

        [TestMethod]
        public void Select_WithColumns_ReturnsSqlSelectWithColumns()
        {
            var query = Sql
                .Select("Id", "Name", "Email");

            Assert.AreEqual(3, query.Columns.Count());
            Assert.AreEqual("Id", query.Columns.First().ColumnName);
            Assert.AreEqual("Name", query.Columns.Skip(1).First().ColumnName);
            Assert.AreEqual("Email", query.Columns.Last().ColumnName);
        }

        [TestMethod]
        public void Update_WithTable_ReturnsSqlUpdateWithTable()
        {
            var query = Sql
                .Update("Users");

            Assert.IsNotNull(query);
            Assert.IsNotNull(query.Table);
            Assert.AreEqual("Users", query.Table.TableName);
        }

        [TestMethod]
        public void Insert_WithTable_ReturnsSqlInsertWithTable()
        {
            var query = Sql
                .Insert();
            
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void Delete_ReturnsSqlDelete()
        {
            var query = Sql
                .Delete();

            Assert.IsNotNull(query);
        }
    }
}
