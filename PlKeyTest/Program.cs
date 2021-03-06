﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlKeyTest
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Program
    {

        //This URL is generated by the PL platform when requesting an iFrame:
        //https://inventory.trackmyvehicle.com/PLLocationDetails.aspx?key=823f1e9af3d579e88d0d006bcbf90d086855c90d&clientID=2&userID=26&itemID=988
        //The configured shared secret is: e25844e6-353f-40da-92ec-16c3760fbe34

        public static void Main()
        {
            var result1 = ConstructIFrameURL("e25844e6-353f-40da-92ec-16c3760fbe34", "2", "26");
            var result2 = ConstructIFrameURL("f514f847-353f-40da-92ec-16c3760fbe34", "2", "26");
            var result3 = ConstructIFrameURL("d5d41737-353f-40da-92ec-16c3760fbe34", "2", "26");
            Console.WriteLine(result1);
            Console.WriteLine(result2);
            Console.WriteLine(result3);
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        public static string ConstructIFrameURL(string key, string clientId, string userId)
        {
            var initialURL = "https://inventory.trackmyvehicle.com/PLLocationDetails.aspx";


            var finalURL = string.Empty;

            if (!string.IsNullOrWhiteSpace(clientId))
            {
                finalURL = finalURL + "&clientID=" + clientId;
                key += "clientID=" + clientId;
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                finalURL = finalURL + "&userID=" + userId;
                key += "userID=" + userId;
            }

            var url = string.Format("{0}?key={1}{2}", initialURL, EncriptKey(key), finalURL);

            return url;
        }

        public static string EncriptKey(string str)
        {
            var sha1 = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var sb = new StringBuilder();
            var stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", i);
            return sb.ToString();
        }
    }
}
