using System;
using UnityEngine;

namespace api
{
    public class ApiHostName
    {
        private ApiHostName()
        {
            // hostName = Environment.GetEnvironmentVariable("HOKUMA_FRIENDS_HOSTNAME", EnvironmentVariableTarget.User) ?? "http://localhost:8000";
            hostName = "https://kotokotosoft.com";
        }

        public static ApiHostName instance = new ApiHostName();
        public string hostName;
    }
}