using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public abstract class State
    {
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Tick();

    }
}