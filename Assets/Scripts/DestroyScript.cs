using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    BuildingSystem build;

    InventorySystem inv;

    public GameObject item;
    GameObject objToMove;

    private void Start()
    {
        objToMove = GameObject.FindGameObjectWithTag("Cursor");
        build = Camera.main.GetComponent<BuildingSystem>();
        inv = Camera.main.GetComponent<InventorySystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && build.distance < build.maxDist)
        {
            if (build.canSee)
                objToMove.SetActive(false);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    for (int i = 0; i < inv.slots.Length; i++)
                    {
                        if (!inv.isFull[i])
                        {
                            if (inv.amount[i] < inv.maxAmount[i])
                            {
                                if(inv.amount[i] == 0)
                                    inv.inSlot[i] = Instantiate(item, inv.slots[i].transform, false);
                                Destroy(gameObject);
                                if(build.canSee)
                                    objToMove.SetActive(true);
                                inv.amount[i]++;
                                break;
                            }
                        }
                        if (inv.amount == inv.maxAmount && !inv.isFull[i])
                        {
                            inv.amount[i] -= inv.maxAmount[i];
                            inv.isFull[i] = true;
                            Instantiate(item, inv.slots[i].transform, false);
                            if (build.canSee)
                                objToMove.SetActive(true);
                            Destroy(gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }
}

