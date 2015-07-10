using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API
{
    public class AuthContext : IdentityDbContext<IdentityUser> //Special version of the DbContext class
                                                                //Provides the code-first mapping and dbset props
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}