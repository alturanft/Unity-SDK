
namespace SDK.Assets.Models
{

    public class User
    {
        public string address;

        /// <summary>
        /// meatadata
        /// </summary>
        public string name;
        public string bio;
        public string socialLink;

        /// <summary>
        /// image
        /// </summary>
        public string imageHash;
        public string profilePic;
        public string profilePicUrl;

        /// <summary>
        /// login & security
        /// </summary>
        public int nonce;
        public DateTime lastLogin;

        /// <summary>
        /// Altura Guard code
        /// </summary>
        public string authCode;

        /// <summary>
        /// Sys flags
        /// </summary>
        public bool admin;
        public bool agreeToTerms;
        public bool blacklisted;

        public string Address { get => address; set => address = null; }
        public string Name { get => name; set => name = value; }
        public string Bio { get => bio; set => bio = value; }
        public string SocialLink { get => socialLink; set => socialLink = value; }
        public string ImageHash { get => imageHash; set => imageHash = value; }
        public string ProfilePic { get => profilePic; set => profilePic = value; }
        public string ProfilePicUrl { get => profilePicUrl; set => profilePicUrl = value; }
        public int Nonce { get => nonce; set => nonce = value; }
        public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
        public string AuthCode { get => authCode; set => authCode = value; }
        public bool Admin { get => admin; set => admin = value; }
        public bool AgreeToTerms { get => agreeToTerms; set => agreeToTerms = value; }
        public bool Blacklisted { get => blacklisted; set => blacklisted = value; }

    }
}
