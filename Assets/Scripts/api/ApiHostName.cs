using System;
using UnityEngine;

namespace api
{
    public class ApiHostName
    {
        private ApiHostName()
        {
            hostName = Environment.GetEnvironmentVariable("HOKUMA_FRIENDS_HOSTNAME") ?? "http://localhost:8000";
            Debug.Log(hostName);
        }

        public static ApiHostName instance = new ApiHostName();
        public string hostName;
    }
}