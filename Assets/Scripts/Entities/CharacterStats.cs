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

[Serializable] // ����ȭ�� �����ϵ���
public class CharacterStats
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;

    // ���� ������ ��ũ���ͺ� ������Ʈ
    public AttackSO attakSO;
}

