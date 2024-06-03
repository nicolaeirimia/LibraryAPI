using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace LibraryAPI.Models
{
    public class UserCredentials
    {
        [JsonProperty ("userJson")]
        [XmlElement ("userXml")]
        [ModelBinder (Name = "userForm")]
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
