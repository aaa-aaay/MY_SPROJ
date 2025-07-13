using UnityEngine;

public class ColorButton : ColorPuzzle
{

    [SerializeField] private Laser Laser;
    [SerializeField] private GameObject greenBar;
    [SerializeField] private float amountToActive = 3;


    private RectTransform greenBarRect;
    private float currentFill;

    [SerializeField]private ColorButton otherColoredButton;
    public bool barFilled;



    private void Start()
    {
        greenBarRect = greenBar.GetComponent<RectTransform>();
        currentFill = 0;
        greenBarRect.localScale = new Vector3(0f, 1f, 1f);
    }

    public override void Interact()
    {
        currentFill++;
        float fillRatio = Mathf.Clamp01(currentFill / amountToActive);
        greenBarRect.localScale = new Vector3(fillRatio, 1f, 1f);


        if (fillRatio >= 1f)
        {
            barFilled = true;
        }


    }


    private void Update()
    {
        if (otherColoredButton != null)
        {

            if (otherColoredButton.barFilled && barFilled) { Laser.DisableLaser(); }



        }
        else
        {
            if (barFilled && Laser != null) { Laser.DisableLaser(); }
        }
    }
}
