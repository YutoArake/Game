using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStatusSO : ScriptableObject
{
    [SerializeField] int hp;
    [SerializeField] int attack;

    public int HP { get => hp; }

    public int GetATTACK()
    {
        return attack;
    }
}
