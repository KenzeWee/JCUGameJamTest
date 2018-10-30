using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour, IInput {
    public enum AISTATE { IDLE, CHASING, AIM, ATTACK, DIE };
    public AISTATE currenState = AISTATE.IDLE;

    [SerializeField]
    private Transform target;

    public bool IsFire { get; private set; }

    public Vector2 LookDirection { get; private set; }

	// Update is called once per frame
	void Update () {
        //Isfire

        //LookDirection 
        RunState();
    }

    private void RunState()
    {
        switch (currenState) {
            case AISTATE.IDLE:

                break;

            case AISTATE.CHASING:
                MoveTo();
                break;

            case AISTATE.AIM:
                Aim();
                break;

            case AISTATE.ATTACK:

                break;

            case AISTATE.DIE:

                break;
        }
    }

    void Aim()
    {
        Vector2 direction = target.position - transform.position;

        LookDirection = direction;
    }

    void MoveTo()
    {
        Vector2 direction = Vector2.zero;

        if (target.position.x > transform.position.x)
        {
            direction = Vector2.down - Vector2.right;
        }else if(target.position.x < transform.position.x)
        {
            direction = Vector2.down + Vector2.right;
        }

        IsFire = true;

        LookDirection = direction;
    }
}
