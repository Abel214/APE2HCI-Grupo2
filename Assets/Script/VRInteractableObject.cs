using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRBallController : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    [Header("Configuración")]
    public bool startKinematic = true; // Empezar congelado
    public Vector3 initialPosition;
    public Quaternion initialRotation;

    void Start()
    {
        // Guardar posición inicial
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Configurar Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Empezar congelado si está configurado
        if (startKinematic)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        else
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        // Configurar física
        rb.mass = 1f;
        rb.drag = 0.5f;
        rb.angularDrag = 0.5f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Detener velocidades
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Configurar XR Grab Interactable
        SetupGrabInteractable();
    }

    void SetupGrabInteractable()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        }

        // Configurar eventos
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Configuración de agarre
        grabInteractable.throwOnDetach = true;
        grabInteractable.throwSmoothingDuration = 0.25f;
        grabInteractable.throwVelocityScale = 1.5f;
        grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Activar física cuando se agarra
        if (rb != null && startKinematic)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            Debug.Log("Balón agarrado - Física activada");
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Balón soltado");
        // La física ya está activa, el balón caerá naturalmente
    }

    // Método para resetear el balón a su posición inicial
    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (startKinematic)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}