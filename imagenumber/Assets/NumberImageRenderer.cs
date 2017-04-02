using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class NumberImageRenderer : MonoBehaviour {
	[System.Serializable]
	public struct TextNumRenderData {
		public TextNumRenderData(string fileName, Transform parentTrasform, bool ignoreFilledZero)
			: this()
		{ 
			this.fileName = fileName;
			this.parentTrasform = parentTrasform;
			this.ignoreFilledZero = ignoreFilledZero;
		}

		public string fileName;
		public Transform parentTrasform;
		public bool ignoreFilledZero;
	}

	struct Figure {
		public RectTransform rectTrans;
		public SpriteRenderer sr;
	}

	[SerializeField] TextNumRenderData data;
	public int maxDigit = 0;//表示最大桁数

	Sprite[] sprites = null;
	List<Figure> figureList = new List<Figure>();

	int digit = 0;//表示する数値の桁数

	// 文字（数字）表示用データの生成
	Figure Create(Sprite sprite) {
		var instance = new GameObject();
		instance.name =sprite.name;
		instance.transform.SetParent(data.parentTrasform);

		var rectTrans = instance.AddComponent<RectTransform>();
		instance.transform.localScale = Vector3.one;
		rectTrans.sizeDelta = data.parentTrasform.GetComponent<RectTransform>().sizeDelta;

		var spriteRenderer = instance.AddComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprite;

		var figure = new Figure() { rectTrans = rectTrans , sr = spriteRenderer };

		return figure;
	}

	// 数字をSpriteで描画する
	void DrawSprite(string text) {
		if (sprites == null) {
			sprites = Resources.LoadAll<Sprite>(data.fileName);
		}

		for (int i = text.Length - 1 ; i >= 0 ; i--) {
			Sprite sprite = null;
			var sp_name = data.fileName + "_" + text [i].ToString ();

			//頭の０埋めを表示しない処理
			if (text [i].ToString().Equals("0") && 
				i <= text.Length - 1 - digit &&
				data.ignoreFilledZero) {
				sprite = Resources.Load<Sprite> ("clear");//Resourcesの下に"clear"ファイルがあること
			} else {
				sprite = System.Array.Find (sprites, (s) => s.name.Equals (sp_name));
			}

			var figure = Create(sprite);
			float spWidth = figure.rectTrans.sizeDelta.x / text.Length;
			float spStartx = -0.5f * figure.rectTrans.sizeDelta.x + spWidth * 0.5f;
			figure.rectTrans.anchoredPosition3D = new Vector3(i * spWidth + spStartx, 0, 0);

			//画像のサイズ拡大縮小
			float rateX = spWidth / sprite.bounds.size.x;
			float rateY = figure.rectTrans.sizeDelta.y / sprite.bounds.size.y;
			figure.sr.transform.localScale = new Vector3 (rateX,rateY,1.0f);
			figureList.Add(figure);
		}
	}

	//前回呼び出し時のデータをクリア
	public void Reflesh() {
		foreach (Figure figure in figureList) {
			Destroy(figure.sr.gameObject);
		}
		figureList.Clear();
	}

	// 数字をSpriteで描画する
	public void Draw(int num) {
		if (num <= -1) return;

		digit = (num == 0) ? 1 : (int)Mathf.Log10( num ) + 1;
		if (digit > maxDigit) {
			Debug.Log (num+" exceed the max digit :"+maxDigit );
			return;
		}

		Reflesh();
		String form = null;
		if(maxDigit!=0) {
			form = "D" + maxDigit;
		}
		DrawSprite(num.ToString(form));
	}
}