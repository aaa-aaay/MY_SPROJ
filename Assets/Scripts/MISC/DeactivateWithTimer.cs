using System.Collections;
using UnityEngine;

public class DeactivateWithTimer : MonoBehaviour
{

    [SerializeField] Laser[] lasers;
    [SerializeField] PuzzleButton button;
    [SerializeField] float reactivationTime = 3;
    private bool lasersDeactivated;

    // Update is called once per frame
    void Update()
    {
        if (button.buttonPressed && !lasersDeactivated) {

            button.buttonPressed = false;
            lasersDeactivated = true;
            foreach (var lasser in lasers) lasser.DisableLaser();
            StartCoroutine(ResetLasers());

        }
    }

    private IEnumerator ResetLasers()
    {
        yield return new WaitForSeconds(reactivationTime);

        foreach (var lasser in lasers)
        {

            lasser.EnableLaser();
            lasersDeactivated = false;


        }
    }
}
