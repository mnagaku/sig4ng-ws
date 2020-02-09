using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sound : MonoBehaviour {

	public AudioClip ac;
	private float timeElapsed = 0.0f;

	private bool sflag = true;
	private float x, z;
	private AudioSource audioSource;

    private Material matInstance;

    public bool rotationLockEnabled = true;

    // ローテーションロックのデバッグの有効／無効化
    public const bool ENABLE_ROTATION_LOCK_DEBUG = false;

    void Awake()
    {
        gameObject.GetComponent<Rigidbody>().Sleep();

        if (ENABLE_ROTATION_LOCK_DEBUG)
        {
            var renderer = GetComponent<Renderer>();
            matInstance = Instantiate<Material>(renderer.material);
            renderer.material = matInstance;
        }
    }

    // Use this for initialization
    void Start () {
		if(!ac) {
			ac = Resources.Load("SE/bomb") as AudioClip;
		}
		x = transform.localPosition.x;
		z = transform.localPosition.z;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = ac;
		audioSource.time = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(sflag && 
			(System.Math.Abs(transform.localPosition.x - x) > 0.1 ||
			System.Math.Abs(transform.localPosition.z - z) > 0.1)) {
			audioSource.Play();
			sflag = false;
		}
	}

    void FixedUpdate()
    {
        if (!sflag)
        {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed > 5000.0f)
            {
                gameObject.GetComponent<Rigidbody>().Sleep();
            }
        }
    }

    private Collider firstCollider;

    void OnCollisionEnter(Collision collision)
    {
        if (!rotationLockEnabled)
            return;

        if (collision.gameObject.name == "Terrain")
            return;

        var bc = GetComponent<BoxCollider>();
        if (!bc)
            return;

        var rb = GetComponent<Rigidbody>();
        if (firstCollider == null)
        {
            // どの軸をロックするべきか、オブジェクトのサイズから判定する.
            Vector3 size = gameObject.transform.localScale;
            if (size.x > size.z)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            }

            if (matInstance)
                matInstance.color = Color.red;

            firstCollider = collision.collider;
        }
        else if (firstCollider != collision.collider)
        {
            rb.constraints = RigidbodyConstraints.None;

            if (matInstance)
                matInstance.color = Color.green;
        }

    }
}
