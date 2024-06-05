using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager: MonoBehaviour
{
    private Stack<string> dialogEmpty=new Stack<string>();
    private Stack<string> dialogFinish=new Stack<string>();
    private bool isTalking;

    public DialogData dialogData;

    private void Awake()
    {
        FillDialogStack();
    }

    private void FillDialogStack()
    {
        for (int i = dialogData.CharacterH2Empty.Count-1; i > -1; i--)
        {
            dialogEmpty.Push(dialogData.CharacterH2Empty[i]);
        }

        for (int i = dialogData.CharacterH2Finish.Count-1; i >-1; i--)
        {
            dialogFinish.Push(dialogData.CharacterH2Finish[i]);
        }
    }

    public void PopDialogEmpty()
    {
        if (isTalking==false)
        {
            StartCoroutine(DoPrintDialog(dialogEmpty));
        }
    }

    public void PopDialogFinish()
    {
        if (isTalking==false)
        {
            StartCoroutine(DoPrintDialog(dialogFinish));
        }
    }

    private IEnumerator DoPrintDialog(Stack<string> dialogs)
    {
        isTalking = true;
        if (dialogs.TryPop(out string arg))
        {
            EventSystem.CallDialogPopEvent(arg);
            EventSystem.CallGameStateEvent(GameState.Pause);
            yield return null;
            isTalking = false;
        }
        else
        {
            EventSystem.CallDialogPopEvent(String.Empty);
            EventSystem.CallGameStateEvent(GameState.GamePlay);
            FillDialogStack();
            isTalking = false;
        }
    }
}
