using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f; // UŒ‚‚ÌŠÔŠu
    public int attackDamage = 10; // ƒ_ƒ[ƒW

    private GameObject player; // ƒvƒŒƒCƒ„[
    private bool playerInRange; // UŒ‚”ÍˆÍ“à‚É‚¢‚é‚©‚Ç‚¤‚©
    private float timer; // ŽžŠÔ

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Attack()
    {
        timer = 0.0f;
    }
}
