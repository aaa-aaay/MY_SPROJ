
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrappleAbility : PlayerAbilities
{
    private Rigidbody2D rb;
    private LineRenderer lr;
    private DistanceJoint2D dj;

    private GameObject grappledObject;
    private List<GameObject> thingsToGrapple = new List<GameObject>();

    private bool grappling;
    private float originalMoveSpeed;
    GameObject closest;
    [SerializeField] private float swingForce = 10.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public override void SetAbilityToPlayer()
    {
        base.SetAbilityToPlayer(); // Run base setup first

        rb = GetComponentInParent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponentInParent<DistanceJoint2D>();
        player = GetComponentInParent<PlayerController>();
        dj.enabled = false;
        lr.enabled = false;
        grappling = false;
        originalMoveSpeed = player.moveSpeed;
    }



    // Update is called once per frame
    void Update()
    {

        if (thingsToGrapple.Count > 0) {
            if (closest != null) { closest.GetComponentInChildren<Canvas>().enabled = false; }
            closest = thingsToGrapple .OrderBy(obj => Vector2.Distance(transform.position, obj.transform.position)) .FirstOrDefault();
            Canvas UI = closest.GetComponentInChildren<Canvas>();
            UI.enabled = true;
           


        }

        // Start grappling
        if (abilityAction1.WasPressedThisFrame() && (thingsToGrapple.Count > 0 || grappling))
        {


            if (closest == null) return;

            grappling = true;
            grappledObject = closest;

            foreach (GameObject node in thingsToGrapple)
            {
                Canvas UI = node.GetComponentInChildren<Canvas>();
                UI.enabled = false;
            }

            // Enable and configure DistanceJoint2D
            dj.enabled = true;

            Rigidbody2D parentRb = closest.GetComponentInParent<Rigidbody2D>();
            if (parentRb != null)
            {
                dj.connectedBody = parentRb;
                dj.connectedAnchor = parentRb.transform.InverseTransformPoint(closest.transform.position);
            }
            else
            {
                dj.connectedBody = null;
                dj.connectedAnchor = closest.transform.position;
            }

            Vector2 anchorWorldPos;
            if (dj.connectedBody != null)
            {
                anchorWorldPos = dj.connectedBody.transform.TransformPoint(dj.connectedAnchor);
            }
            else
            {
                anchorWorldPos = dj.connectedAnchor;
            }




            dj.distance = Vector2.Distance(rb.position, anchorWorldPos);

            // LineRenderer setup
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, closest.transform.position);

            // Disable horizontal movement while grappling
            player.disableHorizontalMove = true;
        }

        // Stop grappling
        if (abilityAction1.WasReleasedThisFrame())
        {
            lr.enabled = false;
            dj.enabled = false;
            grappling = false;
            player.disableHorizontalMove = false;
        }

        // While grappling, apply swing force and update rope visuals
        if (grappling && grappledObject != null)
        {
            // Update the LineRenderer
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, grappledObject.transform.position);

            // Apply swinging force
            Vector2 ropeDir = (grappledObject.transform.position - transform.position).normalized;
            Vector2 swingDir = new Vector2(ropeDir.y, -ropeDir.x); // perpendicular
            Vector2 moveInput = player.moveAction.ReadValue<Vector2>();

            rb.AddForce(swingDir * moveInput.x * swingForce);

            // Debug line
            Debug.DrawRay(transform.position, swingDir.normalized * moveInput.x * 2f, Color.red);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Grapple Node"))
        {
            thingsToGrapple.Add(collision.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Grapple Node"))
        {
            thingsToGrapple.Remove(collision.gameObject);

        }
    }
}
