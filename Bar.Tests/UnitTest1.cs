using Barbuuuda.Core.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Bar.Tests
{
    public class UnitTest1
    {
        private const string ACCOUNT = "lera";
        //private readonly BaseTestServerFixture _fixture;
        private readonly IChat _chat;

        public UnitTest1(IChat chat)
        {
            _chat = chat;
        }

        [Fact]
        public async Task Test1Async()
        {
            //var request = new HttpRequestMessage(new HttpMethod(method), "/chat/dialogs");
            //var response = _client.Send(request);
            //var response = await _fixture.Client.GetAsync("/chat/dialogs");
            var result = await _chat.GetDialogsAsync(ACCOUNT);

            System.Console.WriteLine();
        }
    }
}
