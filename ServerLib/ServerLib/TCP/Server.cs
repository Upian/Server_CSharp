using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP
{
	public class Server
	{
		private Socket? _socket = null;
		private DefaultObjectPool<SocketAsyncEventArgs>? _readEventPool = null;
		private Acceptor _acceptor = new Acceptor();

		public Server() 
		{
			Policy.SocketAsyncEventArgPolicy policy = new Policy.SocketAsyncEventArgPolicy(1024, HandleIOCompleted);
			_readEventPool = new DefaultObjectPool<SocketAsyncEventArgs>(policy);
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
		
		private void StartAccept()
		{
			SocketAsyncEventArgs acceptEventArg = new SocketAsyncEventArgs();
			acceptEventArg.Completed += (sender, e) =>
			{
				ProcessAccept(e);
			};

			if (false == _socket.AcceptAsync(acceptEventArg))
			{
				ProcessAccept(acceptEventArg);
			}
		}

		private void ProcessAccept(SocketAsyncEventArgs e)
		{
			Console.WriteLine("Accept");
			if (SocketError.Success != e.SocketError || 
				null == e.AcceptSocket)
			{
				Console.WriteLine($"Accept 실패: {e.SocketError}");
				e.AcceptSocket = null;

				if(false == _socket.AcceptAsync(e))
				{
					ProcessAccept(e);
				}
				return;
			}

			Socket clientSocket = e.AcceptSocket;
			var readEvent = _readEventPool.Get();
			if(null == readEvent)
			{
				Console.WriteLine("ERROR");
				return;
			}
			readEvent.UserToken = clientSocket; //나중에 ClientSession으로
			readEvent.AcceptSocket = clientSocket;

			e.AcceptSocket = null;
		}

		private void HandleIOCompleted(object sender, SocketAsyncEventArgs e)
		{
		}
	}
}