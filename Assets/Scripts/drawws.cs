using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class drawws : MonoBehaviour
{
	public int charNo = 1;
	public string siteName = "https://drawws.kgr-lab.com";

	private GameObject armr, arml;
	private bool[] initialized = new bool[3] {false, false, false};

	// Use this for initialization
	void Start ()
	{
		GameObject body = transform.Find ("body").gameObject;
		Renderer bodyRenderer = body.GetComponent<Renderer> ();

		armr = transform.Find ("armr").gameObject;
		Renderer armrRenderer = armr.GetComponent<Renderer> ();

		arml = transform.Find ("arml").gameObject;
		Renderer armlRenderer = arml.GetComponent<Renderer> ();

		string bodyFile = string.Format ("/draw_data/b{0:0000}", charNo);
		string armFile = string.Format ("/draw_data/a{0:0000}", charNo);

		Texture2D tbody;
		Texture2D tarm;
		tbody = Resources.Load<Texture2D> ("Texture" + bodyFile);
		tarm = Resources.Load<Texture2D> ("Texture" + armFile);

		if (tbody == null || tarm == null) {
			StartCoroutine(GetTexture(siteName + bodyFile + ".png", bodyRenderer.material, 0));
			StartCoroutine(GetTexture(siteName + armFile + ".png", armrRenderer.material, 1));
			StartCoroutine(GetTexture(siteName + armFile + ".png", armlRenderer.material, 2));
		} else {
			bodyRenderer.material.mainTexture = tbody;
			armrRenderer.material.mainTexture = tarm;
			armlRenderer.material.mainTexture = tarm;
			initialized = new bool[3] {true, true, true};
		}
	}

    IEnumerator GetTexture(string url, Material m, int flagNo) {
        using(UnityWebRequest www = UnityWebRequestTexture.GetTexture(url)) {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(www.error);
            } else {
                m.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
				initialized[flagNo] = true;
            }
        }
    }

	// Update is called once per frame
	void Update ()
	{
		if (initialized[0] && initialized[1] && initialized[2]) {
			armr.transform.Rotate (0, 5, 0);
			arml.transform.Rotate (0, -5, 0);
		}
	}
}
