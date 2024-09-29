using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder {

    [SerializeField]
    string name;

    [SerializeField]
    string[] dataList1, dataList2;

    public string[] Data1 => dataList1;
    public string[] Data2 => dataList2;
}
