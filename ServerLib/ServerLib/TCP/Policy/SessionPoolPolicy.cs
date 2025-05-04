using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;

namespace ServerLib.TCP.Policy
{
	internal class SessionPoolPolicy<T_Session> : IPooledObjectPolicy<Session>
	{
		public SessionPoolPolicy()
		{

		}

		public Session Create()
		{
			var e = new Session();
			return e;
		}

		public bool Return(Session obj)
		{
			//반환 전 초기화
			obj.Reset();
			
			return true; //풀에 다시 넣을지 여부
		}
	}
}
