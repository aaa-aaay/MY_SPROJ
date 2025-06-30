using UnityEngine;
using UnityEngine.InputSystem;

public class SlotsManager : MonoBehaviour
{
    [SerializeField] GameObject player2Prefab;
    //[SerializeField] RectTransform[] characterSlots;


    [SerializeField] CharacterSlot[] initalSlots;

    [SerializeField] CharacterSlot[] characterSlots1;

    [SerializeField] CharacterSlot[] characterSlots2;
    private int joinedPlayerCount = 0;
   
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if (joinedPlayerCount >= 2)
        {
            Debug.LogWarning("Too many players for available spawn points!");
            return;
        }
        PlayerInputManager.instance.playerPrefab = player2Prefab;
        // Move the newly spawned player to the assigned spawn point
        initalSlots[joinedPlayerCount].AssignObject(playerInput.gameObject);

        //playerInput.transform.position = characterSlots[joinedPlayerCount].gameObject.transform.position;
        if (joinedPlayerCount == 0) {
            playerInput.GetComponent<CharacterSelector>().SetSlots(characterSlots1, 0);

        }
        else
        {
            playerInput.GetComponent<CharacterSelector>().SetSlots(characterSlots2, 1);
        }


        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            playerInput.transform.SetParent(canvas.transform, worldPositionStays: true);
            playerInput.transform.SetAsLastSibling();
        }

        joinedPlayerCount++;
    }
}
