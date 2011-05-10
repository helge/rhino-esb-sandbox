using System;
using Messages;
using Rhino.ServiceBus;

namespace Subscriber {
	public class HelloWorldHandler : ConsumerOf<ResponseMessage> {
		#region ConsumerOf<HelloWorldMessage> Members
        public void Consume(ResponseMessage message)
        {
			Console.WriteLine("Anwort erhalten: " + message.Response);
		}
		#endregion
	}
}