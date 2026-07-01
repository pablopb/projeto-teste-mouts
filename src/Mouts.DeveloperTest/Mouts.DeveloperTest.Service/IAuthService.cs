using System;
using System.Collections.Generic;
using System.Text;

namespace Mouts.DeveloperTest.Service
{
    public interface IAuthService
    {
        Task<string> Authenticate(string userName, string password);
    }
}
