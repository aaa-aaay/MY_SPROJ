using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class TutorialPlayer : MonoBehaviour
{
    [SerializeField] private List<VideoClip> videos = new List<VideoClip>();
    [SerializeField] VideoPlayer videopPlayer;

    [SerializeField] Canvas toturialCanvas;

    public bool showingClips;
    private int pageCount;


    private void Start()
    {
        toturialCanvas.enabled = false;
        pageCount = 0;
    }

    public void ShowToturial()
    {
        pageCount = 0;
        toturialCanvas.enabled = true;

        PlayerManager.Instance.player1.FreezePlayer(true);
        PlayerManager.Instance.player2.FreezePlayer(true);
        NextToturial();
        showingClips = true;
    }


    private void Update()
    {
        if (!showingClips) return;

        if (PlayerManager.Instance.player1.jumpAction.WasPressedThisFrame() || PlayerManager.Instance.player2.jumpAction.WasPressedThisFrame()) {
            NextToturial();
            AudioManager.instance.PlaySFX("UIConfirm");
        }
    }

    private void NextToturial()
    {
        if (pageCount >= videos.Count || videos[pageCount] == null)
        {
            FinishTut();
            return;
        }
        videopPlayer.clip = videos[pageCount];
        videopPlayer.Play();
        pageCount++;


    }

    private void FinishTut()
    {
        PlayerManager.Instance.player1.FreezePlayer(false);
        PlayerManager.Instance.player2.FreezePlayer(false);

        toturialCanvas.enabled = false;
        showingClips = false;
    }



}
