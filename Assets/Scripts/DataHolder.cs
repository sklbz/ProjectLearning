using UnityEngine;

public class DataHolder : MonoBehaviour {

    [SerializeField]
    string dataName;

    [SerializeField]
    string[] dataList1, dataList2;

    public string[] Data1 => dataList1;
    public string[] Data2 => dataList2;
}
