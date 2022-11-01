using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace Application_QS_CheckList.Helper
{
    class GetIpAddressHelper
    {
        public static string GetIpAddress()
        {
            string IP = string.Empty;

            try
            {
                string HostName = Dns.GetHostName();
                IP = Dns.GetHostByName(HostName).AddressList[0].ToString();
              
            }
            catch (Exception ex)
            { 

            }
            return IP;
        }

        public static string GetPingResult(string InputIP)
        {
            Ping PingThis = new Ping();
            PingReply reply = PingThis.Send(InputIP, 1000);
            if (reply != null)
            {

                return reply.Status.ToString(); 
                //+" \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address;
            }
            else
            {
                return "Fail";
            }
        }

    }
}
