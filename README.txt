===============================================================================
【タイトル】 NicoGetCookie
【ファイル】 NicoGetCookie.exe
【作成月日】 2026/02/23
【著 作 者】 nnn-revo2012
【開発環境】 Microsoft Windows 11
             Microsoft Visual Studio 2019
【動作環境】 Microsoft Windows 11 / Windows 10
             .NET Framework 4.8
【推奨環境】 Microsoft Windows 11
【配布形態】 フリーウェア
【Web Site】 https://github.com/nnn-revo2012/NicoGetCookie
【 連絡先 】 要望やバグ報告等はgithubまで
             その他　nnn_revo2012@yahoo.co.jp　
===============================================================================

■説明
・ブラウザーからニコニコ（ニコ動、ニコ生）のCookieを取得するプログラムです。
・GUI(Windows Forms)使用。
・Firefox系のブラウザーのみ取得可能  
- Google Chrome、Microsoft Edge、OperaなどのChromium系ブラウザーからのCookie取得はできません  

■インストール方法
適当なフォルダにzipファイルの中のファイルを全て解凍してください。解凍したらその中のNicoGetCookie.exe を実行してください。
※ダウンロード時や実行時にウイルスやマルウェアの警告が出る可能性があります。当方でウイルスチェックは行っておりますがあらかじめご了承ください。

■アンインストール方法
アンインストールの際は NicoGetCookie.exe の入っているフォルダごと削除してください。

■使用方法
以下の手順でFirefox系のブラウザーでニコニコにログイン中のCookieの一部(user_session)を取得できます。

1.NicoGetCookie.exeをダブルクリックして起動します。
2.「ブラウザーから取得」のリストボックスをクリックしてCookieを取得したいブラウザー名を選択してください。
　・ブラウザー名の後ろに「（ｘｘｘｘｘ）」と表示されているのが現在ニコニコにログインされているブラウザーです。
　・Google Chrome、Microsoft Edge、OperaなどのChromium系ブラウザーからのCookie取得はできません  
3. ブラウザー名の後ろに「（ｘｘｘｘｘ）」と表示されているリストを選んだと同時に
「取得結果」の「user_session」の下に「user_session_***************」と表示されればＯＫです。
4.その下の「user_sessionをコピー」ボタンをクリックするとクリップボードにuser_sessionがコピーされます。
5.NicoGetCookie.exeはここで終了してもＯＫです。
6.ニコ動やニコ生などの外部ツールでログイン時に「user_session」の文字を入力する欄がある場合、そこにコピーした内容を貼り付けてください。

※user_sessionの文字は不用意にインターネット等で公開すると悪意のある第三者が簡単にあなたのアカウントにアクセスできるようになるので注意してください。

また、ニコ動やニコ生などの外部ツールで指定しているuser_sessionの文字が期限切れになってるかどうかの確認もできます。

1.ニコ動やニコ生などの外部ツールで「user_session」の文字を入力する欄がある場合、その文字列をコピーしてください。
2.NicoGetCookie.exeをダブルクリックして起動します。
3.「取得結果」の「user_session」の下の文字表示欄をクリックし、現在「user_session_***************」と表示されているならそれを削除してから1.でコピーした文字列を貼り付けてください。
4.その右にある「テスト」ボタンをクリックしてください。
5.「user_sessionは有効です」と表示される場合はニコニコに正常にログイン出来ています。
6.「user_sessionは無効です」と表示される場合はそのuser_sessionが何かの原因でログアウトされています。
　その場合は新しいuser_sessionを取得しなおしてみてください。

■動作環境
.Net Framework 4.8が必要です。Windows 11では標準でインストールされているので新たにインストールする必要はありません。
https://dotnet.microsoft.com/ja-jp/download/dotnet-framework/thank-you/net48-web-installer
■免責事項
本ソフトウェアを利用して発生した如何なる損害について著作者は一切の責任を負いません。
また著作者はバージョンアップ、不具合修正の義務を負いません。

■ライセンス関係
・NicoGetCookie
https://github.com/nnn-revo2012/NicoGetCookie
Copyright (c) 2026 nnn-revo2012
Released under the 

・SnkLib.App.CookieGetter
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

・Json.NET
https://www.newtonsoft.com/json
Copyright (c) 2007 James Newton-King
Released under the MIT License

・BouncyCastle
http://www.bouncycastle.org/csharp/
Copyright (c) 2000-2020 Legion of the Bouncy Castle Inc.
Released under the MIT License

・SQLite
https://www.sqlite.org/index.html
Released into the Public Domain

■更新履歴
2026/02/23　Version 0.0.1.01
リリース