using Shouldly;
using Xunit;

namespace CarSpot.Tests.Framework
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void test() 
        { 
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IMessenger,Messenger>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var messenger = serviceProvider.GetService<IMessenger>();

            messenger.Send();
            messenger.ShouldNotBeNull();

            var messenger2 = serviceProvider.GetService<IMessenger>();

            messenger2.Send();

            messenger2.ShouldNotBeNull();
        }

        private interface IMessenger
        {
            void Send();
        }

        private class Messenger : IMessenger
        {
            private readonly Guid _id = Guid.NewGuid();
            public void Send() => Console.WriteLine($"Sending a message... {_id}");
        }
    }
}
