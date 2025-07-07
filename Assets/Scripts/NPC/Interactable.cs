using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected List<PlayerController> playersInRange = new List<PlayerController>();

    [SerializeField] GameObject interactPrefab;
    private bool enableInteract;
    protected bool interacting;


    protected virtual void Start()
    {
        enableInteract = true;
        interacting = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && !playersInRange.Contains(player))
        {
            playersInRange.Add(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playersInRange.Remove(player);
        }

        if (playersInRange.Count == 0 && interactPrefab != null)
            interactPrefab.SetActive(false);
    }

    private void Update()
    {

        if (!enableInteract) return;

        if (interacting) {
            Interacting();
            return;
        }

        for (int i = 0; i < playersInRange.Count; i++)
        {
            PlayerController player = playersInRange[i];

            if (interactPrefab != null)
                interactPrefab.SetActive(true);

            if (player.interactAction.WasPressedThisFrame())
            {
                OnInteract();
            }
        }



    }

    protected abstract void OnInteract();

    protected abstract void Interacting();
}
