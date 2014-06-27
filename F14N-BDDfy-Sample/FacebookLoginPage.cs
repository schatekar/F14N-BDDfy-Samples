using FluentAutomation;

namespace F14N_BDDfy_Sample
{
    public class FacebookLoginPage : PageObject<FacebookLoginPage>
    {
        private const string EmailInput = "input[id='email']";
        private const string PasswordInput = "input[id='pass']";
        private const string LoginButton = "input[value='Log in']";

        public FacebookLoginPage(FluentTest test) : base(test)
        {
            Url = "http://www.facebook.com";
            At = () => I.Expect.Exists(EmailInput);
        }

        public FacebookHomePage Login(string email, string password)
        {
            I.Enter(email).In(EmailInput);
            I.Enter(password).In(PasswordInput);
            I.Click(LoginButton);

            if (I.Find(LoginButton) != null) return null;

            return Switch<FacebookHomePage>();
        }
    }
}
