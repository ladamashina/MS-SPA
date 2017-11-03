
using System.Web;


namespace GeekQuiz.Services
{
    public class MyAccountServicecs : IAccountService
    {
        public ApplicationSignInManager SingInInteface(ApplicationSignInManager UserName)
        {
            return UserName; //?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        }
    }
}