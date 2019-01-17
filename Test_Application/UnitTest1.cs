using System;
using System.Linq;
using Mehdime.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Application
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var scopeFactory = new ContextExtension("bak");
            using (var scope = scopeFactory.GetContextScope())
            {
                var db = scopeFactory.GetDbContext<CommonDbContext>();
                using (var dbContextScope = scopeFactory.GetContextScope())
                {
                    db.Users.Add(new User()
                    {
                        Id = Guid.NewGuid(), Name = "Marshall1", Email = "Marshall@163.com1",
                        WelcomeEmailSent = false,
                        CreatedOn = DateTime.UtcNow
                    });
                    dbContextScope.SaveChanges();
                }

                using (var dbContextScope = scopeFactory.GetContextScope())
                {
                    db.Users.FirstOrDefault().Name = "bababab1";
                    dbContextScope.SaveChanges();
                }

                using (var dbContextScope = scopeFactory.GetContextScope())
                {
                    db.Users.FirstOrDefault().Email = "dddd1";
                    dbContextScope.SaveChanges();
                }

                scope.SaveChanges();
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var scopeFactory = new ContextExtension("demo");
            using (var scope = scopeFactory.GetContextScope())
            {
                var db = scopeFactory.GetDbContext<CommonDbContext>();
                db.Users.Add(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Marshall1",
                    Email = "Marshall@163.com1",
                    WelcomeEmailSent = false,
                    CreatedOn = DateTime.UtcNow
                });
                scope.SaveChanges();
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var scopeFactory = new ContextExtension("demo");
            using (var scope = scopeFactory.GetReadOnlyContextScope())
            {
                var x = TestMethod3_sub(scope);
                Console.WriteLine(x.FirstOrDefault());
            }
        }

        public IQueryable<string> TestMethod3_sub(IDbContextReadOnlyScope scope)
        {
            var db = scope.DbContexts.Get<CommonDbContext>(); ;
            return db.Users.Select(x => x.Name);
        }
    }
}
