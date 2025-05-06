using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.ObjectPool;
using ServerLib.TCP.Policy;

namespace ServerLib.TCP
{
	//연결된 IO 관리
	public class IOHandler
	{
		//static
		private static readonly DefaultObjectPool<IOHandler> _IOHandlerPool 
			= new DefaultObjectPool<IOHandler>(new IOHandlerPoolPolicy());

		public static IOHandler CreateIOHandler() => _IOHandlerPool.Get();
		public static void ReleaseIOHandler(IOHandler obj) => _IOHandlerPool.Return(obj);
		
		///////
		private SocketAsyncEventArgs _socketAsyncEventArg;
		
		public IOHandler() 
		{
			_socketAsyncEventArg = new SocketAsyncEventArgs();
			_socketAsyncEventArg.SetBuffer(new byte[Common.BufferSize], 0, Common.BufferSize);// 버퍼 설정

			_socketAsyncEventArg.Completed += this.HandleIOCompleted;
		}

		//풀 반환시 초기화
		public void Reset()
		{
			_socketAsyncEventArg.SetBuffer(_socketAsyncEventArg.Buffer, 0, Common.BufferSize);
			_socketAsyncEventArg.AcceptSocket = null;
			_socketAsyncEventArg.UserToken = null;
		}

		//초기 설정
		public void Initialize(Socket socket)
		{
			_socketAsyncEventArg.AcceptSocket = socket;
			_socketAsyncEventArg.UserToken = socket;
		}
		private void HandleIOCompleted(object sender, SocketAsyncEventArgs e)
		{
			switch (e.LastOperation)
			{
				case SocketAsyncOperation.Receive:
					if(0 == e.BytesTransferred ||
						SocketError.Success != e.SocketError)
					{
						Console.WriteLine("Client disconnect");
						//처리 필요
						break;
					}
					this.HandleRecieve(e);
					break;
				case SocketAsyncOperation.Send:
					this.HandleSend(e);
					break;
			}
		}

		public void Start()
		{
			bool result = _socketAsyncEventArg.AcceptSocket.ReceiveAsync(_socketAsyncEventArg);
			if (false == result)
				this.HandleRecieve(_socketAsyncEventArg);
		}

		protected void HandleSend(SocketAsyncEventArgs e)
		{

		}
		protected void HandleRecieve(SocketAsyncEventArgs e)
		{

		}
	}
}
