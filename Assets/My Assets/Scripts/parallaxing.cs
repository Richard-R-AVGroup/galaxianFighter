using UnityEngine;
using System.Collections;

public class parallaxing : MonoBehaviour {

    private GameObject[] bgos;
    private float[] parallaxScales;
    public float smoothing;

    private Transform cam;
    private Vector3 prevCamLoc;

    void Awake()
    {
        cam = Camera.main.transform;
    }
	// Use this for initialization
	void Start () {
        prevCamLoc = cam.position;

        bgos = GameObject.FindGameObjectsWithTag("Background");

        parallaxScales = new float [bgos.Length];
        for (int i = 0; i < bgos.Length; i++)
        {
            parallaxScales[i] = bgos[i].gameObject.transform.position.z * -1;
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (bgos.Length > 0)
        {
            for (int i = 0; i < bgos.Length; i++)
            {
                float parallaxX = (prevCamLoc.x - cam.position.x) * parallaxScales[i];
                float parallaxY = (prevCamLoc.y - cam.position.y) * parallaxScales[i];

                float backgroundTargetPosX = bgos[i].gameObject.transform.position.x + parallaxX;

                float backgroundTargetPosY = bgos[i].gameObject.transform.position.y + parallaxY;

                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, bgos[i].gameObject.transform.position.z);

                bgos[i].gameObject.transform.position = Vector3.Lerp(bgos[i].gameObject.transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

            prevCamLoc = cam.position;
        }
	}
}
