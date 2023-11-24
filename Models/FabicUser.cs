using System;
using System.Collections.Generic;

namespace Fabic.Core.Models
{
    public class FabicUser
    {
        public string UserID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string GravatarURL
        {
            get; set;
        }

        public bool EmailVerified
        {
            get; set;
        }

        public DateTime CreationDate
        {
            get; set;
        }

        public DateTime UpdatedDate
        {
            get; set;
        }

        public static FabicUser LoadFromData(string _userID, string _name, string _email, string _gravatarURL)
        {
            FabicUser user = new FabicUser();
            user.UserID = _userID;
            user.Name = _name;
            user.Email = _email;
            user.GravatarURL = _gravatarURL;
            return user;
        }

        public static Dictionary<string, object> SerialiseUser(FabicUser user)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("ID", user.UserID);
            dictionary.Add("Name", user.Name);
            dictionary.Add("Email", user.Email);
            dictionary.Add("Picture", user.GravatarURL);
            return dictionary;
        }

        public static FabicUser DeserialiseUser(Dictionary<string, object> dictionary)
        {
            FabicUser fabicUser = new FabicUser();
            if (dictionary.ContainsKey("ID"))
                fabicUser.UserID = dictionary["ID"].ToString();
            if (dictionary.ContainsKey("Name"))
                fabicUser.Name = dictionary["Name"].ToString();
            if (dictionary.ContainsKey("Email"))
                fabicUser.Email = dictionary["Email"].ToString();
            if (dictionary.ContainsKey("Picture"))
                fabicUser.GravatarURL = dictionary["Picture"].ToString();
            return fabicUser;
        }
    }
}
