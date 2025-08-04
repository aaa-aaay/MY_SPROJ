using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;
    private Coroutine StopMotorAfterTimeCoroutine;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void RumblePulse(float lowFreq, float highFreq, float duration, PlayerInput input)
    {

        Gamepad gamepad = input.devices.OfType<Gamepad>().FirstOrDefault();
        //get refernce to game pad;

        if (gamepad != null) {

            gamepad.SetMotorSpeeds(lowFreq, highFreq);
            StopMotorAfterTimeCoroutine = StartCoroutine(StopRumble(duration, gamepad));




        }
    }



    private IEnumerator StopRumble(float duration, Gamepad  pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration) { 
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0,0);

    }


}
