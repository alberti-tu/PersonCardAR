using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Class
{
    [Serializable]
    public class User : MonoBehaviour
    {
        public string username;
        public string password;
        public new string name;
        public string country;
        public string company;
        public string position;
        public string mail;

        public User() { }

        public User(string username, string password, string name, string country, string company, string position, string mail)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.country = country;
            this.company = company;
            this.position = position;
            this.mail = mail;
        }

        // Convert objecto to JSON string
        public string toJSON()
        {
            return JsonUtility.ToJson(this);
        }

        // Convert JSON string to object
        public User fromJSON(string json)
        {
            return JsonConvert.DeserializeObject<User>(json);
        }

        // Convert JSON string to List of objects
        public List<User> listOfUsers(string json)
        {
            return JsonConvert.DeserializeObject<List<User>>(json);
        }
    }
}