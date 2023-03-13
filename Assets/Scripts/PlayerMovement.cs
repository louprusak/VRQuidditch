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

    public float coefVitesse = 0.1f;
    public float vertVitesse = 1f;
    public float limite = 1f;

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
        float distMainTete = Vector3.Distance(mainTransform.position, milieuTransform.position);
        float vitesse = 0;
        if (distMainTete > limite)
            vitesse = coefVitesse * (distMainTete - limite);

        //Horizontal

        Vector3 delta = mainTransform.position - milieuTransform.position;
        Quaternion look = Quaternion.LookRotation(delta);
        float horizontal = look.eulerAngles.y;
        float vertical = look.eulerAngles.x;

        monTransform.Translate( new Vector3(Mathf.Sin(Mathf.Deg2Rad *horizontal), -Mathf.Sin(Mathf.Deg2Rad * vertical) * vitesse, Mathf.Cos(Mathf.Deg2Rad *horizontal)) * vitesse );


        //Vertical

        //float value = Input.GetAxis("Vertical");
        //monTransform.Translate(new Vector3(0,  -vitesse * touchpadInput.axis.y* vertVitesse,0));
    }
}
