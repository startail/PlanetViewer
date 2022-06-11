using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetRotation : MonoBehaviour
{
    [SerializeField] private SampleRotate planet;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener( OnButtonClicked );
    }

    public void OnButtonClicked()
    {
        if( planet != null ) planet.transform.rotation = new Quaternion(0,0,0,0);
    }
}
