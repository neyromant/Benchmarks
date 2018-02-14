using System;
using System.Collections.Generic;
using System.Linq;
using AsNoTracking.Entities;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AsNoTracking
{
    [CoreJob, MemoryDiagnoser]
    public class AsNoTrackingTests
    {
        [Params(1, 10, 100, 1000)]
        public int Records { get; set; }

        [Params(DbTypes.InMemory, DbTypes.SqlLight)]
        public DbTypes DbType { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            using (var prepareDataContext = CreateDbContext())
            {
                prepareDataContext.Database.EnsureCreated();

                for (var i = 0; i < 1000; i++)
                {
                    prepareDataContext.Users.Add(new User
                    {
                        Id = i,
                        Name = $"User_{i}"
                    });
                }

                prepareDataContext.SaveChanges();
            }
        }

        [Benchmark(Baseline = true)]
        public List<User> Track()
        {
            using (var db = CreateDbContext())
            {
                return db.Users.Where(x => x.Id < Records).ToList();
            }
        }

        
        [Benchmark]
        public List<User> NoTrack()
        {
            using (var db = CreateDbContext())
            {
                return db.Users.Where(x => x.Id < Records).AsNoTracking().ToList();
            }
        }

        
        
        private TestDbContext CreateDbContext()
        {
            if (DbType == DbTypes.InMemory)
            {
                var dbName = Records.ToString();
                var options = new DbContextOptionsBuilder<TestDbContext>()
                    .UseInMemoryDatabase(dbName)
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;

                return new TestDbContext(options);
            }

            if (DbType == DbTypes.SqlLight)
            {
                var builder = new DbContextOptionsBuilder<TestDbContext>();
                builder.UseSqlite($"Filename=./Test.AsNoTracking.{Records}.db");
                var options = builder.Options;

                return new TestDbContext(options);
            }

            throw new NotSupportedException($"{nameof(DbType)} = {DbType} not suppoted");
        }
    }
}
