using System;
using Rhino.ServiceBus.Autofac;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.Msmq;
using Rhino.ServiceBus.StructureMap;
using Utils;

namespace Publisher.balancer
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueUtil.Prepare("msmq://localhost/rhino_publisher.balancer", QueueType.Standard);
            QueueUtil.Prepare("msmq://localhost/rhino_publisher.balancer.acceptingwork", QueueType.Standard);

            var host = new DefaultHost();
            host.Start<StructureMapLoadBalancerBootStrapper>();

            Console.WriteLine("Publisher.balancer: Gestartet ...");

            while (true)
            {
                var lLine = Console.ReadLine();
                if (lLine.StartsWith("q"))
                {
                    return;
                }
            }
        }
    }
}
