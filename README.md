# NicoGetCookie

ブラウザーからニコニコ（ニコ動、ニコ生）のCookieを取得するプログラムです  

# 特徴

- GUI(Windows Forms)使用。  
- Firefox系のブラウザーのみ取得可能  
- Google Chrome、Microsoft Edge、OperaなどのChromium系ブラウザーからのCookie取得はできません  

# 開発環境

- Windows 11  
- Microsoft Visual Studio 2019以降  
- .NET 4.8  

# パッケージ

以下のパッケージをインストールしてください。  
(ローカルのフォルダーにコピーして、そのフォルダーをVisual StudioでNugetのパッケージソースとして設定してください）  

- SnkLib.App.CookieGetter.2.4.4  
- SnkLib.App.CookieGetter.Forms.1.4.5  

https://github.com/nnn-revo2012/SnkLib.App.CookieGetter/releases/tag/v2.4.4  
namoshikaさんのSnkLib.App.CookieGetter (https://github.com/namoshika/SnkLib.App.CookieGetter) を元にguest-nicoさんがGoogleChrome80対応他をされたもの (https://github.com/guest-nico/SnkLib.App.CookieGetter) を元に追加修正したものです。  

# 実行方法

実行ファイル・ライブラリーを同じフォルダーに入れて実行してください。  
また、外部プログラムも同じフォルダーに入れてください。  

# ライセンス
- NicoGetCookie
https://github.com/nnn-revo2012/NicoGetCookie  
Copyright (c) 2026 nnn-revo2012  
Released under the MIT Licence  

- SnkLib.App.CookieGetter  
https://github.com/namoshika/SnkLib.App.CookieGetter   
Copyright (c) 2014 namoshika.  
Released under the GNU Lesser GPL  
本ソフトウェアでは上記にGoogleChrome80対応の修正他を行ったものを使用しております。  
https://github.com/guest-nico/SnkLib.App.CookieGetter  
Copyright (c) 2019 guest-nico  
Released under the GNU Lesser GPL  
本ソフトウェアでは上記に更に追加修正を行ったものを使用しております。  
https://github.com/nnn-revo2012/SnkLib.App.CookieGetter  
Copyright (c) 2019 nnn-rev02012  

- Json.NET  
https://www.newtonsoft.com/json  
Copyright (c) 2007 James Newton-King  
Released under the MIT License  

- BouncyCastle  
http://www.bouncycastle.org/csharp/  
Copyright (c) 2000-2020 Legion of the Bouncy Castle Inc.  
Released under the MIT License  

- SQLite  
https://www.sqlite.org/index.html  
Released into the Public Domain  

