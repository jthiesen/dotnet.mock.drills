using System;
using NUnit.Framework;

namespace MyBillingProduct.UnitTests {
    public class LoginManagerWithStaticsTest {
        [Test]
        public void IsLoginOK_WhenLoginOK_StaticLoggerWriteLogsMessage() {
            var loginManager = new TestableLoginManagerWithLogging();

            loginManager.IsLoginOK("user", "pass");
            
            Assert.AreEqual("user [username] logged in ok", loginManager.LastLoggerMessage);
        }
        
        [Test]
        public void IsLoginOK_WhenLoggerThrowsException_WebSericeWriteLogsMessageContainingExceptionMessage() {
            var loginManager = new TestableLoginManagerWithLogging();
            loginManager.SimulateLoggerWriteException = true;

            loginManager.IsLoginOK("user", "pass");
            
            StringAssert.Contains("exceptionmessage", loginManager.LastWebServiceMessage);
        }        
        
        [Test]
        public void IsLoginOK_WhenLoggerThrowsException_WebSericeWriteLogsMessageContainingCurrentMachineName() {
            var loginManager = new TestableLoginManagerWithLogging();
            loginManager.SimulateLoggerWriteException = true;

            loginManager.IsLoginOK("user", "pass");
            
            StringAssert.Contains("currentmachinename", loginManager.LastWebServiceMessage);
        }
    }

    public class TestableLoginManagerWithLogging : LoginManagerWithStatics {
        public string LastLoggerMessage = "";
        public string LastWebServiceMessage = "";
        public bool SimulateLoggerWriteException = false;

        protected override void LoggerWrite(string message) {
            if (SimulateLoggerWriteException) {
                throw new LoggerException("exceptionmessage");
            }
            LastLoggerMessage = message;
        }

        protected override void WebServiceWrite(string message) {
            LastWebServiceMessage = message;
        }

        protected override string GetMachineName() {
            return "currentmachinename";
        }
    }
}
