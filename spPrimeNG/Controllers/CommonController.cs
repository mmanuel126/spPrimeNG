using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using sportprofiles.Repository;
using sportprofiles.DBContextModels;
using sportprofiles.Models.Members;
using Swashbuckle.AspNetCore.Annotations;
using sportprofiles.Models.Organizations;

namespace sportprofiles.Controllers
{
    /// <summary>
    /// a collection of common interfaces and shared functionalities used by the ES.
    /// </summary>
    [Route("services/[controller]")]
    [SwaggerTag("a collection of common interfaces and shared functionalities used by the ES.")]
    public class CommonController : Controller
    {
        
        readonly ICommonRepository _comRepo;
        readonly ILogger<CommonController> _log;
        readonly ILogger _loggerFactory;

        public CommonController(ILogger<CommonController> log, ICommonRepository comRepo, ILoggerFactory loggerFactory)
        {
            _comRepo = comRepo;
            _log = log;
            _loggerFactory = loggerFactory.CreateLogger("SP_ANGULAR_APP"); 
        }

        private static readonly string IV = "IV_VALUE_16_BYTE";
        private static readonly string PASSWORD = "PASSWORD_VALUE";
        private static readonly string SALT = "SALT_VALUE";

        /// <summary>
        /// Encrypts the given string.
        /// </summary>
        /// <param name="encrypt">The string to encrypt.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("EncryptString")]
        public string EncryptString([FromQuery] string encrypt)
        //public static string EncryptAndEncode(string raw)
        {
            using (var csp = Aes.Create())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(encrypt);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                string encrypted = Convert.ToBase64String(output);
                return encrypted;
            }
        }

        /// <summary>
        /// Decrypt an encrypted string.
        /// </summary>
        /// <param name="encrypted">The encrypted string to be decrypted.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DecryptString")]
        public string DecryptString([FromQuery] string encrypted)
        {
            using (var csp =  Aes.Create())
            {
                var d = GetCryptoTransform(csp, false);
                byte[] output = Convert.FromBase64String(encrypted);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        private static ICryptoTransform GetCryptoTransform( Aes csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), Encoding.UTF8.GetBytes(SALT), 65536);
            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(IV);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }

        /// <summary>
        /// Gets the list of states.
        /// </summary>
        /// <returns>The states.</returns>
        [HttpGet]
        [Authorize]
        [Route("GetStates")]
        public List<TbStates> GetStates()
        {
            return _comRepo.GetStates();
        }

        /// <summary>
        /// Logs the specified obj log model to file including error msg and stack trace.
        /// </summary>
        /// <param name="message">the error message to log.</param>
        /// <param name="stack">the errorstack to log.</param>
        [HttpGet]
        [Route("Logs")]
        public void Logs([FromQuery] string message, [FromQuery] string stack)
        {
            string logText = LogText(message, stack);
            _loggerFactory.LogError(logText);
        }

        /// <summary>
        /// returns sports lists.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSportsList")]
        public List<SportsListModel> GetSportsList()
        {
               return _comRepo.GetSportsList();
        }

        /// <summary>
        /// Gets school by state.
        /// </summary>
        /// <returns>The school by state.</returns>
        /// <param name="state">State.</param>
        /// <param name="institutionType">Institution type.</param>
        [HttpGet]
        [Authorize]
         [Route("GetSchoolByState")]
        public List<SchoolByStateModel> GetSchoolByState([FromQuery] string state, [FromQuery] string institutionType)
        {
            return _comRepo.GetSchoolsByState(state, institutionType);
        }

        /// <summary>
        /// returns a list of ads depending on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAds")]
        public List<AdsModel> GetAds([FromQuery] string type)
        {
            return _comRepo.GetAds (type);
        }

        /// <summary>
        /// Logs the text.
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="msg">Message.</param>
        /// <param name="stack">Stack.</param>
        private string LogText(string msg, string stack)
        {
            string txt = "";
            txt += "\n";
            txt = txt + "Message: " + msg + "\n";
            txt = txt + "Stack Trace: " + stack + "\n";
            txt += "\n";
            return txt;
        }
    }

}
