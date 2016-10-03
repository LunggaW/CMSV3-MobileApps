using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS
{
    public interface IServiceWrapper
    {
        Task<TokenModel> GetAuthorizationTokenData(string username, string password);
        Task<User> GetUserData(string username, string password, string authenticationToken);
        Task<bool> UploadSales(User user, List<Transaction> trans);
    }
}
