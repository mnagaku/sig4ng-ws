using UnityEngine;
using System.Collections;

public class drawws : MonoBehaviour {

	public int charNo = 7;

	private GameObject armr, arml;
	private bool flag = false;

	// Use this for initialization
	IEnumerator Start () {
		string bodyURL
			= string.Format("http://kgr-lab.ddo.jp/drawws/b{0:0000}.png", charNo);
		string armURL
			= string.Format("http://kgr-lab.ddo.jp/drawws/a{0:0000}.png", charNo);

		WWW bodyWWW = new WWW(bodyURL);
		WWW armWWW = new WWW(armURL);
		yield return bodyWWW;
		yield return armWWW;

		GameObject body = transform.FindChild ("body").gameObject;
		Renderer bodyRenderer = body.GetComponent<Renderer>();
		bodyRenderer.material.mainTexture = bodyWWW.texture;

		armr = transform.FindChild ("armr").gameObject;
		Renderer armrRenderer = armr.GetComponent<Renderer>();
		armrRenderer.material.mainTexture = armWWW.texture;

		arml = transform.FindChild ("arml").gameObject;
		Renderer armlRenderer = arml.GetComponent<Renderer>();
		armlRenderer.material.mainTexture = armWWW.texture;

		flag = true;
	}

	// Update is called once per frame
	void Update () {
		if(flag) {
			armr.transform.Rotate(0, 5, 0);
			arml.transform.Rotate(0, -5, 0);
		}
	}
}
