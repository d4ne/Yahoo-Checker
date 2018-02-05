using System.IO;
using System.Net;
using System.Text;

namespace yahooChecker
{
    public class yahoo
    {
        public string checkYahoo(string handle, string host)
        {
            if (handle.Length < 4)
            {
                return "carackters";
            }
            else
            {
                if(host == "yahoo.com" || host == "yahoo.de" || host == "yahoo.com.ar" || host == "yahoo.com.br" || host == "yahoo.com.mx" || host == "yahoo.co.uk" || host == "yahoo.co.kr" || host == "yahoo.co.id" || host == "yahoo.co.in" || host == "yahoo.it" || host == "yahoo.com.sg" || host == "yahoo.com.ph" || host == "yahoo.fr")
                {
                    string result = yahooCom(handle);
                    return result;
                }
                else if(host == "yahoo.co.jp")
                {
                    string result = yahooJp(handle);
                    return result;
                }
                else
                {
                    return "unknown";
                }
            }
        }

        public string yahooJp(string handle)
        {
            string post = ".bcrumb=dD1FRUwxWkImc2s9TngzaHdiSnouVGo5Wlk1aUlTMktiUjV0azY4LQ%3D%3D&.id=" + handle;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://account.edit.yahoo.co.jp/json/suggest_id?") as HttpWebRequest;

            request.Method = "POST";
            request.Accept = "*/*";
            request.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
            request.Host = "account.edit.yahoo.co.jp";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.KeepAlive = true;
            request.Headers.Add("Cookie", "B=fhrinb1ct7mb6&b=3&s=ns");
            request.Referer = "https://account.edit.yahoo.co.jp/registration?src=ym&done=https%3A%2F%2Fmail.yahoo.co.jp%2F&fl=100";

            byte[] postBytes = Encoding.ASCII.GetBytes(post);
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (html.Contains("Availability\":false"))
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public string yahooCom(string handle)
        {
            string post = "browser-fp-data=%7B%22language%22%3A%22de%22%2C%22color_depth%22%3A24%2C%22resolution%22%3A%7B%22w%22%3A1920%2C%22h%22%3A1080%7D%2C%22available_resolution%22%3A%7B%22w%22%3A1920%2C%22h%22%3A1040%7D%2C%22timezone_offset%22%3A-120%2C%22session_storage%22%3A1%2C%22local_storage%22%3A1%2C%22indexed_db%22%3A1%2C%22open_database%22%3A1%2C%22cpu_class%22%3A%22unknown%22%2C%22navigator_platform%22%3A%22Win32%22%2C%22do_not_track%22%3A%22unknown%22%2C%22canvas%22%3A%22canvas%20winding%3Ayes~canvas%22%2C%22webgl%22%3A1%2C%22adblock%22%3A0%2C%22has_lied_languages%22%3A0%2C%22has_lied_resolution%22%3A0%2C%22has_lied_os%22%3A0%2C%22has_lied_browser%22%3A0%2C%22touch_support%22%3A%7B%22points%22%3A0%2C%22event%22%3A0%2C%22start%22%3A0%7D%2C%22plugins%22%3A%7B%22count%22%3A4%2C%22hash%22%3A%2263899fc39ae54819a22a20e37bb2d2d1%22%7D%2C%22fonts%22%3A%7B%22count%22%3A35%2C%22hash%22%3A%220b9c90628ff20d0d70ae34bcc57ea4cd%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1507032957576%2C%22render%22%3A1507033041359%7D%7D&specId=yidReg&cacheStored=true&crumb=za9qfJLgRb9&acrumb=DGepjH7X&c=&sessionIndex=&done=https%3A%2F%2Fmail.yahoo.com%2F%3F.intl%3Dde%26amp%3B.lang%3Dde-DE%26amp%3B.partner%3Dnone%26amp%3B.src%3Dfp&googleIdToken=&authCode=&tos0=yahoo_freereg%7Cde%7Cde-DE&tos1=yahoo_comms_atos%7Cde%7Cde-DE&firstName=&lastName=&yid=" + handle + "&password=&shortCountryCode=DE&phone=&mm=&dd=&yyyy=&freeformGender=";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://login.yahoo.com/account/module/create?validateField=yid") as HttpWebRequest;

            request.Method = "POST";
            request.Accept = "*/*";
            request.Headers.Add("Origin", "https://login.yahoo.com");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
            request.Host = "login.yahoo.com";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.KeepAlive = true;
            request.Headers.Add("Cookie", "B=a5hdl75ct5gr0&b=3&s=lm; AS=v=1&s=DGepjH7X&d=C59d4d0f9|EUaFUr3.2cLT2JrwDutLTnBVkng.HwrhhYYUDk0y7LkoVYkrKt2sXXgseZaK66lynj3D2fKZ5ft4jhC0Yala5cmoOIF9iBAMDrq2uZpv4Gd8.NJo3Retb0xYUmURiufFt6.z1H11rORfnG.OdlHrAtx31J1zomosTjpCxtA5qjP3F9wLD6pbgaa9U8iqwWrCZlj3HY2exFqGyYLELMaaj0EYu9RWcejbc8qw9G7B_8tuGDgCf_1AQzh9aaSf6McQF1m6vcYSYx9jhiSl4GZhrCJjWcpPhq49KpNuMi0ZSV1W6l_gVtFG8m.FaAYeMh4CkZFoA05g4c4oJ2Y.yrL0RR3ey26J5WGyo0624AGNkaJ5udiaSVvmjDfo98axvrhQ.r6aRLxkajRSODsZMIJ5Va98OQ7VADO82dJJKBtGS37YHmkPNwxqodAHaCWaNcPUF.MRvFAQu9SseU24.So5BSCx_h7S2kC_14JeRuDGKREIkVVc5b1dpQ8BxgUqiK4.0jDsEUh2qdNzKKo3nUL6_W0JvY5FGAil24KBJmsx27kSsIdmrVrj2p4uksLHvLdvEUwcA2AzL.B_ixFznbQwcdpZL8ZFycIJjfGf6UFS8GzGTrC6xACYTOQ570ulTsDlETz9Q9DOzndNyxrCyYfmUN2MMJb.RR5ELQwDnstpxxbYAYjGrVzxBuKVkuQr74PzSvXYIVecbI_ZZJ8qlSaN0znRfwf3RmyDPpHMyCR.BMbiIKkZh_OmUxQbgk5WtuobL66WEpSu8zihi1Lx1h90PlBNo4msb6Yi2P.llcZnPyjdcwz1GTEH4DMzkadcGIO8Qckh2R2s9siixIPqmx3r_Zc.5PJO4Q--~A");
            request.Referer = "https://login.yahoo.com/account/create?src=ym&intl=de&partner=none&done=https%3A%2F%2Fmail.yahoo.com%2F%3F.intl%3Dde%26amp%3B.lang%3Dde-DE%26amp%3B.partner%3Dnone%26amp%3B.src%3Dfp&specId=yidReg";

            byte[] postBytes = Encoding.ASCII.GetBytes(post);
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (html.Contains("\"error\":\"IDENTIFIER_EXISTS\""))
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }
    }
}
