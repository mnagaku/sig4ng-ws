using UnityEngine;

public class disable_object : MonoBehaviour {

    public Transform explosion_prefab;

	private bool sflag = true;
	private float x, z;

	// Use this for initialization
	void Start()
	{
   		x = transform.localPosition.x;
		z = transform.localPosition.z;
	}

	// Update is called once per frame
	void Update ()
	{
		if(sflag && 
			(System.Math.Abs(transform.localPosition.x - x) > 0.1 ||
			System.Math.Abs(transform.localPosition.z - z) > 0.1))
		{
            gameObject.SetActive(false);
            Instantiate(explosion_prefab, gameObject.transform.position, gameObject.transform.rotation);
			sflag = false;
		}
	}
}
