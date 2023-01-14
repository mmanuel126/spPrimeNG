using System.Net;
using System.Net.Mail;
using sportprofiles.DBContextModels;
using sportprofiles.Models.Members;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using sportprofiles.Models.Organizations;

namespace sportprofiles.Repository
{
    /// <summary>
    /// describes the functionalities to manage the business and data requirements for Site Common usage needs
    /// </summary>
    public class CommonRepository : ICommonRepository
    {
        public readonly dbContexts _context;
        private readonly IConfiguration _configuration;
        public CommonRepository(dbContexts context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Get Recent news 
        /// </summary>
        /// <returns></returns>
        public List<TbRecentNews> GetRecentNews()
        {
            var q = (from r in _context.TbRecentNews orderby r.PostingDate descending select r).Take(8).ToList();
            return q;
        }


        /// <summary>
        /// Get states
        /// </summary>
        /// <returns></returns>
        public List<TbStates> GetStates()
        {
            var q = (from s in _context.TbStates orderby s.Name ascending select s).ToList();
            return q;
        }

        public void SendGenEmailToUser(string memberName, string fromEmail, string toEmail, string fromName, string toName, string subject, string msg)
        {

            string name = memberName;

            if (string.IsNullOrEmpty(memberName))
                name = _configuration.GetValue<string>("AppStrings:AppAdmin");

            //(1) Create the MailMessage instance
            fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, name); //IMPORTANT: This must be same as your smtp authentication address.
            mail.To.Add(toEmail);

            //(2) Assign the MailMessage's properties
            mail.Subject = subject;
            string appName = _configuration.GetValue<string>("AppStrings:AppName");
            string webSiteLink = _configuration.GetValue<string>("AppStrings:WebSiteLink"); 
            string pwd = _configuration.GetValue<string>("AppStrings:AppSMTPpwd");
            mail.Body = HTMLBodyText(fromName, toName, msg, appName, webSiteLink);
            mail.IsBodyHtml = true;

            //(3) Create the SmtpClient object
            string smtpHost = _configuration.GetValue<string>("AppStrings:AppSMTPHost");
            SmtpClient smtp = new SmtpClient(smtpHost);
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;

            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //IMPORANT:  Your smtp login email MUST be same as your FROM address.
            NetworkCredential Credentials = new NetworkCredential(fromEmail, pwd);
            smtp.Credentials = Credentials;

            //(4) Send the MailMessage
            smtp.Send(mail);
        }

        public List<SportsListModel> GetSportsList()
        {
            List<SportsListModel> sp = new List<SportsListModel>();
            var url = _configuration.GetValue<string>("AppStrings:ThirdPartySportsListAPI_URL");
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                dynamic? dynJson = JsonConvert.DeserializeObject(response);
                foreach (var item in dynJson.data)
                {
                    SportsListModel s = new SportsListModel();
                    s.Id = item.id;
                    s.Description = item.attributes.description;
                    s.Name = item.attributes.name;
                    sp.Add(s);
                }
                return sp.OrderBy(o => o.Name).ToList();
            }
        }

        public List<SchoolByStateModel> GetSchoolsByState(string state, string institutionType)
        {
            List<SchoolByStateModel> sM = new List<SchoolByStateModel>();
            if (institutionType == "1") //public
            {
                sM = (from s in _context.TbPublicSchools
                      where s.State == state
                      orderby s.SchoolName ascending

                      select new SchoolByStateModel()
                      {
                          SchoolId = s.SchoolId,
                          SchoolName = s.SchoolName
                      }
                        ).Distinct().ToList();
            }
            else if (institutionType == "2") //private
            {
                sM = (from s in _context.TbPrivateSchools
                      where s.State == state
                      orderby s.SchoolName ascending

                      select new SchoolByStateModel()
                      {
                          SchoolId = s.Lgid.ToString(),
                          SchoolName = s.SchoolName
                      }
                        ).Distinct().ToList();
            }
            else if (institutionType == "3") //colleges
            {
                sM = (from s in _context.TbColleges
                      where s.State == state
                      orderby s.Name ascending

                      select new SchoolByStateModel()
                      {
                          SchoolId = s.SchoolId.ToString(),
                          SchoolName = s.Name
                      }
                        ).Distinct().ToList();
            }
            return sM;
        }


        private static string HTMLBodyText(string fromName, string toName, string msg, string appName, string webSiteLink)
        {
            string str = "";

            str = str + "<table width='100%' style='text-align: center;'>";
            str = str + "<tr>";
            str = str + "<td style='font-weight: bold; font-size: 14pt; height: 25px; text-align: left; background-color: #4a6792;";
            str = str + "vertical-align: middle; color: White; font-family:Lucida Grande, tahoma, helvetica;'>";
            str = str + "&nbsp;" + appName;
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr><td>&nbsp;</td></tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12pt; text-align: left; width: 100%; font-family: font-family:Lucida Grande, tahoma, helvetica;'>";
            str = str + "<p>Hi " + toName + ",<p/>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "<tr>";
            str = str + "<td style='font-size: 12pt; text-align: left; width: 100%; font-family: font-family:Lucida Grande, tahoma, helvetica;'>";
            str = str + "<p>" + fromName + " has sent you the following message from " + appName + ":<br/><br/>";
            str = str + "<p></p><p>";
            str = str + "<p> " + msg + "<br/>";
            str = str + "</p>";

            str = str + "<p></p><p>";

            str = str + "You can access the site via the link below. <br />";
            str = str + "<a href='" + webSiteLink + "'>" + webSiteLink + "</a></p>";

            str = str + "<p>";
            str = str + "Sincerely yours,<br />";
            str = str + appName + " Team <br />";
            str = str + "</p>";
            str = str + "</td>";
            str = str + "</tr>";
            str = str + "</table>";

            return str;
        }

        public void SendMail(string memberName, string fromEmail, string toEmail, string subject, string body, bool isBodyHtml)
        {
            string name = memberName;

            if (string.IsNullOrEmpty(name))
                name = _configuration.GetValue<string>("AppStrings:AppAdmin");

            //(1) Create the MailMessage instance
            fromEmail = _configuration.GetValue<string>("AppStrings:AppFromEmail");

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, name); //IMPORTANT: This must be same as your smtp authentication address.
            mail.To.Add(toEmail);

            //(2) Assign the MailMessage's properties
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //(3) Create the SmtpClient object
            string smtpHost = _configuration.GetValue<string>("AppStrings:AppSMTPHost");

            int smtpPort = 25;
            if (String.IsNullOrEmpty(_configuration.GetValue<string>("AppStrings:AppSMTPPort")))
            {
                smtpPort = Convert.ToInt32(_configuration.GetValue<string>("AppStrings:AppSMTPPort"));
            }

            string appSMTPpwd = _configuration.GetValue<string>("AppStrings:AppSMTPpwd");

            var client = new SmtpClient(smtpHost, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, appSMTPpwd),
                EnableSsl = false
            };
            client.Send(mail);
        }

        public List<AdsModel> GetAds(string type)
        {
            var result = _context.Set<AdsModel>().FromSqlRaw("exec spGetAds @AdType ", new SqlParameter("@AdType", type));
            return new List<AdsModel>(result);
        }
    }

    public interface ICommonRepository
    {
        List<TbRecentNews> GetRecentNews();
        List<TbStates> GetStates();
        void SendGenEmailToUser(string memberName, string fromEmail, string toEmail, string fromName, string toName, string subject, string msg);
        void SendMail(string memberName, string fromEmail, string toEmail, string subject, string body, bool isBodyHtml);
        List<SportsListModel> GetSportsList();
        List<SchoolByStateModel> GetSchoolsByState(string state, string institutionType);
        List<AdsModel> GetAds(string type);
    }

}
