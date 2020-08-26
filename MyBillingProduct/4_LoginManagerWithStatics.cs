using System;
using System.Collections;
using Step1Mocks;

namespace MyBillingProduct
{
	public class LoginManagerWithStatics
	{
	    private Hashtable m_users = new Hashtable();

	    public bool IsLoginOK(string user, string password)
	    {
	        try {
		        LoggerWrite("user [username] logged in ok");
	        }
	        catch (LoggerException e) {
		        WebServiceWrite(e.Message + GetMachineName());
	        }
	        if (m_users[user] != null &&
	            (string) m_users[user] == password)
	        {
	            return true;
	        }
	        return false;
	    }

	    protected virtual string GetMachineName() {
		    return Environment.MachineName;
	    }

	    protected virtual void WebServiceWrite(string message) {
		    StaticWebService.Write(message);
	    }

	    protected virtual void LoggerWrite(string message) {
		    StaticLogger.Write(message);
	    }

	    public void AddUser(string user, string password)
	    {
	        m_users[user] = password;
	    }

	    public void ChangePass(string user, string oldPass, string newPassword)
		{
			m_users[user]= newPassword;
		}
	}
}
