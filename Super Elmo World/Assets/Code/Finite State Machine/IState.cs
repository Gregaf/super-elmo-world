
using UnityEngine;

public interface IState
{

    void Enter();

    void Tick();

    void Exit();

    void OnTriggerEnter(Collider2D collider2D);
}
