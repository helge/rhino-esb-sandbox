using System;
using System.Diagnostics;
using log4net;
using log4net.Config;
using Messages;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.Msmq;
using Utils;

namespace Publisher {
	internal class Program {
		//private static readonly ILog log = LogManager.GetLogger(typeof(Program));

		private static void Main(string[] pArgs) {
		//  Debugger.Launch();
		//  // Set up a simple configuration that logs on the console.
		//  XmlConfigurator.Configure(new System.IO.FileInfo("Publisher.log4net.xml"));

		//  log.Info("TEST");
		//  //Debugger.Break();
		  QueueUtil.Prepare("msmq://localhost/rhino_publisher", QueueType.Standard);

		  var host = new DefaultHost();
		  host.Start<PublisherBootStrapper>();

		  Console.WriteLine("Publisher: Hit Enter to publish ...");

		  var bus = host.Bus as IServiceBus;
		  if (bus == null) {
		    return;
		  }

		  while (true) {
		  	var lLine = Console.ReadLine();
				if (lLine.StartsWith("q")) {
					return;
				}
		  	bus.Publish(new HelloWorldMessage {
		  	                                  	Say = "Hello World!!! " + Guid.NewGuid()
		  	                                  });
		  }
		}
	}
}