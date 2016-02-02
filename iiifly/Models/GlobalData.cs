using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
            return GetEntry(_userIdToPublicPath, userId, GetPublicPathInternal);
        }

        private static string GetPublicPathInternal(string userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.Find(userId);
                if (string.IsNullOrWhiteSpace(user.PublicPathName))
                {
                    var s = "7wteja" + userId + "85ghgl";
                    user.PublicPathName = string.Format("{0:X}", s.GetHashCode());
                    db.SaveChanges();
                }
                return user.PublicPathName;
            }
        }

        public static string GetUserIdFromPublicPath(string publicPath)
        {
            return GetEntry(_publicPathToUserId, publicPath, GetUserIdInternal);
        }

        private static string GetUserIdInternal(string publicPath)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.PublicPathName == publicPath);
                if (user == null) return null;
                return user.Id;
            }
        }

        private static string GetEntry(Dictionary<string, string> dict, string key, Func<string, string> makeValue)
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
                var value = makeValue(key);
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }
                dict[key] = value;
            }
            return dict[key];
        }

        private const int ImageSetIdLength = 8;

        public static bool IsValidImageSetId(string isid)
        {
            if (string.IsNullOrWhiteSpace(isid))
            {
                return false;
            }
            return isid.Length == ImageSetIdLength;
        }

        public static string GenerateImageSetId()
        {
            return Guid.NewGuid().ToString().Substring(0, ImageSetIdLength);
        }
    }
}
