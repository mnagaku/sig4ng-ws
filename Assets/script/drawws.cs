using UnityEngine;
using System.Collections;

public class drawws : MonoBehaviour
{
	public int charNo = 1;
	public string siteName = "https://drawws.kgr-lab.com";

	private GameObject armr, arml;
	private bool flag = false;

	// Use this for initialization
	IEnumerator Start ()
	{
		GameObject body = transform.Find ("body").gameObject;
		Renderer bodyRenderer = body.GetComponent<Renderer> ();

		armr = transform.Find ("armr").gameObject;
		Renderer armrRenderer = armr.GetComponent<Renderer> ();

		arml = transform.Find ("arml").gameObject;
		Renderer armlRenderer = arml.GetComponent<Renderer> ();

		string bodyFile = string.Format ("/b{0:0000}", charNo);
		string armFile = string.Format ("/a{0:0000}", charNo);

		Texture2D tbody;
		Texture2D tarm;
		tbody = Resources.Load<Texture2D> ("Texture" + bodyFile);
		tarm = Resources.Load<Texture2D> ("Texture" + armFile);

		if (tbody == null || tarm == null) {
			WWW bodyWWW = new WWW (siteName + bodyFile + ".png");
			WWW armWWW = new WWW (siteName + armFile + ".png");
			yield return bodyWWW;
			yield return armWWW;
			bodyRenderer.material.mainTexture = bodyWWW.texture;
			armrRenderer.material.mainTexture = armWWW.texture;
			armlRenderer.material.mainTexture = armWWW.texture;
		} else {
			bodyRenderer.material.mainTexture = tbody;
			armrRenderer.material.mainTexture = tarm;
			armlRenderer.material.mainTexture = tarm;
		}

		flag = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (flag) {
			armr.transform.Rotate (0, 5, 0);
			arml.transform.Rotate (0, -5, 0);
		}
	}
}
