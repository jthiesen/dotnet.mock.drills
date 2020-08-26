using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace MyBillingProduct.UnitTests
{
    //rename this class as needed
    public class LoginManager1Tests
    {
        [TestCase("a", "pass")]
        [TestCase("b", "pass")]
        public void IsLoginOK_IfCredentialsCorrect_ShouldLogOK(string user, string pass)
        {
            var log = new FakeLogger();
            var webservice = new FakeWebService();
            LoginManager1 lm = new LoginManager1(log, webservice);
            lm.AddUser(user, pass);
            lm.IsLoginOK(user, pass);
            Assert.That(log.GetLastLog().Equals($"login ok: user: [{user}]"));
        }

        [TestCase("b", "pass")]
        [TestCase("c", "pass")]
        public void IsLoginOK_IfUnknwonUser_ShouldLogBadLogin(string user, string pass)
        {
            var log = new FakeLogger();
            var webservice = new FakeWebService();
            LoginManager1 lm = new LoginManager1(log, webservice);
            lm.AddUser("a", pass);
            lm.IsLoginOK(user, pass);
            Assert.AreEqual(log.GetLastLog(), $"bad login: [{user}],[{pass}]");
        }

        [TestCase("a", "pass")]
        public void IsLoginOK_IfLoggerThrowsException_NotifyWebService(string user, string pass) 
        {
            var log = new FakeLogger();
            log.simulateException = true;
            var webservice = new FakeWebService();
            LoginManager1 lm = new LoginManager1(log, webservice);
            lm.AddUser(user, pass);
            lm.IsLoginOK(user, pass);
            StringAssert.Contains(webservice.GetLastLog(), $"got exception - [message]");
        }

        // [TestCase("a", "pass")]
        // public void AddUser_IfLoggerThrowsException_NotifyWebService(string user, string pass) 
        // {
        //     var log = new FakeLogger();
        //     log.simulateException = true;
        //     var webservice = new FakeWebService();
        //     LoginManager1 lm = new LoginManager1(log, webservice);
        //     lm.AddUser(user, pass);
        //     StringAssert.Contains($"got exception - [message]",webservice.GetLastLog());
        // }
        //
        // [TestCase("a", "password")]
        // [TestCase("b", "password")]
        // public void AddUser_WhenCalled_ShouldLog(string user, string pass)
        // {
        //     var log = new FakeLogger();
        //     var webservice = new FakeWebService();
        //     LoginManager1 lm = new LoginManager1(log, webservice);
        //     lm.AddUser(user, pass);
        //     Assert.That(log.GetLastLog().Equals($"user added: [{user}],[{pass}]"));
        // }

    }

    public class FakeWebService : IWebService {
        private string loggedMessage = "";
        public void Write(string message) {
            loggedMessage = message;
        }
        
        public string GetLastLog()
        {
            return loggedMessage;
        }
    }

    public class FakeLogger : ILogger
    {
        private string loggedMessage = "";
        public bool simulateException = false;

        public string GetLastLog()
        {
            return loggedMessage;
        }

        public void Write(string text)
        {
            if (simulateException) {
                throw new LoggerException();
            }
            loggedMessage = text;
        }
    }
}