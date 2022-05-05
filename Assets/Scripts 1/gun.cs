using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class gun : MonoBehaviour
{

    public float damage = 40f;
    public float range = 100f;
    public Camera fpscam;
    public ParticleSystem muzzleflash;
    public GameObject impacteffect;
    public GameObject blooedeffect;
        public float impactforce = 30f;
    public float firerate = 6f;
    public float nexttimetofire = 0f;
    private bool isreloading = false;
    int allammo;
    public int maxammo;
    public static float currentammo;
    public float reloadtime = 1f; 
    public Animator reloadanimator;
    public TextMeshProUGUI currentammotxt;
    public TextMeshProUGUI allammotxt;
    public GameObject showdamage;

    public Animator anmt;

    public AudioSource ads;
    public AudioClip shootsound;

    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            allammo = 120;
            maxammo = 30;
            allammo = allammo - maxammo;
            currentammo = maxammo;
        }
    }

    void Update()
    {
       

        if (GetComponent<PhotonView>().IsMine)
        {
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
                GetComponent<PhotonView>().RPC("reload", RpcTarget.All, null);
               // StartCoroutine(reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= nexttimetofire)
            {
                ads.PlayOneShot(shootsound);
                nexttimetofire = Time.time + 1f / firerate;
                reloadanimator.SetBool("shooting", true);
                anmt.SetBool("isshooting", true);
                anmt.SetBool("ismoving", false);
                GetComponent<PhotonView>().RPC("shoot", RpcTarget.All, null);
                transform.localEulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(-7, 0, 0), 7);
                //shoot();

            }
            else if(Input.GetButtonUp("Fire1"))
            {
                GetComponent<PhotonView>().RPC("endshooting", RpcTarget.All, null);
                //StartCoroutine(endshooting());
            }
        }
        
    }

    [PunRPC]
    IEnumerator endshooting()
    {

        yield return new WaitForSeconds(0.15f);
        transform.localEulerAngles = Vector3.Lerp(new Vector3(-7, 0, 0), new Vector3(0, 0, 0), 7);
        yield return new WaitForSeconds(0.15f);
        reloadanimator.SetBool("shooting", false);

        anmt.SetBool("isshooting", false);

    }

    [PunRPC]
    IEnumerator reload()
    {
        isreloading= true;
        reloadanimator.SetBool("shooting", false);
        reloadanimator.SetBool("reloading", true);
        anmt.SetBool("isshooting", false);
        yield return new WaitForSeconds(1.2f);
        reloadanimator.SetBool("reloading", false);
        
        isreloading = false;
        currentammo = maxammo;
        allammo = allammo - maxammo;
    }

    [PunRPC]
    void shoot()
    {
        muzzleflash.Play();
        if (GetComponent<PhotonView>().IsMine)
        {

            currentammo--;

           
            RaycastHit hit;
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
            {
                bool applydamage = false;
                if(hit.collider.transform.root.gameObject.GetComponent<playermovement>().awayTeam != tmdmscript.IsAwayTeam)
                {
                    applydamage = true;
                }
                //target trgt = hit.transform.GetComponent<target>();
                if (applydamage)
                {

                    takedamage tkdmg = hit.transform.GetComponent<takedamage>();

                    if (tkdmg != null)
                    {
                        //trgt.TakeDamage(damage);
                        tkdmg.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, damage, PhotonNetwork.LocalPlayer.ActorNumber);
                    }
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactforce);
                    }
                    
                    if(hit.collider.transform.root.gameObject.tag!="Player")
                    {
                       GameObject impactgo = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
                       Destroy(impactgo, 2f);
                    }
                    else if (hit.collider.transform.root.gameObject.tag == "Player")
                    {
                        GameObject impactgo = Instantiate(blooedeffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impactgo, 2f);
                        GameObject dmgshw = Instantiate(showdamage, hit.point, Quaternion.LookRotation(transform.position));
                        showdamage.GetComponent<TextMesh>().text = damage+"";
                        
                        Destroy(dmgshw, 0.5f);
                    }

                }
            }
        }
    }
   
  
}
