using Barbuuuda;
using Barbuuuda.Core.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace Bar.Tests
{
    public class BaseTestServerFixture
    {
        public TestServer TestServer { get; }
        public ApplicationDbContext _db { get; }
        public PostgreDbContext _postgre { get; }
        public HttpClient Client { get; }

        public BaseTestServerFixture(ApplicationDbContext db, PostgreDbContext postgre)
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            _db = db;
            _postgre = postgre;
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}
