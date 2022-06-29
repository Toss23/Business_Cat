using UnityEngine;

public class LocalDataEditor : MonoBehaviour
{
    [Header("Edit Bool")]
    [SerializeField] private string keyBool;
    [SerializeField] private bool valueBool;
    [SerializeField] private bool setBool;
    [SerializeField] private bool getBool;

    private void Update()
    {
        if (setBool)
        {
            setBool = false;
            LocalData.SetBool(keyBool, valueBool);
            Debug.Log("[LDE] Bool [" + keyBool + "] write [" + valueBool + "]");
        }

        if (getBool)
        {
            getBool = false;
            Debug.Log("[LDE] Bool [" + keyBool + "] equal [" + LocalData.GetBool(keyBool) + "]");
        }
    }
}