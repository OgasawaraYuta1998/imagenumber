Image Number
====
Unityで数字を画像(スプライト)で表示するためのスクリプトセット


![スプラッシュ画像](https://darjjeelling.files.wordpress.com/2017/04/splash.png)

## 概要
作成したGameObjectの枠内に画像化した数字を表示します。
指定した最大桁数まで０で埋めます。またその０を表示させないようにもできます

## 使い方
1. 以下のファイルをAssets内にコピーします
- NumberImageRenderer
- SampleScript
- Resources以下のファイル
2. HierarchyでCreate->Create Emptyで生成したオブジェクトにRect TransformをAdd Componentします
3. さらにNumberImageRenderer, SampleScriptの２つのスクリプトを追加します
4. Inspector上でNumberImageRenderer(Script)で以下のように指定します
- File Name : img123456789
- Parent Transform : <2.で作成したオブジェクト>
5. プロジェクトを実行します。2.で作成したオブジェクト内に数字が表示されます。

## 解説

####呼び出す側のコード
（SampleScript.csを参考にしてください）

```
NumberImageRenderer ni = null;
ni = GetComponent<NumberImageRenderer> ();	
ni.Draw (1973476);
```

最大桁数を指定するとその桁数になるよう０で埋めて表示されます。

```
ni.maxDigit = 9;
```

０埋めを表示したくない場合は、Ignore Filled Zeroのチェックをオンにしてください。

####数字の画像を変えたい場合
Resources/img123456789のファイルを差し替えてください


さらに詳しくは以下ページをごらんください
https://darjjeelling.wordpress.com/

## 動作確認環境
- Unity5.5
- MacOSX10.11.6

## Licence
MIT Licence

## Author
- Twitter: [@darjjeelling](https://twitter.com/darjjeelling)
- Github: [darjjeelling](https://github.com/darjjeelling)

