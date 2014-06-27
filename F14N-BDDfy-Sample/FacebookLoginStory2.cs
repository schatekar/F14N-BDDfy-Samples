using FluentAutomation;
using NUnit.Framework;
using TestStack.BDDfy;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace F14N_BDDfy_Sample
{
    [TestFixture]
    public class FacebookLoginStory2 : FluentTest
    {
        private FacebookLoginPage facebookLoginPage;
        private FacebookHomePage facebookHomePage;

        public FacebookLoginStory2()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
            FluentSession.EnableStickySession();
        }

        [Test]
        public void CannotLoginToFacebookWithInvalidCrdentials()
         {
             new FacebookLoginStory2()
                 .Given(s => s.GivenIAmOnFacebookLoginPage())
                 .And(s => s.AndILoginUsingEmailAndPassword("a@b.com", "password"), "And I login using email '{0}' and password '{1}'")
                 .Then(s => s.ThenShowsMessage("Incorrect email address"))
                 .BDDfy();
         }
        [Test]
        public void PasswordIsMandatory()
        {
            new FacebookLoginStory2()
                .Given(s => s.GivenIAmOnFacebookLoginPage())
                .And(s => s.AndILoginUsingEmailAndPassword("a@b.com", ""), "And I login using email '{0}' and password '{1}'")
                .Then(s => s.ThenShowsMessage("Please re-enter your password"), "Then error message '{0}' is displayed")
                .BDDfy();
        }

        private void ThenShowsMessage(string message)
        {
            I.Assert.Text(message);
        }

        private void AndILoginUsingEmailAndPassword(string email, string password)
        {
            facebookHomePage = facebookLoginPage.Login(email, password);
        }

        private void GivenIAmOnFacebookLoginPage()
        {
            facebookLoginPage = new FacebookLoginPage(this).Go();
        }

        public void TearDown()
        {
            I.Dispose();
        }
    }
}