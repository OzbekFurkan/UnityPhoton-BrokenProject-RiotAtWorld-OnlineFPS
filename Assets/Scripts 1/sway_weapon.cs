using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sway_weapon : MonoBehaviour
{
    #region variables


    public float intensity;
    public float smooth;
    private Quaternion originrotation;




    #endregion



    //dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd


    #region monobehaviour callbacks

    private void Start()
    {
        originrotation = transform.localRotation;



    }
    private void Update()
    {

        updatesway();


    }



    #endregion

    //dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd


    #region private methods


    private void updatesway()
    {

        float xmouse = Input.GetAxis("Mouse X");
        float ymouse = Input.GetAxis("Mouse Y");

        Quaternion xadj = Quaternion.AngleAxis(-intensity*xmouse, Vector3.up);
        Quaternion yadj = Quaternion.AngleAxis(intensity * ymouse, Vector3.right);
        Quaternion targetrotation = originrotation * xadj*yadj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetrotation, Time.deltaTime*smooth);

    }





    #endregion
    

    
    
}
