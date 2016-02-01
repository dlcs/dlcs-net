using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using iiifly.Util;
using Microsoft.AspNet.Identity;

namespace iiifly.Models
{
    public static class GlobalData
    {
        private static readonly object Lock = new object();

        private static Dictionary<string, string> _userIdToPublicPath = new Dictionary<string, string>();
        private static Dictionary<string, string> _publicPathToUserId = new Dictionary<string, string>();

        public static string GetPublicPath(this IPrincipal user)
        {
            return GetPublicPath(user.Identity.GetUserId());
        }

        public static string GetPublicPath(string userId)
        {
            return GetEntry(_userIdToPublicPath, userId, true);
        }

        public static string GetUserIdFromPublicPath(string publicPath)
        {
            return GetEntry(_publicPathToUserId, publicPath, false);
        }

        private static string GetEntry(Dictionary<string, string> dict, string key, bool encrypt)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            lock (Lock)
            {
                if (dict.ContainsKey(key))
                {
                    return dict[key];
                }
                var aes = new SimpleAES();
                string value = encrypt ? aes.EncryptToString(key) : aes.DecryptString(key);
                dict[key] = value;
            }
            return dict[key];
        }
    }
}
