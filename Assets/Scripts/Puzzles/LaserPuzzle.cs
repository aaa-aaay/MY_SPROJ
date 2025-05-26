using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserPuzzle : MonoBehaviour
{

    [SerializeField] Laser[] lasers;
    [SerializeField] PuzzleButton button;
    [SerializeField] PuzzleButton buttonFinish;
    private bool puzzleState;

    private void Start()
    {
        lasers[1].DisableLaser();
        puzzleState = true;
    }

    private void Update()
    {
        if (button.buttonPressed == true) { 

            ChangeStates();
            puzzleState = !puzzleState;
            button.buttonPressed = false;
        }
        if (buttonFinish != null && buttonFinish.buttonPressed == true) {

            lasers[lasers.Count() - 1].DisableLaser();
        
        }
    }

    private void ChangeStates()
    {
        if (puzzleState)
        {
            lasers[1].EnableLaser();
            lasers[0].DisableLaser();
            lasers[2].DisableLaser();
        }
        else
        {
            lasers[1].DisableLaser();
            lasers[0].EnableLaser();
            lasers[2].EnableLaser();
        }
    }


}
