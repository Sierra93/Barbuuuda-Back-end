using Barbuuuda.Core.Interfaces;

namespace Barbuuuda.Tests
{
    public class BaseServiceTest
    {
        private readonly IChat _chat;

        protected BaseServiceTest(IChat chat)
        {
            _chat = chat;
        }
    }
}
