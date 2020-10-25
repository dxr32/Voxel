using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public Transform canPlaceCheck;

    public InventorySystem inv;

    public GameObject objToMove;
    public GameObject objToPlace;

    public LayerMask mask;

    float lastPosX, lastPosY, lastPosZ;
    float nextMove;
    public float distance;
    public float maxDist;

    public bool activeGizmo;
    public bool canSee;

    Vector3 mousePos;

    private void Start()
    {
        objToMove = GameObject.Instantiate(objToMove, canPlaceCheck.position, Quaternion.identity);
        canSee = true;
    }

    void Update()
    {
        for(int i = 0; i < inv.slots.Length; i++)
        {
            if (inv.amount[i] <= 0)
            {
                inv.isFull[i] = false;
                Destroy(inv.inSlot[i]);
            }
            else if (inv.amount[i] >= inv.maxAmount[i])
                inv.isFull[i] = true;
        }

        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        distance = Vector3.Distance(canPlaceCheck.position, transform.position);
        
        if (inv.amount[inv.selected] == 0)
        {
            canSee = false;
            objToMove.SetActive(false);
        }
        else if (inv.amount[inv.selected] > 0)
        {
            canSee = true;
            objToMove.SetActive(true);
        }

        if (inv.amount[inv.selected] > 0)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    float posX = Mathf.Round(hit.point.x);
                    float posY = Mathf.Round(hit.point.y);
                    float posZ = Mathf.Round(hit.point.z);

                    if (posX != lastPosX || posY != lastPosY || posZ != lastPosZ)
                    {
                        lastPosX = posX;
                        lastPosY = posY;
                        lastPosZ = posZ;
                        canPlaceCheck.position = new Vector3(posX, posY + .5f, posZ);

                        nextMove = Time.time + 0.001f;
                    }

                    if (distance < maxDist && nextMove < Time.time)
                        objToMove.transform.position = new Vector3(posX, posY + .5f, posZ);
                    if (Input.GetMouseButtonDown(0) && distance < maxDist)
                    {
                        objToMove.gameObject.SetActive(true);
                        Instantiate(objToPlace, objToMove.transform.position, Quaternion.identity);
                        inv.amount[inv.selected]--;
                    }
                }
            }   
    }

    private void OnDrawGizmos()
    {
        if (activeGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, new Vector3(maxDist * 2, maxDist * 2, maxDist * 2));
        }
    }
}
