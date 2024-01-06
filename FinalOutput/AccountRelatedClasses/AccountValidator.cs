using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    public static class AccountValidator
    {

        /// <summary>
        /// Return true if the account is registered.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string username, string password)
        {
            var costumerList = FileManager.GetCostumerAccounts();

            return costumerList.Any(account => account.Username.ToLower() == username.ToLower() && account.Password == password);
        }


        public static bool DoesCostumerAccountExist(string username)
        {

            var costumerList = FileManager.GetCostumerAccounts();

            return costumerList.Any(user => user.Username.ToLower() == username.ToLower());

        }
        
        //Work upon
        //public static bool DoesAdminAccountExist(string username)
        //{

        //    var costumerList = FileManager.GetAdminAccounts();

        //    return costumerList.Any(user => user.Username == username);

        //}

    }
}
