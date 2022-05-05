using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textexplotion : MonoBehaviour
{
    bool isupped = false;

    void Start()
    {
       
 StartCoroutine(makeuptrue());
       

    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0,180,0);
        if (!isupped)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + 5, transform.localPosition.z + 1), 0.02f);

        }
        
        if (isupped)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y - 5, transform.localPosition.z - 1.5f), 0.02f);

        }
    }
    IEnumerator makeuptrue()
    {
        yield return new WaitForSeconds(0.1f);
        isupped = true;
    }

}
