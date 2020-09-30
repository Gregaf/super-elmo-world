using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public bool isInvincible { get; private set; }

    public void BecomeInvicible(float invincibilityDuration)
    {
        if (isInvincible)
            return;

        StartCoroutine(InvincibilityCouroutine(invincibilityDuration));   
    }


    private IEnumerator InvincibilityCouroutine(float invincibilityDuration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }
}
