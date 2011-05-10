using System;
using Messages;
using Rhino.ServiceBus;

namespace Subscriber {
	public class HelloWorldHandler : ConsumerOf<HelloWorldMessage> {
	    private IServiceBus mBus;

	    public HelloWorldHandler(IServiceBus pBus) {
            mBus = pBus;
        }
		#region ConsumerOf<HelloWorldMessage> Members
		public void Consume(HelloWorldMessage message) {
			Console.WriteLine("Message erhalten: " + message.Say);
			Console.WriteLine("Sende Antwort: " + "Antwort");
            mBus.Publish(new ResponseMessage{Response = "Antwort"});
		}
		#endregion
	}
}