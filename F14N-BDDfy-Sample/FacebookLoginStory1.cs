using FluentAutomation;
using NUnit.Framework;
using TestStack.BDDfy;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace F14N_BDDfy_Sample
{
    [TestFixture]
    public class FacebookLoginStory : FluentTest
    {
        private const string EmailInput = "input[id='email']";
        private const string PasswordInput = "input[id='pass']";
        private const string LoginButton = "input[value='Log in']";

        public FacebookLoginStory()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
            FluentSession.EnableStickySession();
        }

        [Test]
        public void CannotLoginToFacebookWithInvalidCrdentials()
         {
             new FacebookLoginStory()
                 .Given(s => s.IAmOnFacebookLoginPage())
                 .And(s => s.ILoginUsingEmailAndPassword("a@b.com", "password"), "I login using email '{0}' and password '{1}'")
                 .Then(s => s.ShowsMessage("Incorrect email address"))
                 .BDDfy();
         }
        [Test]
        public void PasswordIsMandatory()
        {
            new FacebookLoginStory()
                .Given(s => s.IAmOnFacebookLoginPage())
                .And(s => s.ILoginUsingEmailAndPassword("a@b.com", ""), "I login using email '{0}' and password '{1}'")
                .Then(s => s.ShowsMessage("Please re-enter your password"), "Error message '{0}' is displayed")
                .BDDfy();
        }

        private void ShowsMessage(string message)
        {
            I.Assert.Text(message);
        }

        private void ILoginUsingEmailAndPassword(string email, string password)
        {
            I.Enter(email).In(EmailInput);
            I.Enter(password).In(PasswordInput);
            I.Click(LoginButton);
        }

        private void IAmOnFacebookLoginPage()
        {
            I.Open("http://www.facebook.com");
        }
    }
}