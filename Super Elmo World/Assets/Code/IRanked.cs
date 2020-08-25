using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRanked
{
    void Promote(int level, int specifiedID);
    

    void Demote();
}
