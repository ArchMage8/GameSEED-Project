using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D), typeof(LineRenderer), typeof(Rigidbody2D))]
public class Swing_Player : MonoBehaviour
{
    [Header("Player Settings")]
    public bool inRange = false;

    private bool isSwinging = false;
    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;
    private Rigidbody2D playerRb;
    private PlayerMovement playerMovement;

    [Header("Swing Force Settings")]
    [SerializeField] private float swingForce = 5f;

    [Header("Line Settings")]
    [SerializeField] private float lineAppearDuration = 0f; // Time for the line to appear

    private Transform detectorTransform;

    private void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (inRange && !playerMovement.isGrounded && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            if (!isSwinging)
            {
                StartSwing();
            }
        }
        
        else if (isSwinging && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.L)))
        {
            EndSwing();
        }

        // Continuously update the line renderer if swinging
        if (isSwinging && detectorTransform != null)
        {
            UpdateLineRenderer();
        }
    }

    private void StartSwing()
    {
        distanceJoint.enabled = true;
        playerMovement.enabled = false;
        ApplySwingForce();
        isSwinging = true;
        StartCoroutine(AnimateLineAppearance()); // Animate the line appearance
    }

    private void ApplySwingForce()
    {
        float forceDirection = playerMovement.transform.localScale.x > 0 ? -swingForce : swingForce;
        playerRb.AddForce(new Vector2(forceDirection, 0f), ForceMode2D.Impulse);
    }

    private void EndSwing()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        distanceJoint.connectedBody = null;
        playerMovement.enabled = true;
        isSwinging = false;
    }

    public void SetSwingTarget(Rigidbody2D target, Transform detectorPos)
    {
        distanceJoint.connectedBody = target;
        detectorTransform = detectorPos;
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, transform.position);   // Player position
        lineRenderer.SetPosition(1, detectorTransform.position); // Detector position
    }

    private IEnumerator AnimateLineAppearance()
    {
        lineRenderer.enabled = true;

        float elapsedTime = 0f;
        while (elapsedTime < lineAppearDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lineAppearDuration);

            // Gradually make the line appear
            Vector3 currentEndPosition = Vector3.Lerp(transform.position, detectorTransform.position, t);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentEndPosition);

            yield return null;
        }

        // After the animation, make sure the line reaches the full distance
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, detectorTransform.position);
    }
}
