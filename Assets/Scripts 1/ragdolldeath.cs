using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdolldeath : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator = null;

    private Rigidbody[] ragdollbodies;
    private Collider[] ragdollcolliders;
    // Start is called before the first frame update
    private void Start()
    {
        ragdollbodies = GetComponentsInChildren<Rigidbody>();
        ragdollcolliders = GetComponentsInChildren<Collider>();
        ToggleRagdoll(false);
    }

    public void die()
    {
        ToggleRagdoll(true);
        foreach(Rigidbody rb in ragdollbodies)
        {
            rb.AddExplosionForce(107f, transform.position, 5f, 0.01f, ForceMode.Impulse);
        }
        
    }

    private void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;
        foreach(Rigidbody rb in ragdollbodies)
        {
            rb.isKinematic = !state;
        }

        /*foreach (Collider collider in ragdollcolliders)
        {
            collider.enabled = state;
        }*/

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
