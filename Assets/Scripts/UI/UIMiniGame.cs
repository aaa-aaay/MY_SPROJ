using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniGame : MonoBehaviour
{
    public RectTransform movingBar;     // green bar
    public RectTransform targetArea;    // green bar (1)
    private GreenBarMover greenBar;
    private PlayerController playerController;
    [HideInInspector]public bool pressedCorrectly = false;
    [SerializeField] private float timeForDisable = 2.0f;


    private void Start()
    {
        greenBar = movingBar.GetComponent<GreenBarMover>();
    }

    public void StartMiniGame(PlayerController player)
    {
        gameObject.SetActive(true);
        playerController = player;
    }
    public void StopMiniGame() {
        greenBar.moveSpeed = 0;
        playerController.FreezePlayer(false);
        StartCoroutine(DisableObjectAfterSeconds(timeForDisable));

    }



    private IEnumerator DisableObjectAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerController.interactAction.WasPressedThisFrame())
        {
            if (IsOverlapping())
            {
                Debug.Log("Puzzle Complete!");
                pressedCorrectly = true;
            }
            else
            {
                Debug.Log("Failed - Not aligned!");
            }
        }
        if (pressedCorrectly && !IsOverlapping()) { pressedCorrectly = false; }
    }

    bool IsOverlapping()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(targetArea, movingBar.position, null);
    }
}
