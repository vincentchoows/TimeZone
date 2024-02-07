using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BCrypt.Net;

namespace TimeZone_Assign.front_end
{
    public partial class forgot_password_input : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            watchEntities2 db = new watchEntities2();

            string email = txtEmail.Text;
            string securityAns = txtSecurityQuestion.Text;
            string password = txtReenterPassword.Text;


            //check account existence 
            CUSTOMER customer = new CUSTOMER();
            try
            {
                customer = db.CUSTOMERs.SingleOrDefault(c => c.EMAIL == email);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }

            //existence check
            if (customer != null)
            {
                Random random = new Random();
                int code = random.Next(100000, 999999);
                string securitySet = code.ToString();

                btnReset.CommandArgument = securitySet;

                string recipientEmail = email;
                string subject = "Your review has been replied - TimeZone";
                string body = $@"
                                Dear {customer.NAME},
                                <br><br>
                                Your SECUTIRY CODE IS {securitySet}. 
                                <br>
                                If you have any further questions or concerns, please do not hesitate to contact us.
                                <br><br>
                                Best regards,
                                <br>
                                TimeZone
                                ";

                SendEmail(recipientEmail, subject, body);

                lblTest.Text = recipientEmail;
            }
            else
            {
                cvAccount.IsValid = false;
            }
        }

        protected bool validationCustomer(string email, string password)
        {
            string regexEmail = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            string regexPassword = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$";

            if (Regex.IsMatch(email, regexEmail) &&
                Regex.IsMatch(password, regexPassword))
            {
                //if all matches
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CompareSixDigitCodes(string code1, string code2)
        {
            if (code1 == code2) return 100.0;

            return 0.0;
        }

        private void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                string mailSender = "timezoneeeeee@gmail.com";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailSender);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(mailSender, "pwmgkwmmwgghawjv");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("An error occurred while sending the email: {0}", ex.ToString());
                System.Diagnostics.Debug.WriteLine(errorMessage);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string securitySet = btn.CommandArgument;

            watchEntities2 db = new watchEntities2();

            string email = txtEmail.Text;
            string securityAns = txtSecurityQuestion.Text;
            string password = txtReenterPassword.Text;

            //server - side validations
            if (!validationCustomer(email, password))
            {
                return;
            }

            //check account existence 
            CUSTOMER customer = new CUSTOMER();
            try
            {
                customer = db.CUSTOMERs.SingleOrDefault(c => c.EMAIL == email);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }

            //existence check
            if (customer != null)
            {


                

                double matchPercentage = CompareSixDigitCodes(securitySet, securityAns);

                if (matchPercentage == 100)
                {
                    //hash the password and store into db
                    string hashedPassword = Encryption.HashPassword(password);


                    customer.PASSWORD = hashedPassword;
                    db.SaveChanges();


                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Password successfully reset!'); window.location.href = 'user-login.aspx';", true);
                }
                else
                {
                    cvMatch.IsValid = false;
                }
            }
            else
            {
                cvAccount.IsValid = false;
            }
        }
    }
}