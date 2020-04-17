namespace SocialMedia.Services.Data.Tests.Common
{
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using SocialMedia.Data;
    using System;

    public class InMemoryDbContext
    {
        public static ApplicationDbContext Context()
        {
            var dbName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff");

            IOptions<OperationalStoreOptions> operationalStoreOptions = Options.Create(new OperationalStoreOptions());

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: dbName)
               .Options;

            return new ApplicationDbContext(options, operationalStoreOptions);
        }
    }
}
