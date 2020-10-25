using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountShowerScript : MonoBehaviour
{
    public Text[] amount;

    public InventorySystem inv;

    public string[] amountS;

    void Update()
    {
        for (int i = 0; i < inv.amount.Length; i++)
        {
            amountS[i] = "" + inv.amount[i];
            amount[i].text = amountS[i];
        }
    }
}
