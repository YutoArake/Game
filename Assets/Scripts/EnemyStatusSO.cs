using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatusSO : ScriptableObject
{
    public List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();

    [System.Serializable]
    public class EnemyStatus
    {
        [SerializeField] int hp;
        [SerializeField] int attack;

        public int HP { get => hp; set => hp = value; }
    }
}
