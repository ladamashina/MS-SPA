using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using GeekQuiz.Models;


namespace GeekQuiz.Services
{
    public interface IAccountService
    {
        ApplicationSignInManager SingInInteface(ApplicationSignInManager UserName);
    }
}



