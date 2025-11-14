using Microsoft.EntityFrameworkCore;
using TekusServices.Domain.Entities;
using TekusServices.Infrastructure.Data;
using TekusServices.Infrastructure.Data.Repositories;

namespace TekusServices.Tests.UnitTests.Repositories
{
    public class ProviderRepositoryTests
    {
        private ApplicationDbContext _context = null!;
        private ProviderRepository _repository = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Providers")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // seed
            _context.Providers.Add(new Provider { 
                ProviderId = 1, 
                Nit = "1", 
                Name = "P1", 
                Email = "p1@t.com", 
                Active = true });
            _context.Providers.Add(new Provider { 
                ProviderId = 2, 
                Nit = "2", 
                Name = "P2", 
                Email = "p2@t.com", 
                Active = true });
            _context.SaveChanges();

            _repository = new ProviderRepository(_context);
        }

        [Test]
        public async Task GetProviders_ReturnsOnlyActive()
        {
            var result = await _repository.GetProviders();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("P1"));
        }
    }
}
