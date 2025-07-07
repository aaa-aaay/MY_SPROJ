using UnityEngine;

public class ColorButton : ColorPuzzle
{

    [SerializeField] private Laser Laser;
    protected override void Interact()
    {
        Laser.DisableLaser();
    }
}
