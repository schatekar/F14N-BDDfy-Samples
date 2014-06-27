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
                 .Given(s => s.GivenIAmOnFacebookLoginPage())
                 .And(s => s.AndILoginUsingEmailAndPassword("a@b.com", "password"), "And I login using email '{0}' and password '{1}'")
                 .Then(s => s.ThenShowsMessage("Incorrect email address"), "Then error message '{0}' is displayed")
                 .BDDfy();
         }
        [Test]
        public void PasswordIsMandatory()
        {
            new FacebookLoginStory()
                .Given(s => s.GivenIAmOnFacebookLoginPage())
                .And(s => s.AndILoginUsingEmailAndPassword("a@b.com", ""), "I login using email '{0}' and password '{1}'")
                .Then(s => s.ThenShowsMessage("Please re-enter your password"), "Then error message '{0}' is displayed")
                .BDDfy();
        }

        private void ThenShowsMessage(string message)
        {
            I.Assert.Text(message);
        }

        private void AndILoginUsingEmailAndPassword(string email, string password)
        {
            I.Enter(email).In(EmailInput);
            I.Enter(password).In(PasswordInput);
            I.Click(LoginButton);
        }

        private void GivenIAmOnFacebookLoginPage()
        {
            I.Open("http://www.facebook.com");
        }
    }
}