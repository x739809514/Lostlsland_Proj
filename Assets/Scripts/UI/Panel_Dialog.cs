using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Dialog : MonoBehaviour
{
    private Transform handle;
    private Text dialogTxt;
    
    private void OnEnable()
    {
        EventSystem.dialogPop += OnPrintDialog;
    }

    private void OnDisable()
    {
        EventSystem.dialogPop -= OnPrintDialog;
    }

    private void Awake()
    {
        handle = transform.Find("handle");
        dialogTxt = transform.Find("handle/txt").GetComponent<Text>();
    }

    private void OnPrintDialog(string dialog)
    {
        handle.gameObject.SetActive(dialog!=String.Empty);
        dialogTxt.text = dialog;
    }
}
