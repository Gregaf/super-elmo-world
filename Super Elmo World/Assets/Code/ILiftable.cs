using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiftable
{
    
    GameObject Lift();

    void Release(Vector2 releaseVector);

}
