using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace NicoGetCookie
{
    public partial class LoginNico : Form
    {
        public bool IsDebug { get; set; }

        private WebView2 webView;

        private const string LoginUrl =
            "https://account.nicovideo.jp/spa/login/index.html" +
            "?sec=header_pc&redirect_uri=https%3A%2F%2Fwww.nicovideo.jp%2F";

        // ID / パスワード
        private string _nicoid = string.Empty;
        private string _nicopass = string.Empty;

        CookieContainer _cc = new CookieContainer();

        // 二重取得防止
        private bool cookieRetrieved = false;
        public LoginNico(CookieContainer cc, string nicoid, string nicopass)
        {
            //InitializeComponent();

            IsDebug = false;

            this.Text = "ニコニコログイン";
            this.Width = 500;
            this.Height = 720;

            webView = new WebView2();
            webView.Dock = DockStyle.Fill;

            this.Controls.Add(webView);

            this.Load += LoginNico_Load;

            _nicoid = nicoid;
            _nicopass = nicopass;
            _cc = cc;
        }
        private async void LoginNico_Load(object sender, EventArgs e)
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);

                webView.CoreWebView2.NavigationCompleted +=
                    CoreWebView2_NavigationCompleted;

                webView.CoreWebView2.Navigate(LoginUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "WebView2初期化エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void CoreWebView2_NavigationCompleted(
            object sender,
            CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
                return;

            string url = webView.Source != null
                ? webView.Source.ToString()
                : "";

            Console.WriteLine("NavigationCompleted:");
            Console.WriteLine(url);

            // -----------------------------------------
            // ログインページ
            // -----------------------------------------
            if (url.IndexOf(
                    "account.nicovideo.jp",
                    StringComparison.OrdinalIgnoreCase) >= 0)
            {
                await Task.Delay(300);
                if (string.IsNullOrEmpty(_nicoid) || string.IsNullOrEmpty(_nicopass))
                    return;
                else
                    await AutoInputLogin();
            }

            // -----------------------------------------
            // ニコニコトップページ
            // -----------------------------------------
            if (url.StartsWith(
                    "https://www.nicovideo.jp/",
                    StringComparison.OrdinalIgnoreCase))
            {
                if (!cookieRetrieved)
                {
                    cookieRetrieved = true;

                    await GetCookiesAsync(_cc);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// ID/PASSを自動入力してログインボタンをクリック
        /// </summary>
        private async Task AutoInputLogin()
        {
            string id = JavaScriptString(_nicoid);
            string password = JavaScriptString(_nicopass);

            string script = @"
(function() {

    var inputs = document.querySelectorAll('input');

    var idInput = null;
    var passwordInput = null;

    // -------------------------------
    // ID / Password入力欄を探す
    // -------------------------------
    for (var i = 0; i < inputs.length; i++) {

        var input = inputs[i];

        if (input.type === 'password') {
            passwordInput = input;
        }
        else if (
            input.type === 'text' ||
            input.type === 'email'
        ) {
            if (!idInput) {
                idInput = input;
            }
        }
    }

    if (!idInput || !passwordInput) {
        return 'INPUT_NOT_FOUND';
    }

    // React等に入力を認識させるため
    var valueSetter =
        Object.getOwnPropertyDescriptor(
            HTMLInputElement.prototype,
            'value'
        ).set;

    // -------------------------------
    // ID
    // -------------------------------
    idInput.focus();

    valueSetter.call(
        idInput,
        '" + id + @"'
    );

    idInput.dispatchEvent(
        new Event('input', { bubbles: true })
    );

    idInput.dispatchEvent(
        new Event('change', { bubbles: true })
    );

    // -------------------------------
    // Password
    // -------------------------------
    passwordInput.focus();

    valueSetter.call(
        passwordInput,
        '" + password + @"'
    );

    passwordInput.dispatchEvent(
        new Event('input', { bubbles: true })
    );

    passwordInput.dispatchEvent(
        new Event('change', { bubbles: true })
    );

    // -------------------------------
    // ログインボタンを探す
    // -------------------------------
    var buttons =
        document.querySelectorAll(
            'button, input[type=submit]'
        );

    var loginButton = null;

    for (var j = 0; j < buttons.length; j++) {

        var button = buttons[j];

        var text =
            (button.innerText ||
             button.value ||
             '').trim();

        if (
            text.indexOf('ログイン') >= 0 ||
            text.toLowerCase().indexOf('login') >= 0
        ) {
            loginButton = button;
            break;
        }
    }

    if (!loginButton) {
        return 'LOGIN_BUTTON_NOT_FOUND';
    }

    // 少し待ってからクリック
    setTimeout(function() {
        loginButton.click();
    }, 3000);

    return 'OK';

})();
";

            try
            {
                string result =
                    await webView.CoreWebView2.ExecuteScriptAsync(script);

                Console.WriteLine(
                    "自動ログイン処理結果: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// WebView2に保存されているCookieを取得
        /// </summary>
        public async Task GetCookiesAsync(CookieContainer cc)
        {
            string user_session = string.Empty;

            try
            {
                CoreWebView2CookieManager cookieManager =
                    webView.CoreWebView2.CookieManager;

                // ニコニコドメインのCookieを取得
                List<CoreWebView2Cookie> cookies =
                    await cookieManager.GetCookiesAsync(
                        "https://www.nicovideo.jp/");

                foreach (CoreWebView2Cookie wc in cookies)
                {
                    try
                    {
                        Cookie cookie = new Cookie();

                        if (wc.Name == "user_session")
                            user_session = wc.Value;

                        cookie.Name = wc.Name;
                        cookie.Value = wc.Value;

                        // Domain
                        string domain = wc.Domain;

                        // CookieContainerでは先頭の"."を付けたままでも扱える
                        cookie.Domain = domain;

                        // Path
                        cookie.Path = string.IsNullOrEmpty(wc.Path)
                            ? "/"
                            : wc.Path;

                        // Secure
                        cookie.Secure = wc.IsSecure;

                        // HttpOnly
                        cookie.HttpOnly = wc.IsHttpOnly;

                        // 有効期限
                        cookie.Expires = wc.Expires;

                        cc.Add(cookie);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            "Cookie追加エラー: " +
                            wc.Name + " : " + ex.Message);
                    }
                }

                // 必要ならMessageBoxで確認
                if (IsDebug)
                {
                    if (!string.IsNullOrEmpty(user_session))
                    {
                        MessageBox.Show(
                            "ログイン成功\n" +
                            "user_sessionを取得しました",
                            "完了",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "ログイン失敗\n" +
                            "user_sessionを取得できませんでした",
                            "エラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Cookie取得エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return;
        }

        /// <summary>
        /// CookieをHTTP Cookieヘッダー形式に変換
        /// </summary>
        private string CreateCookieHeader(
            List<CoreWebView2Cookie> cookies)
        {
            StringBuilder sb =
                new StringBuilder();

            for (int i = 0; i < cookies.Count; i++)
            {
                if (i > 0)
                    sb.Append("; ");

                sb.Append(cookies[i].Name);
                sb.Append("=");
                sb.Append(cookies[i].Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// JavaScript文字列用にエスケープ
        /// </summary>
        public string JavaScriptString(string value)
        {
            if (value == null)
                return "";

            return value
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

    }
}
