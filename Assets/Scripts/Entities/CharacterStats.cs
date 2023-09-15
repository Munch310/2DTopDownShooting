using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

[Serializable] // 직렬화가 가능하도록
public class CharacterStats
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;

    // 공격 데이터 스크립터블 오브젝트
    public AttackSO attakSO;
}

