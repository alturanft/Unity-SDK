using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownScript : MonoBehaviour
{
    public GameObject getuserdata;
    public GameObject getusersitems;
    public GameObject getmanyusers;
    public GameObject getitemdata;
    public GameObject getmanyitems;
    public GameObject getcollection;
    public GameObject getmanycollections;
    public GameObject getitemholder;
    public GameObject getitemhistory;
    public GameObject getuserbalance;
    public GameObject getusererc20balance;
    public GameObject getitembalance;
    public Dropdown dropDown;
    public void openCanvas()
    {
        if(dropDown.value == 0)
        {
            getuserdata.SetActive(true);

            getusersitems.SetActive(false);
            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 1)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(true);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 2)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);
            getmanyusers.SetActive(true);

            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 3)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);
            getmanyusers.SetActive(false);
            getitemdata.SetActive(true);

            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 4)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);
            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(true);

            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 5)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(true);

            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 6)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(true);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 7)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(true);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 8)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(true);

            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 9)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(true);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 10)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(true);
            getitembalance.SetActive(false);
        }
        else if (dropDown.value == 11)
        {
            getuserdata.SetActive(false);
            getusersitems.SetActive(false);

            getmanyusers.SetActive(false);
            getitemdata.SetActive(false);
            getmanyitems.SetActive(false);
            getcollection.SetActive(false);
            getmanycollections.SetActive(false);
            getitemholder.SetActive(false);
            getitemhistory.SetActive(false);
            getuserbalance.SetActive(false);
            getusererc20balance.SetActive(false);
            getitembalance.SetActive(true);
        }
    }
}
