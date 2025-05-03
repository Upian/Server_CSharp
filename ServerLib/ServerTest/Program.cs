using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLib;

namespace ServerTest
{
	class Program
	{
		private static void Main()
		{
			Console.WriteLine("Start");
			ServerLib.TCP.Server server = new ServerLib.TCP.Server();
			server.AddAcceptor(8888);
			server.Start();

			Console.ReadLine();
		}
	}
}
