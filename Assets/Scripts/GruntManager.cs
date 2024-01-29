using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] EnemyStatusSO enemyStatus;
    [SerializeField] PlayerStatusSO playerStatus;
    private NavMeshAgent agent;
    public float speed = 3.0f;
    public float distance;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        currentHP = enemyStatus.enemyStatusList[0].HP;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, this.transform.position);

        if (distance < 10.0f )
        {
            agent.destination = target.position;
        }

        if(currentHP < 0 )
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            currentHP -= playerStatus.GetATTACK();
        }
    }
}
