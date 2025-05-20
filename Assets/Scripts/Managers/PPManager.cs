using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPManager : MonoBehaviour
{

    public static PPManager Instance;

    [Header("Volume Reference")]
    public Volume p1Volume;
    public Volume p2Volume;

    //private Bloom bloom;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    //private Bloom p2Bloom;
    private Vignette p2Vignette;
    private ColorAdjustments p2ColorAdjustments;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //// Cache components if they exist
        //if (p1Volume.profile.TryGet(out bloom))
        //    bloom.active = false;



        // Cache components if they exist
        //if (p2Volume.profile.TryGet(out p2Bloom))
        //    p2Bloom.active = false;



    }

    public void SetVolunmes(Volume vol, int player)
    {

        if (player == 1) {
            p1Volume = vol;


            if (p1Volume.profile.TryGet(out vignette))
                vignette.active = false;

            if (p1Volume.profile.TryGet(out colorAdjustments))
                colorAdjustments.active = false;
        }
        else
        {
            p2Volume = vol;

            if (p2Volume.profile.TryGet(out p2Vignette))
                p2Vignette.active = false;

            if (p2Volume.profile.TryGet(out p2ColorAdjustments))
                p2ColorAdjustments.active = false;
        }
    }

    //public void EnableBloom(bool enable, int player1)
    //{
    //    if (player1 == 1)
    //    {
    //        if (bloom != null)
    //        {
    //            bloom.active = enable;
    //        }
    //    }
    //    else
    //    {
    //        if (p2Bloom != null)
    //        {
    //            p2Bloom.active = enable;
    //        }
    //    }

    //}

    public void EnableVignette(bool enable, int player1)
    {
        Debug.Log("called");
        if (player1 == 1)
        {
            if(enable) {
                if (vignette != null)
                {
                    Debug.Log("called2");
                    vignette.active = enable;
                    StartCoroutine(RemoveVignette(player1));

                }
            }
            else
            {
                if (vignette != null)
                {
                    Debug.Log("called3");
                    vignette.active = false;

                }
            }

        }
        else
        {
            if (enable)
            {
                if (p2Vignette != null)
                {
                    p2Vignette.active = enable;
                    StartCoroutine(RemoveVignette(player1));
                }
            }
            else
            {
                if (p2Vignette != null)
                {
                    p2Vignette.active = false;

                }
            }

        }

    }

    public void SetBlackAndWhite(bool enable, int player1)
    {
        if (player1 == 1)
        {
            if (colorAdjustments != null)
            {
                colorAdjustments.active = true;
                colorAdjustments.saturation.value = enable ? -100f : 0f;
            }
        }
        else
        {
            if (p2ColorAdjustments != null)
            {
                p2ColorAdjustments.active = true;
                p2ColorAdjustments.saturation.value = enable ? -100f : 0f;
            }
        }

    }

    private IEnumerator RemoveVignette(int playerno)
    {
        yield return new WaitForSeconds(0.2f);
        EnableVignette(false, playerno);
    }
}
