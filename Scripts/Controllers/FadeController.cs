using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private GameObject fade;
    private Animator anim;

    [SerializeField] private string animBoolName;

    void Awake()
    {
        anim = fade.GetComponent<Animator>();
    }

    public void StartingFade()
    {
        anim.SetBool("bStarting", true);
    }

    public void FadeIn()
    {
        anim.SetBool(animBoolName, true);
    }

    public void FadeOut()
    {
        anim.SetBool(animBoolName, false);
    }
}
