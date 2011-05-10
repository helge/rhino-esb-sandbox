using System;
using System.IO;
using System.Linq;
using Messages;
using Publisher;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Msmq;
using StructureMap;
using Utils;

namespace Subscriber {
	internal class Program {
		private static void Main(string[] args) {
			//TestMain();
			//return;
			QueueUtil.Prepare("msmq://localhost/rhino_subscriber", QueueType.Standard);
			QueueUtil.Prepare("msmq://localhost/audit.queue", QueueType.Standard);

			var host = new DefaultHost();
			host.Start<SubscriberBootStrapper>();


		    var lBus = host.Bus as IServiceBus;
            if (lBus == null) {
                return;
            }
			Console.WriteLine("Subscriber is listening...");

            while (true) {
                var lLine = Console.ReadLine();
                if (lLine.StartsWith("q")) {
                    return;
                }
                lBus.Send(new HelloWorldMessage { Say = "Hello World!" });
                Console.WriteLine("Sending HelloWorld!");
            }
		}

		private static void TestMain() {
			var container = ObjectFactory.Container;
			container.Configure(c => c.Scan(s => {
			  s.TheCallingAssembly();
			  s.AddAllTypesOf<IMessageConsumer>().NameBy(t => t.FullName);
				s.Exclude(t => typeof(IOccasionalMessageConsumer).IsAssignableFrom(t) == true);
			                                }));

			var lQuery = from lType in typeof(HelloWorldHandler).Assembly.GetTypes()
									 where typeof(IMessageConsumer).IsAssignableFrom(lType)
			             select lType.FullName;

			var q = from l in container.Model.AllInstances
			        select l;
			foreach (var lInstanceRef in q) {
				Console.WriteLine(lInstanceRef.PluginType + " is " + lInstanceRef.Name);
			}

			//Console.WriteLine(string.Join(Environment.NewLine, lQuery));
			File.WriteAllText("container.log", container.WhatDoIHave());
			Console.ReadLine();
		}
	}
}