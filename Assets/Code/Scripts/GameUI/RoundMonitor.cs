using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class RoundMonitor : MonoBehaviour
{
    public int dropdownValue2;
    public int dropdownValue3;
    public int dropdownValue4;
    public int dropdownValue5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject dropdownObject2 = GameObject.FindGameObjectWithTag("Round2Config");
        GameObject dropdownObject3 = GameObject.FindGameObjectWithTag("Round3Config");
        GameObject dropdownObject4 = GameObject.FindGameObjectWithTag("Round4Config");
        GameObject dropdownObject5 = GameObject.FindGameObjectWithTag("Round5Config");

        if (dropdownObject2 != null && dropdownObject3 != null && dropdownObject4 != null && dropdownObject5 != null)
        {
            Debug.Log("dropdownValue2 is working £¿£¿£¿£¿£¿£¿£¿£¿£¿£¿£¿£¿£¿" + dropdownValue2);
            Dropdown dropdown2 = dropdownObject2.GetComponent<Dropdown>();
            Dropdown dropdown3 = dropdownObject3.GetComponent<Dropdown>();
            Dropdown dropdown4 = dropdownObject4.GetComponent<Dropdown>();
            Dropdown dropdown5 = dropdownObject5.GetComponent<Dropdown>();


            if (dropdown2 != null && dropdown3 != null && dropdown4 != null && dropdown5 != null)
            {

                dropdownValue2 = dropdown2.value;
                dropdownValue3 = dropdown3.value;
                dropdownValue4 = dropdown4.value;
                dropdownValue5 = dropdown5.value;

                Debug.Log("dropdownValue2 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue2);
                Debug.Log("dropdownValue3 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue3);
                Debug.Log("dropdownValue4 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue4);
                Debug.Log("dropdownValue5 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue5);

            }
        }
    }
}
 
