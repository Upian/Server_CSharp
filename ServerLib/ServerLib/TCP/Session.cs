using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerLib.TCP
{
	//연결된 클라이언트 객체
	internal class Session
	{
		private SocketAsyncEventArgs _socketAsyncEventArg;
		public SocketAsyncEventArgs SocketAsyncEventArg
		{
			get => _socketAsyncEventArg;
			set => _socketAsyncEventArg = value;
		}
		public Session() 
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
		}
		private void HandleIOCompleted(object sender, SocketAsyncEventArgs e)
		{

		}

	}
}
