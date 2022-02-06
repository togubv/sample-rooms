using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        animator.SetBool("Fade", true);
    }

    public void FadeOut()
    {
        animator.SetBool("Fade", false);
    }
}
