using System.Runtime.InteropServices;
using NUnit.Framework;

namespace MyBillingProduct.UnitTests
{
    //rename this class as needed
    public class Class1
    {
        [TestCase("a", "pass")]
        [TestCase("b", "pass")]
        public void IsLoginOK_IfCredentialsCorrect_ShouldLogOK(string user, string pass)
        {
            var log = new FakeLogger();
            LoginManager1 lm = new LoginManager1(log);
            lm.AddUser(user, pass);
            lm.IsLoginOK(user, pass);
            Assert.That(log.GetLastLog().Equals($"login ok: user: [{user}]"));
        }

        [TestCase("b", "pass")]
        [TestCase("c", "pass")]
        public void IsLoginOK_IfUnknwonUser_ShouldLogBadLogin(string user, string pass)
        {
            var log = new FakeLogger();
            LoginManager1 lm = new LoginManager1(log);
            lm.AddUser("a", pass);
            lm.IsLoginOK(user, pass);
            Assert.AreEqual(log.GetLastLog(), $"bad login: [{user}],[{pass}]");
        }

        [Test]
        public void IsLoginOK_IfLoggerThrowsException_NotifyWebService() 
        {
    
                
        }
    }

    public class FakeLogger : ILogger
    {
        private string loggedMessage = "";

        public string GetLastLog()
        {
            return loggedMessage;
        }

        public void Write(string text)
        {
            loggedMessage = text;
        }
    }
}