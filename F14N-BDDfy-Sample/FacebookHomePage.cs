using FluentAutomation;

namespace F14N_BDDfy_Sample
{
    public class FacebookHomePage : PageObject<FacebookLoginPage> {
        public FacebookHomePage(FluentTest test) : base(test) {}
    }
}