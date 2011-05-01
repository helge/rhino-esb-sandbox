using System;
using Messages;
using Rhino.ServiceBus;

namespace Subscriber {
	public class HelloWorldHandler : ConsumerOf<HelloWorldMessage> {
		#region ConsumerOf<HelloWorldMessage> Members
		public void Consume(HelloWorldMessage message) {
			Console.WriteLine(message.Say);
		}
		#endregion
	}
}