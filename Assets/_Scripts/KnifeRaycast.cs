using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KnifeRaycast : MonoBehaviour
{
    //public GameObject ControllerRight;
    //public GameObject ControllerLeft;

    private bool hasHit;
    //private XRDirectInteractor interactorLeft = null;
    //private XRDirectInteractor interactorRight = null;
    private XRBaseInteractor interactor;
    private bool isHeld;
    private Rigidbody rb;
    private void Start()
    {
        var theKnife = transform.parent;
        //interactorLeft = ControllerLeft.GetComponent<XRDirectInteractor>();
        //interactorRight = ControllerRight.GetComponent<XRDirectInteractor>();
        interactor = GetComponent<XRBaseInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnSelectExited);
        rb = theKnife.GetComponent<Rigidbody>();

    }

    private void OnDestroy()
    {
        interactor.selectEntered.RemoveListener(OnSelectEntered);
        interactor.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    private void Update()
    {
        if (!hasHit && !isHeld) ShootRaycast();

        if (hasHit && !isHeld)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (hasHit && isHeld)
        {
            hasHit = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }

    }

    private void ShootRaycast()
    {
        hasHit = Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 20f);
    }
}
