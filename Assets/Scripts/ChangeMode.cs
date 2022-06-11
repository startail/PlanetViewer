using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : MonoBehaviour
{
    [SerializeField] private SampleRotate.Mode mode;
    [SerializeField] private SampleRotate planet;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener( OnButtonClicked );
    }

    public void OnButtonClicked()
    {
        if( planet != null ) planet.mode = mode;
    }
}
