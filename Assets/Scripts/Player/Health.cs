﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable {
    [SerializeField] private int maxHP = 10;
    [SerializeField] private PlayerVariable hpContainer;
    public int HP { get { return hpContainer.CurrentHP; } private set { hpContainer.CurrentHP = value; } }

    public event OnDie onDieEvent;

    private void Start () {
        HP = maxHP;
    }

    public void ChangeHealth (int amount) {
        HP += amount;

        if (HP <= 0) {
            Die ();
        }
    }

    private void Update () {
        if (transform.position.y < -15 || transform.position.y > 13) {
            Die ();
        }
    }

    void Die () {
        if (onDieEvent != null)
            onDieEvent ();
        gameObject.SetActive (false);

        Respawn ();
    }

    public void Respawn () {
        GameManager.Instance.RunPlayerSpawnCoroutine (gameObject);
    }
}