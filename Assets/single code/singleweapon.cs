using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class singleweapon : MonoBehaviour
{
    public float damage = 30f;
    public float range = 100f;
    public Camera fpscam;
    public ParticleSystem muzzleflash;
    public GameObject impacteffect;
    public GameObject blooedeffect;
    public float impactforce = 30f;
    public float firerate = 30f;
    public float nexttimetofire = 0f;
    private bool isreloading = false;
    int allammo;
    public int maxammo;
    public static float currentammo;
    public float reloadtime = 1f;
    public Animator reloadanimator;
    public TextMeshProUGUI currentammotxt;
    public TextMeshProUGUI allammotxt;

    public Animator anmt;
    public GameObject showdamage;


    public AudioSource ads;
    public AudioClip shootsound;




    // Start is called before the first frame update
    void Start()
    {


        
       

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount>1)
        {
            allammo = transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().allammo;
            maxammo = transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().maxammo;
            currentammo = transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().currentammo;
        }


        if (showdamage != null)
        {
            showdamage.transform.LookAt(transform);
        }
        currentammotxt.GetComponent<TextMeshProUGUI>().text = currentammo + " / ";
        allammotxt.GetComponent<TextMeshProUGUI>().text = allammo + "";
        if (isreloading)
        {
            return;
        }
        if (currentammo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (allammo <= 0)
                return;

            StartCoroutine(reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nexttimetofire && transform.childCount > 1)
        {
            
            ads.PlayOneShot(shootsound);
            nexttimetofire = Time.time + 1f / firerate;
            reloadanimator.SetBool("shooting", true);

            anmt.SetBool("isshooting", true);
            anmt.SetBool("ismoving", false);
            transform.localEulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(-7, 0, 0), 7);

            shoot();

        }
        else if (Input.GetButtonUp("Fire1"))
        {

            StartCoroutine(endshooting());
        }


    }

    IEnumerator endshooting()
    {

        yield return new WaitForSeconds(0.15f);
        transform.localEulerAngles = Vector3.Lerp(new Vector3(-7, 0, 0), new Vector3(0, 0, 0), 7);
        yield return new WaitForSeconds(0.15f);
        reloadanimator.SetBool("shooting", false);

        anmt.SetBool("isshooting", false);

    }


    IEnumerator reload()
    {
        isreloading = true;
        reloadanimator.SetBool("shooting", false);
        reloadanimator.SetBool("reloading", true);
        anmt.SetBool("isshooting", false);
        yield return new WaitForSeconds(1.2f);

        reloadanimator.SetBool("reloading", false);

        isreloading = false;
        transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().currentammo = transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().maxammo;
        transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().allammo = transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().allammo - transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().maxammo;
    }







    void shoot()
    {
        muzzleflash.Play();

        transform.GetChild(1).gameObject.GetComponent<ammoforeachweapon>().currentammo--;


        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {

            bot_sgl_tkdmg tkdmg = hit.transform.GetComponent<bot_sgl_tkdmg>();
            //target trgt = hit.transform.GetComponent<target>();

            if (tkdmg != null)
            {
                tkdmg.gameObject.GetComponent<bot_sgl_tkdmg>().TakeDamage(damage);

            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactforce);
                if (hit.rigidbody.gameObject.tag == "barel")
                {
                    hit.rigidbody.gameObject.GetComponent<barelexplosion>().readytoexp = true;
                }
            }
            if (hit.collider.transform.root.gameObject.tag != "Player")
            {
                GameObject impactgo = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactgo, 2f);
            }
            else if (hit.collider.transform.root.gameObject.tag == "Player")
            {
                GameObject impactgo = Instantiate(blooedeffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactgo, 2f);
                GameObject dmgshw = Instantiate(showdamage, hit.point, Quaternion.LookRotation(transform.position));
                showdamage.GetComponent<TextMesh>().text = damage + "";

                Destroy(dmgshw, 0.5f);
            }

        }

    }
}
