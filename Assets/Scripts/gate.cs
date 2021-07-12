using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class gate : MonoBehaviour {

    static int currentActiveStage = -1;

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
        bool isInEditor = false;

#if UNITY_EDITOR
        isInEditor = !EditorApplication.isPlaying;
#endif

        if (isInEditor)
        {
            UpdateInEditor();
        }
        else
        {
            if (!fflag && count < step)
            {
                Camera.main.transform.position =
                    Vector3.Lerp(fromPosition, toPosition,
                    (float)count / step);
                Camera.main.transform.rotation =
                    Quaternion.Lerp(fromRotation, toRotation,
                    (float)count / step);
                count++;
            }
        }
    }

    private void UpdateInEditor()
    {
        var bc = GetComponent<BoxCollider>();
        int layerMask = ~LayerMask.GetMask("CameraTrigger");
        Collider[] colliders = Physics.OverlapBox(bc.bounds.center, bc.bounds.size * 0.5f, Quaternion.identity, layerMask);
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "Movable")
                {
                    Debug.LogErrorFormat("{0}の{1}が、{2}の{3}と接触しています",
                        gameObject.scene.name, gameObject.name, collider.gameObject.scene.name, collider.gameObject.name);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
		if(fflag && other.gameObject.tag == "Movable")
        {
            // スリープ中のオブジェクトとは接触したことにしない.
            // ただしこれは解決にならないので、エラーを出しておく.
            var rb = other.GetComponent<Rigidbody>();
            if (rb.IsSleeping())
            {
                Debug.LogErrorFormat("{0}の{1}が、スリープ中のオブジェクト: {2}の{3}と接触しています",
                    gameObject.scene.name, gameObject.name, other.gameObject.scene.name, other.gameObject.name);
            }
            else
            {
                ValidateCameraSwitch(other);
                ApplyCamera(false);
                fflag = false;
            }
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

    protected void ValidateCameraSwitch(Collider other)
    {
        var sceneName = gameObject.scene.name;
        if (sceneName.StartsWith("ステージ"))
        {
            int stageNum = int.Parse(sceneName.Substring("ステージ".Length));

            if (currentActiveStage != -1
                && stageNum != currentActiveStage + 1
                && !(stageNum == 1 && currentActiveStage == 13))
            {
                // カメラがステージ単位で順番に切り替わっていない疑いを検知した
                Debug.LogErrorFormat("カメラはステージ{0}にありましたが、{1}の{2}が、{3}の{4}と接触し、切り替わりました",
                    currentActiveStage, gameObject.scene.name, gameObject.name, other.gameObject.scene.name, other.gameObject.name);
            }
            currentActiveStage = stageNum;
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
