using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using SunokoLibrary.Application;

using NicoGetCookie.Prop;
using NicoGetCookie.Net;

namespace NicoGetCookie
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            this.Text = Ver.GetFullVersion();
            //IsBatchMode = (args.Length > 0) ? true : false;
            //if (IsBatchMode)
            //{
            //    liveId = NicoLiveNet.GetLiveID(args[0]);
            //    if (string.IsNullOrEmpty(liveId))
            //    {
            //        this.Close();
            //    }
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //設定データー読み込み
            //accountdbfile = Path.Combine(Props.GetSettingDirectory(), "account.db");
            //props = new Props();
            //props.LoadData(accountdbfile);
            nicoSessionComboBox1.Selector.PropertyChanged += Selector_PropertyChanged;
            //var tsk = nicoSessionComboBox1.Selector.SetInfoAsync(Properties.Settings.Default.SelectedSourceInfo);

            if (checkBox2.Checked)
            {
                textBox1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = null;
                button2.Enabled = false;
            }
        }

        async void Selector_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedIndex":
                    var cookieContainer = new CookieContainer();
                    var currentGetter = nicoSessionComboBox1.Selector.SelectedImporter;
                    if (currentGetter != null)
                    {
                        var result = await currentGetter.GetCookiesAsync(new Uri(Props.NicoDomain));
                        var cookie = result.Status == CookieImportState.Success ? result.Cookies["user_session"] : null;
                        //UI更新
                        //textBox1.Text = currentGetter.SourceInfo.CookiePath;
                        //button2.Enabled = true;
                        textBox2.Text = cookie != null ? cookie.Value : null;
                        //textBox2.Enabled = result.Status == CookieImportState.Success;
                        //Properties.Settings.Default.SelectedSourceInfo = currentGetter.SourceInfo;
                        //Properties.Settings.Default.Save();
                    }
                    else
                    {
                        textBox1.Text = null;
                        textBox2.Text = null;
                        //textBox2.Enabled = false;
                        //button2.Enabled = false;
                    }
                    break;
            }
        }
        //void btnReload_Click(object sender, EventArgs e)
        //{ var tsk = nicoSessionComboBox1.Selector.UpdateAsync(); }
        //void btnOpenCookieFileDialog_Click(object sender, EventArgs e)
        //{ var tsk = nicoSessionComboBox1.ShowCookieDialogAsync(); }
        //void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
        //{ nicoSessionComboBox1.Selector.IsAllBrowserMode = checkBoxShowAll.Checked; }


        private void button1_Click(object sender, EventArgs e)
        {
            //クッキー一覧更新
            var tsk = nicoSessionComboBox1.Selector.UpdateAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //Cookieファイル直接指定
            var tsk = nicoSessionComboBox1.ShowCookieDialogAsync();
            var currentGetter = nicoSessionComboBox1.Selector.SelectedImporter;
            if (currentGetter != null)
            {
                var result = await currentGetter.GetCookiesAsync(new Uri(Props.NicoDomain));
                var cookie = result.Status == CookieImportState.Success ? result.Cookies["user_session"] : null;
                //UI更新
                textBox1.Text = currentGetter.SourceInfo.CookiePath;
                textBox2.Text = cookie != null ? cookie.Value : null;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //テスト
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                var flag = false;
                var cc = new CookieContainer();
                var nln = new NicoLiveNet();
                cc = nln.SetCookie(textBox2.Text);
                (flag, _, _) = await nln.IsLoginNicoAsync(cc);
                if (flag)
                {
                    MessageBox.Show("user_sessionは有効です",
                                   "テスト",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("user_sessionは無効です",
                                   "テスト",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //コピー
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                Clipboard.SetText(textBox2.Text);
            }
        }

        private void 終了XToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            nicoSessionComboBox1.Selector.IsAllBrowserMode = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox1.Enabled = true;
                //textBox1.Text = null;
                button2.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = null;
                button2.Enabled = false;
            }
        }
    }
}
