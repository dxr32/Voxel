using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int[] amount;
    public int[] maxAmount;
    public int selected;

    public bool[] isFull;

    public GameObject[] slots;
    public GameObject[] inSlot;

    public Transform changer;

    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selected >= slots.Length - 1)
                selected = 0;
            else
                selected++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selected <= 0)
                selected = slots.Length - 1;
            else
                selected--;
        }

        changer.position = new Vector3(slots[selected].transform.position.x, slots[selected].transform.position.y -45f, slots[selected].transform.position.z);

    }
}
