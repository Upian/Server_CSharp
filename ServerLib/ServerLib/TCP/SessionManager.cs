using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
using ServerLib.TCP.Policy;

namespace ServerLib.TCP
{
	internal class SessionManager<T_Session>
		where T_Session : Session, new()
	{
		private DefaultObjectPool<T_Session> _sessionPool;
		public SessionManager() 
		{
			Policy.SessionPoolPolicy<T_Session> policy = new Policy.SessionPoolPolicy<T_Session>();
			_sessionPool = new DefaultObjectPool<T_Session>(policy);
		}

		public Session CreateSession()
		{
			var session = _sessionPool.Get();
			return session;
		}

		
		public void ReleaseSession(T_Session session)
		{
			_sessionPool.Return(session);
		}
	}
}
