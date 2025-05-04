using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP
{
	public class Server<T_Session>
		where T_Session : Session, new()
	{
		private SessionManager<T_Session> _sessionManager;
		private Acceptor _acceptor;
		public Server() 
		{
			_sessionManager = new SessionManager<T_Session>();
			_acceptor = new Acceptor(_sessionManager);
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