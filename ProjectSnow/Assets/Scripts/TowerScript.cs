using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public SpriteRenderer selectedRenderer;

    private Camera cameraRef;

    private bool canRotate = true;
    private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraRef = GameObject.Find("CameraHolder/PlayerCamera").gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        if (canRotate)
        {
            if (cameraRef != null)
            {
                transform.LookAt(cameraRef.transform.position, -Vector3.up);
                transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
                transform.forward = new Vector3(cameraRef.transform.forward.x, transform.forward.y, cameraRef.transform.forward.z);
            }
        }
    }

    public void SelectTower(bool shouldSelect)
    {
        selected = shouldSelect;

        selectedRenderer.enabled = shouldSelect;
    }
}
