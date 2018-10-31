using UnityEngine;
using System.Collections;

public class gate : MonoBehaviour {

    public enum CameraMode
    {
        Default,
        Top,
        North,
        West,
        South,
        East
    };

    public CameraMode cameraMode = CameraMode.Default;

	[Range(-10.0f, 10.0f)]
	public float targetX = 0.5f;

	[Range(0.0f, 15.0f)]
	public float targetY = 10.0f;

	[Range(-10.0f, 10.0f)]
	public float targetZ = -15.0f;

	private bool fflag = true;
	private int count = 0, step = 50;
	private Vector3 fromPosition, toPosition;
	private Quaternion fromRotation, toRotation;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(!fflag && count < step) {
			Camera.main.transform.position =
				Vector3.Lerp(fromPosition, toPosition,
				(float)count / step);
			Camera.main.transform.rotation =
				Quaternion.Lerp(fromRotation, toRotation,
				(float)count / step);
			count++;
		}
	}

	void OnTriggerEnter(Collider other)
    {
		if(fflag)
        {
            ApplyCamera(false);
			fflag = false;
		}
	}

    private Vector3 getTargetVector()
    {
        switch (cameraMode)
        {
            case CameraMode.Default:
                return new Vector3(targetX, targetY, targetZ);
            case CameraMode.Top:
                return new Vector3(0f, 13f, 0f);
            case CameraMode.North:
                return new Vector3(0f, 8f, 10f);
            case CameraMode.West:
                return new Vector3(-10f, 8f, 0f);
            case CameraMode.South:
                return new Vector3(0f, 8f, -10f);
            case CameraMode.East:
                return new Vector3(10f, 8f, 0f);
            default:
                return new Vector3(targetX, targetY, targetZ);
        }
    }

    public void ApplyCamera(bool force)
    {
        var targetVec = getTargetVector();

        toPosition = new Vector3(
            transform.root.position.x + targetVec.x,
            transform.root.position.y + targetVec.y,
            transform.root.position.z + targetVec.z);
        toRotation = Quaternion.LookRotation(
            new Vector3(-targetVec.x, -targetVec.y, -targetVec.z));
        fromPosition = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            Camera.main.transform.position.z);
        fromRotation = new Quaternion(
            Camera.main.transform.rotation.x,
            Camera.main.transform.rotation.y,
            Camera.main.transform.rotation.z,
            Camera.main.transform.rotation.w);

        if (force)
        {
            fromPosition = toPosition;
            fromRotation = toRotation;

            Camera.main.transform.position = toPosition;
            Camera.main.transform.rotation = toRotation;

            fflag = false;
            count = step;
        }
    }
}
