﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Mehdime.Entity
{
    public class ContextExtension
    {
        private readonly string _site;
        public ContextExtension(string site = null)
        {
            _site = site;
        }
        public IDbContextScope GetContextScope(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            var dbContextScopeFactory = new DbContextScopeFactory(site: _site);
            return dbContextScopeFactory.Create(joiningOption);
        }

        public IDbContextReadOnlyScope GetReadOnlyContextScope(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            var dbContextScopeFactory = new DbContextScopeFactory(site: _site);
            return dbContextScopeFactory.CreateReadOnly(joiningOption);
        }

        public TDbContext GetDbContext<TDbContext>() where TDbContext : System.Data.Entity.DbContext
        {
            var ambientDbContextLocator = new AmbientDbContextLocator();
            var dbContext = ambientDbContextLocator.Get<TDbContext>();

            if (dbContext == null)
                throw new InvalidOperationException(
                    "No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

            return dbContext;
        }
    }
}
