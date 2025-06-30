using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public bool isSlotTaken;


    private void Start()
    {
    }
    public void AssignObject(GameObject obj)
    {
        if (isSlotTaken) return;


        obj.transform.position = gameObject.transform.position;
        isSlotTaken = true;
    }

    public void setSlotTaken(bool isSlotTaken)
    {
        this.isSlotTaken = isSlotTaken;
    }
    

}
