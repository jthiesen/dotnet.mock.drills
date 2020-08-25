using System;
using System.Collections;

namespace MyBillingProduct
{
	public class LoginManager1
	{
	    private Hashtable m_users = new Hashtable();
        private ILogger m_log;
        private readonly IWebService m_webService;

        public LoginManager1(ILogger log, IWebService fakeWebService) {
	        m_log = log;
	        m_webService = fakeWebService;
        }

        public bool IsLoginOK(string user, string password)
	    {
		    try {
			    if (m_users[user] != null &&
			        (string) m_users[user] == password) {
				    m_log.Write($"login ok: user: [{user}]");
				    return true;

			    }
			    m_log.Write($"bad login: [{user}],[{password}]");
			    return false;
		    }catch (LoggerException) {
			    m_webService.Write("got exception - [message]");
			    return false;
		    }
	    }

	    public void AddUser(string user, string password)
	    {
	        m_users[user] = password;
	        // m_log.Write($"user added: [{user}],[{password}]");
	    }

	    public void ChangePass(string user, string oldPass, string newPassword)
		{
			m_users[user]= newPassword;
		}
	}
}
