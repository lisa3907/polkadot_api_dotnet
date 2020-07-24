namespace PolkaTest
{
    using Polkadot.Api;
    using System;
    using Xunit;
    using Xunit.Abstractions;
    using System.Threading;
    using Polkadot.Data;
    using System.Numerics;

    [Collection("Sequential")]
    public class WssubscribeBalance
    {
        private readonly ITestOutputHelper output;

        public WssubscribeBalance(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Ok()
        {
            using (IApplication app = PolkaApi.GetApplication())
            {
                app.Connect(Constants.LocalNodeUri);
                BigInteger maxValue = (new BigInteger(1) << 128) - 1;

                var addr = Constants.LocalAliceAddress;
                bool doneS = false;
                BigInteger balanceResult = maxValue;
                var sid = app.SubscribeAccountInfo(addr.Symbols, (accountInfo) => {
                    var free = accountInfo.AccountData.Free;
                    output.WriteLine($"Balance: {free}");
                    Console.WriteLine($"\nBalance: {free}\n");
                    balanceResult = free;
                    doneS = true;
                });

                while (!doneS)
                {
                    Thread.Sleep(1000);
                }

                app.UnsubscribeAccountInfo(sid);

                app.Disconnect();

                Assert.True(balanceResult < maxValue);
            }
        }
    }
}
