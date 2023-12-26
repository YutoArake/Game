using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollideer : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            animator.SetTrigger("Gethit"); // ƒvƒŒƒCƒ„[‚ÌUŒ‚‚ª“–‚½‚Á‚½‚ç”­¶
        }
    }
}
