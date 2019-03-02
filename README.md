JoyPhotoshop
======================

Photoshop を JoyCon の左手で操作するためのツールです。カスタマイズ機能などはないので自分でビルドできる人向けです。

使い方
=======================
Visual Studio で自分でビルドしてください。Managed DirectX を使用しているので http://blog.sys-ni.com/article/154055413.html を参考に、 DirectInput の DLL の追加と、LoaderLock を例外設定から外すのをしないと動きません。

また実行には以下のツールが必要です。

- vJoy http://vjoystick.sourceforge.net/site/
- JoyCon-Driver https://github.com/mfosse/JoyCon-Driver

JoyCon-Driver の Y 軸反転と左手ジャイロを使うの設定をしないと動かないと思います。
