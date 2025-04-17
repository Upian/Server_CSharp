using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP
{
	public class Server
	{
		private Socket? m_socket = null;
		private DefaultObjectPool<SocketAsyncEventArgs>? _readEventPool = null;
		private Policy.SocketAsyncEventArgPolicy? _policy = null;
		public Server() 
		{
			m_socket = new Socket();
			_policy = new Policy.SocketAsyncEventArgPolicy(1024, HandleCompleted);
//			m_readEventPool = new DefaultObjectPool<SocketAsyncEventArgs>();
		}

		public void Start(ushort _port)
		{
			m_socket.Bind(_port);
			m_socket.Listen();
		}
		
		private void HandleCompleted(object sender, SocketAsyncEventArgs e)
		{
		}
	}
}