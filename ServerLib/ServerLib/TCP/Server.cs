using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP
{
	public class Server
	{
		private Acceptor _acceptor;
		public Server() 
		{
			_acceptor = new Acceptor();
		}

		public void AddAcceptor(ushort port)
		{
			_acceptor.Initialize(port);
		}
		public void Start()
		{
			_acceptor.Start();
			Console.WriteLine("Start listen");
			
//			this.StartAccept();
		}
	
	}
}