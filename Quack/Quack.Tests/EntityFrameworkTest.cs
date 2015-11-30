using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quack.Core.Infrastructure;
using System.Linq;

namespace Quack.Tests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void GenerateDatabaseTest()
        {
            using (var db = new QuackDbContext())
            {
                var firstTeacher = db.Teachers.FirstOrDefault();

                Assert.IsNull(firstTeacher);
            }
        }
    }
}
