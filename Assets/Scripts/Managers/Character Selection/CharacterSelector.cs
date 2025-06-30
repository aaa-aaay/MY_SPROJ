using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelector : MonoBehaviour
{
    public int playerIndex;
    public CharacterSlot[] characterSlots;
    private int currentIndex = 1;
    private int previousIndex = -1; // Track previously used index
    private bool hasConfirmed = false;
    private Image image;

    public void SetSlots(CharacterSlot[] characterSlots, int playerIndex)
    {
        this.characterSlots = characterSlots;
        image = GetComponent<Image>();
        UpdateSlotAssignment(); // Assign initial slot
        this.playerIndex = playerIndex;
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (hasConfirmed) return;

        Vector2 input = context.ReadValue<Vector2>();

        if (input.x > 0.5f)
        {
            int nextIndex = Mathf.Min(currentIndex + 1, characterSlots.Length - 1);
            if (nextIndex != currentIndex && characterSlots[nextIndex].isSlotTaken == false)
            {
                previousIndex = currentIndex;
                currentIndex = nextIndex;
                UpdateSlotAssignment();
            }
        }
        else if (input.x < -0.5f)
        {
            int prevIndex = Mathf.Max(currentIndex - 1, 0);
            if (prevIndex != currentIndex && characterSlots[prevIndex].isSlotTaken == false)
            {
                previousIndex = currentIndex;
                currentIndex = prevIndex;
                UpdateSlotAssignment();
            }
        }
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (!context.performed || hasConfirmed) return;

        if (currentIndex == 1) return;



        hasConfirmed = true;
        FadeImage(hasConfirmed);
        int characterIndex = 0;

        if (currentIndex == 0) {
            characterIndex = 0;
        }
        if (currentIndex == 2) {
            characterIndex = 1;
        }

        InputDevice device = context.control.device;

        PlayerSelectionManager.Instance.SetCharacterForPlayer(playerIndex, characterIndex, device);
        Debug.Log(playerIndex + "Locked in");
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(!hasConfirmed) return;
        hasConfirmed = false;
        FadeImage(hasConfirmed);
    }

    private void UpdateSlotAssignment()
    {
        if (previousIndex >= 0 && previousIndex < characterSlots.Length)
        {
            characterSlots[previousIndex].setSlotTaken(false);
        }

        characterSlots[currentIndex].AssignObject(gameObject);
    }

    private void FadeImage(bool fade)
    {
        if (image == null) return;

        Color color = image.color;
        color.a = fade ? 0.5f : 1f; // 50% transparent if confirmed
        image.color = color;
    }


}
