using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffect : MonoBehaviour
{
    public ParticleSystem rippleEffect;
    public void RippleEffectParticle()
    { 
        rippleEffect.Play(); 
    }
}
