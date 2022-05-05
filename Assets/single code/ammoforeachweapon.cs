using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoforeachweapon : MonoBehaviour
{
    public int allammo = 120;
    public int maxammo = 30;
    public int currentammo;
    // Start is called before the first frame update
    void Start()
    {
        allammo = allammo - maxammo;
        currentammo = maxammo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
