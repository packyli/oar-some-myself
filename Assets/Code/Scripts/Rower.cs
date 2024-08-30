using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

/// <summary>
/// Interface to the rowing machine API.
/// </summary>
public class Rower : MonoBehaviour
{
    public struct RowData
    {
        public uint Pace;
        public uint Power;
        public uint Horizontal;
    }

    //[DllImport("C2API", EntryPoint = "InitRower")]
    //public static extern int InitRower();      // Returns: -1: Init failed, -2: No connected rower detected, >0: number of initialised rowers

    //[DllImport("C2API", EntryPoint = "GetRowData")]
    //public static extern void GetRowData(ref RowData rowData);

    //[DllImport("C2API", EntryPoint = "CloseRower")]
    //public static extern int CloseRower();

    public uint RowPace { get; private set; }
    public uint RowPower { get; private set; }
    public uint RowDistance { get; private set; }

    public bool DEBUG;

    private int numRowers;


    public InputField InputField_power;
  

    // Use this for initialization
    void Start()
    {
        RowPace = 0;
        RowPower = 0;
        RowDistance = 0;
        //numRowers = InitRower();
        switch (numRowers)
        {
            case -1:
                Debug.LogWarning("Rower init failed. Switching to manual mode.");
                DEBUG = true;
                break;
            case -2:
            case 0:
                Debug.LogWarning("No connected rower. Switching to manual mode.");
                DEBUG = true;
                break;
            default:
                Debug.Log("Number of rowers: " + numRowers);
                break;
        }
        if (numRowers > 0 && !DEBUG)
        {
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    RowData rowData = new RowData();
                    //GetRowData(ref rowData);
                    RowPace = rowData.Pace;
                    RowPower = rowData.Power;
                    RowDistance = rowData.Horizontal;
                    Thread.Sleep(100);
                }
            })).Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // RowPace = (uint)Random.Range(30, 40);
                RowPace = 35;
                // RowPower = (uint)Random.Range(50, 70);
                if (RowPower <= uint.Parse(InputField_power.text))
                {
                    //Debug.Log(11);
                    RowPower = uint.Parse(InputField_power.text) + 1;
                    //Debug.Log("RowPower: " + RowPower);
                }
                else
                {
                    RowPower = uint.Parse(InputField_power.text) - 1;
                    //Debug.Log(121);
                    //Debug.Log("RowPower: " + RowPower);
                }

                //   RowPower = uint.Parse(InputField_power.text)+ (uint)new System.Random().Next(1,2);
                RowDistance += (uint)Random.Range(2, 4);
                //InputField_power.text = RowPower.ToString();
                //InputField_pace.text = RowPace.ToString();
               // Text_dis.text = RowDistance.ToString();
            }
            return;
        }
    }

    void OnApplicationQuit()
    {
        if (numRowers > 0)
        {
            //CloseRower();
        }
    }
}
