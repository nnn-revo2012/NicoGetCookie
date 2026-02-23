using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

using SunokoLibrary.Application;

namespace NicoGetCookie.Prop
{

    public class Props
    {
        //定数設定
        public static readonly string UserAgent = "Mozilla/5.0 (" + Ver.GetAssemblyName() + "; " + Ver.Version + ")";
        public static readonly string NicoDomain = "https://www.nicovideo.jp/";

        public static readonly string NicoLiveUrl = "https://live.nicovideo.jp/watch/";
        public static readonly string NicoOrigin  = "https://live.nicovideo.jp";
        public static readonly string NicoCommUrl = "https://com.nicovideo.jp/community/";
        public static readonly string NicoChannelUrl = "https://ch.nicovideo.jp/";
        public static readonly string NicoUserUrl = "https://www.nicovideo.jp/user/";

        public static readonly string NicoLoginUrl = "https://account.nicovideo.jp/login/redirector?show_button_twitter=1&site=niconico&show_button_facebook=1&next_url=%2F";
        public static readonly string NicoGetPlayerStatus = "https://ow.live.nicovideo.jp/api/getplayerstatus?v=";

        public bool IsDebug { get; set; }

        public Props() {}

        //設定ファイルの場所をGet
        public static string GetSettingDirectory()
        {
            //設定ファイルの場所
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.PerUserRoamingAndLocal);
            return Path.GetDirectoryName(config.FilePath);
        }

        //アプリケーションの場所をGet
        public static string GetApplicationDirectory()
        {
            //アプリケーションの場所
            var tmp = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(tmp);
        }

        //ログファイル名をGet
        public static string GetLogfile(string dir, string filename)
        {
            var tmp = Path.GetFileNameWithoutExtension(filename) + "_" + System.DateTime.Now.ToString("yyMMdd_HHmmss") + ".log";
            return Path.Combine(dir, tmp);
        }

        //実行ログファイル名をGet
        public static string GetExecLogfile(string dir, string filename)
        {
            var tmp = Path.GetFileNameWithoutExtension(filename) + "_exec_" + System.DateTime.Now.ToString("yyMMdd_HHmmss") + ".log";
            return Path.Combine(dir, tmp);
        }

        //data-propsファイル名をGet
        public static string GetDataPropsfile(string dir, string filename)
        {
            var tmp = Path.GetFileNameWithoutExtension(filename) + "_data-props_" + System.DateTime.Now.ToString("yyMMdd_HHmmss") + ".log";
            return Path.Combine(dir, tmp);
        }

        public static string GetDirSepString()
        {
            return Path.DirectorySeparatorChar.ToString();
        }

        public static string GetLiveUrl(string liveid)
        {
            return NicoLiveUrl + liveid;
        }

        public static string GetChannelUrl(string channelid)
        {
            return NicoChannelUrl + channelid;
        }

        public static string GetCommUrl(string channelid)
        {
            return NicoCommUrl + channelid;
        }

        public static string GetUserUrl(string userid)
        {
            return NicoUserUrl + userid;
        }

        public static string GetProviderType(string type)
        {
            var result = "？？";
            switch (type)
            {
                case "community":
                    result = "コミュニティ";
                    break;
                case "user":
                    result = "コミュニティ";
                    break;
                case "channel":
                    result = "チャンネル";
                    break;
                case "official":
                    result = "公式生放送";
                    break;
                case "cas":
                    result = "実験放送";
                    break;
                default:
                    break;
            }
            return result;
        }

        private static readonly Regex RgxChNo = new Regex("/([^/]+)$", RegexOptions.Compiled);
        public static string GetChNo(string url)
        {
            return RgxChNo.Match(url).Groups[1].Value;
        }

        private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static long GetUnixTime(DateTime localtime)
        {
            DateTime utc = localtime.ToUniversalTime();
            return (long)(((TimeSpan)(utc - UNIX_EPOCH)).TotalSeconds);
        }

        public static DateTime GetUnixToDateTime(long unix)
        {
            return UNIX_EPOCH.AddSeconds(unix).ToLocalTime();
        }

        public static long GetLongParse(string ttt)
        {
            double dd = -1.0D;
            double.TryParse(ttt, out dd);
            return (long )dd;

        }
        //特殊文字をエンコードする
        public static string HtmlEncode(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            s = s.Replace("&", "&amp;");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
             s = s.Replace("\"", "&quot;");

            return s;
        }

    }
}
