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

    private bool grappling;
    private GameObject grappledObject;
    private List<GameObject> thingsToGrapple = new List<GameObject>();


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

        }
        if (player.abilityAction.WasReleasedThisFrame()) { 
            lr.enabled = false;
            dj.enabled = false;
            grappling = false;
        }

        if (grappling) {


            dj.enabled = true;
            dj.connectedBody = null;
            dj.distance = Vector2.Distance(rb.position, grappledObject.transform.position);

            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, grappledObject.transform.position);


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
