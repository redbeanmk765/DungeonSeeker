using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public Monster monsterStat;
    public abstract class BaseState
    {
        protected enemy curEnemy;
        public GameObject curPlayer;
        protected BaseState(enemy enemy, GameObject player)
        {
            curEnemy = enemy;
            curPlayer = player;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }


    public class FSM
    {
        public FSM(BaseState initState)
        {
            curState = initState;
            ChangeState(curState);
        }

        private BaseState curState;

        public void ChangeState(BaseState nextState)
        {
            if (nextState == curState)
                return;

            if (curState != null)
                curState.OnStateExit();

            curState = nextState;
            curState.OnStateEnter();
        }
            
        public void UpdateState()
        {
            if (curState != null)
                curState.OnStateUpdate();
        }
    }
}
