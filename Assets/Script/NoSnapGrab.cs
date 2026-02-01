using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class NoSnapGrab : MonoBehaviour
{
    private XRGrabInteractable grab;
    private Transform originalAttach;
    private Transform runtimeAttach;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        originalAttach = grab.attachTransform;

        // Creamos un attach temporal (hijo del objeto)
        GameObject go = new GameObject("RuntimeAttach");
        go.hideFlags = HideFlags.HideInHierarchy;
        runtimeAttach = go.transform;
        runtimeAttach.SetParent(transform, false);

        grab.selectEntered.AddListener(OnSelectEntered);
        grab.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnSelectEntered);
        grab.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Posición/rotación del punto de agarre (attach del interactor)
        Transform interactorAttach = args.interactorObject.GetAttachTransform(grab);

        // Convertimos ese punto a espacio local del objeto para que coincida al agarrar
        runtimeAttach.localPosition = transform.InverseTransformPoint(interactorAttach.position);
        runtimeAttach.localRotation = Quaternion.Inverse(transform.rotation) * interactorAttach.rotation;

        // Forzamos a usar este attach: así NO hay salto, porque ya coincide
        grab.attachTransform = runtimeAttach;

        // Recomendado: evita re-parenting raro
        grab.retainTransformParent = false;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Restaurar attach original al soltar
        grab.attachTransform = originalAttach;
    }
}