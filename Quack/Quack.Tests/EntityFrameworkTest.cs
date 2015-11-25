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
                var firstUser = db.Users.FirstOrDefault();

                Assert.IsNull(firstUser);
            }
        }
    }
}
