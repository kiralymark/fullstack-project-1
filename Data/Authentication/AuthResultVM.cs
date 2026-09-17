using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_project_1.Data.Authentication
{
    public class AuthResultVM
    {
        // Authentication Result View Model

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

    }

}
