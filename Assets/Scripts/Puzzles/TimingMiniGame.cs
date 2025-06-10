using System;
using UnityEngine;

public class TimingMiniGame : Puzzle
{
    [SerializeField] Laser laser;
    [SerializeField] UIMiniGame game1;
    [SerializeField] UIMiniGame game2;
    private bool minigameStarted = false;
    private bool isPuzzleOver;

    protected override void PuzzleOver()
    {
        isPuzzleOver = true;
    }
    private void Update()
    {
        if (isPuzzleOver) return;
        if (button1.buttonPressed && button2.buttonPressed) { 
            CameraManager.Instance.SwitchMode(CameraManager.mode.UpDown);
            game1.StartMiniGame(button1.player);
            game2.StartMiniGame(button2.player);
            minigameStarted = true;
            //start mini game;

        }
        if (minigameStarted)
        {
            if (game1.pressedCorrectly && game2.pressedCorrectly)
            {
                game1.StopMiniGame();
                game2.StopMiniGame();
                laser.DisableLaser();
                
                PuzzleOver();

            }
        }
    }


}
