﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace TimeZone_Assign
{
    public class Security
    {
        public static string GetHash(string strPass)
        {
            //convert string into byte array
            byte[] binPass = Encoding.Default.GetBytes(strPass);

            //SHA hash algorithm
            SHA256 sha = SHA256.Create();

            //convert ori binPass to hashPass in hash format
            byte[] binHash = sha.ComputeHash(binPass);

            //convert hash in bin > string
            string strHash = Convert.ToBase64String(binHash);

            return strHash;

        }

        public static void LoginUser(string username, string role, bool rememberMe)
        {
            HttpContext ctx = HttpContext.Current;

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, rememberMe);
            FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                oldTicket.Version,
                oldTicket.Name,
                oldTicket.IssueDate,
                oldTicket.Expiration,
                oldTicket.IsPersistent,
                role
            );
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            ctx.Response.Cookies.Add(authCookie);

            string redirectUrl = FormsAuthentication.GetRedirectUrl(username, rememberMe);
            //ctx.Response.Redirect(redirectUrl);

            if(role == "Employee")
            {
                //ctx.Response.Redirect("~/back_end/BackEndHomePage.aspx");
                ctx.Response.Redirect("~/back_end/record/customer/CustomerRecord.aspx");
            }
            else
            {
                ctx.Response.Redirect(redirectUrl);
            }
            






            //if (role == "Customer")
            //{
            //    if (redirectUrl.Contains("front-end"))
            //    {
            //        ctx.Response.Redirect(redirectUrl);

            //    }
            //    else if (redirectUrl.Contains("back_end"))
            //    {
            //        ctx.Response.Redirect("~/front-end/home.aspx");
            //    }
            //}
            //else // role == "Employee"
            //{

            //    if (redirectUrl.Contains("front-end"))
            //    {
            //        ctx.Response.Redirect("~/back_end/BackEndHomePage.aspx");

            //    }
            //    else if (redirectUrl.Contains("back_end"))
            //    {
            //        ctx.Response.Redirect(redirectUrl);
            //    }
            //}

            //if (role == "Employee")
            //{
            //    ctx.Response.Redirect("~/back_end/BackEndHomePage.aspx");
            //}
            //else // role == "Customer"
            //{
            //    //ctx.Response.Redirect(redirectUrl);
            //    ctx.Response.Redirect("~/front-end/home.aspx");
            //}


        }

        public static void ProcessRoles()
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.User != null &&
                ctx.User.Identity.IsAuthenticated &&
                ctx.User.Identity is FormsIdentity)
            {
                FormsIdentity identity = (FormsIdentity)ctx.User.Identity;
                string[] roles = identity.Ticket.UserData.Split(',');

                GenericPrincipal principal = new GenericPrincipal(identity, roles);
                ctx.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }

    }
}