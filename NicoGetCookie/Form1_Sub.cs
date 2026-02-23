using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using SunokoLibrary.Application;
using SunokoLibrary.Windows.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NicoGetCookie.Prop;
using NicoGetCookie.Net;

namespace NicoGetCookie
{
    public partial class Form1 : Form
    {
        //ログウインドウ初期化


        //実行ファイルと同じフォルダにある指定ファイルのフルパスをGet
        private string GetExecFile(string file)
        {
            var fullAssemblyName = this.GetType().Assembly.Location;
            if (Path.GetFileName(file) == file)
                return Path.Combine(Path.GetDirectoryName(fullAssemblyName), file);
            return file;
        }

    }
}
