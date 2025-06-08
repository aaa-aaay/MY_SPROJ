using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class GrappleAbility : MonoBehaviour
{
    private Rigidbody2D rb;
    private LineRenderer lr;
    private DistanceJoint2D dj;
    private PlayerController player;

    private GameObject grappledObject;
    private List<GameObject> thingsToGrapple = new List<GameObject>();

    private bool grappling;
    private float originalMoveSpeed;
    [SerializeField] private float swingForce = 10.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        if (player.abilityAction.WasPressedThisFrame() && (thingsToGrapple.Count > 0 || grappling)) {
            GameObject closest = thingsToGrapple.OrderBy(obj => Vector2.Distance(transform.position, obj.transform.position)).FirstOrDefault();

            grappling = true;
            grappledObject = closest;

            dj.enabled = true;
            dj.connectedAnchor = closest.transform.position;
            dj.connectedBody = null;
            dj.distance = Vector2.Distance(rb.position, closest.transform.position);

            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, closest.transform.position);

            player.disableHorizontalMove = true; // prevent player movement reseting grapple momentum, also prevents p[ayer dashing while while grappling

        }
        if (player.abilityAction.WasReleasedThisFrame()) { 
            lr.enabled = false;
            dj.enabled = false;
            grappling = false;

            player.disableHorizontalMove = false;
        }

        if (grappling) {


            dj.enabled = true;
            dj.connectedBody = null;
            dj.distance = Vector2.Distance(rb.position, grappledObject.transform.position);

            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, grappledObject.transform.position);


            Vector2 ropeDir = (grappledObject.transform.position - transform.position).normalized;
            Vector2 swingDir = new Vector2(ropeDir.y, -ropeDir.x); // Corrected direction

            Vector2 moveInput = player.moveAction.ReadValue<Vector2>();

            rb.AddForce(swingDir * moveInput.x * swingForce);

            Vector2 forceDir = swingDir * moveInput.x;  
            Debug.DrawRay(transform.position, forceDir.normalized * 2f, UnityEngine.Color.red);


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
