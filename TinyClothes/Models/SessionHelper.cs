using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public static class SessionHelper
    {
        private const string IdKey = "ID";
        private const string UsernameKey = "Username";

        /// <summary>
        /// Creates User Session
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="http"></param>
        public static void CreateUserSession(int id, string username, IHttpContextAccessor http)
        {
            // new is glue, works but with less flexability
            // parameter names are optional, used here to demonstrate usability
            http.HttpContext.Session.SetInt32(key: IdKey, value: id) ;
            http.HttpContext.Session.SetString(key: UsernameKey, value: username);
        }

        /// <summary>
        /// Log user out; destroy/end user session/token.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="http"></param>
        public static void DestroyUserSession(IHttpContextAccessor http)
        {
            http.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Returns true if user is logged in
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static bool IsUserLoggedIn(IHttpContextAccessor http)
        {
            // I prefer the single expression
            return http.HttpContext.Session.GetInt32(key: IdKey).HasValue;

            // alternate method using a variable to improve readability
            //int? memberID = http.HttpContext.Session.GetInt32("ID");
            //return memberID.HasValue;
        }
    }
}
