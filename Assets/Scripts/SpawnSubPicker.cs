using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSubPicker : MonoBehaviour
{
    [SerializeField] public GameObject subPickerBase;
    [SerializeField] public GameObject subPickerPool;
    [SerializeField] private GameObject planet;
    [SerializeField] public Transform mainPicker;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener( OnButtonClicked );
    }

    public void OnButtonClicked()
    {
        if( planet == null ) return ;
        if( mainPicker == null ) return;
        if( subPickerBase == null ) return;
        if( subPickerPool == null ) return;

        GameObject subPicker = Instantiate(subPickerBase,mainPicker.position,new Quaternion(0,0,0,0));
        subPicker.transform.parent = subPickerPool.transform;
        subPicker.GetComponent<SubPicker>().planet = planet;
    }
}
