using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float interactDistance = 100.0f;

    Ray rayOrigin;
    RaycastHit hitInfo;

    private bool canInteract = true;

    private TowerScript towerRef;

    enum InteractableTargets
    {
        PAWN
    }

    void Start()
    {

    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
    }

    private void CheckInput()
    {
        if(Input.GetMouseButtonUp(0))
        {
            canInteract = true;
        }

        if(Input.GetMouseButton(0)) // LEFT CLICK
        {
            CheckInteraction();
        }


    }

    private void CheckInteraction()
    {

        if (canInteract)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, interactDistance))
            {
                //Debug.Log(hitInfo.transform.tag);
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green, 1.0f);

                if (hitInfo.transform.tag == "Pawn")
                {
                    SelectTargetOnInteraction(InteractableTargets.PAWN, hitInfo);
                   // pawnRef = hitInfo.transform.parent.gameObject;
                    //Debug.Log(pawnRef.name);
                   //pawnRef.GetComponent<PawnScript>().SelectPawn(true);
                }

                //Default
                else
                {
                    Deselect();
                }
            }

            //No Hit
            else
            {
                Deselect();
            }
        }


        canInteract = false;
    }

    private void SelectTargetOnInteraction(InteractableTargets targetType, RaycastHit target)
    {
        //Already selected a tower
        if(towerRef != null)
        {
            towerRef.SelectTower(false);

            //Get our new reference to selected tower
            towerRef = target.transform.parent.gameObject.GetComponent<TowerScript>();

            towerRef.SelectTower(true);
        }

        else
        {
            towerRef = target.transform.parent.gameObject.GetComponent<TowerScript>();

            towerRef.SelectTower(true);
        }

    }

    private void Deselect()
    {
        if(towerRef != null)
        {
            towerRef.SelectTower(false);
            towerRef = null;

        }
    }
}
