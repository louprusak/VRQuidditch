using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{

    private Transform monTransform;

    public GameObject mainBalai;
    private Transform mainTransform;

    public GameObject milieu;
    private Transform milieuTransform;

    public float coefVitesseHor = 0.8f;
    public float coefVitesseVert= 0.8f;
    public float limiteHor = 1f;
    public float limiteVert = 1f;

    public SteamVR_Action_Vector2 touchpadInput;


    // Start is called before the first frame update
    void Start()
    {
        monTransform = GetComponent<Transform>();
        mainTransform = mainBalai.GetComponent<Transform>();
        milieuTransform = milieu.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float distMainTeteHor = Vector3.Distance(new Vector3(mainTransform.position.x, 0, mainTransform.position.z), new Vector3(milieuTransform.position.x,0, milieuTransform.position.z));
        float distMainTeteVert = mainTransform.position.y - milieuTransform.position.y;
        float vitesseHor = 0;
        float vitesseVert = 0;

        if (Mathf.Abs(distMainTeteHor) > limiteHor)
        {
            vitesseHor = coefVitesseHor * (distMainTeteHor - limiteHor) * Time.deltaTime;
        }
        if (Mathf.Abs(distMainTeteVert) > limiteVert)
        {
            vitesseVert = coefVitesseVert * (distMainTeteVert - limiteVert) * Time.deltaTime;
        }
            
        //Horizontal

        Vector3 delta = mainTransform.position - milieuTransform.position;
        Quaternion look = Quaternion.LookRotation(delta);
        float horizontal = look.eulerAngles.y;
        float vertical = look.eulerAngles.x;

        monTransform.Translate( new Vector3(Mathf.Sin(Mathf.Deg2Rad *horizontal)* vitesseHor,  vitesseVert, Mathf.Cos(Mathf.Deg2Rad *horizontal)) * vitesseHor);

    }
}
