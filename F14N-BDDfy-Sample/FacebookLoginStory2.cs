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
                 .Given(s => s.IAmOnFacebookLoginPage())
                 .And(s => s.ILoginUsingEmailAndPassword("a@b.com", "password"), "I login using email '{0}' and password '{1}'")
                 .Then(s => s.ShowsMessage("Incorrect email address"))
                 .BDDfy();
         }
        [Test]
        public void PasswordIsMandatory()
        {
            new FacebookLoginStory2()
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
            facebookHomePage = facebookLoginPage.Login(email, password);
        }

        private void IAmOnFacebookLoginPage()
        {
            facebookLoginPage = new FacebookLoginPage(this).Go();
        }

        public void TearDown()
        {
            I.Dispose();
        }
    }
}