
    using System;
    using UnityEngine;

    [RequireComponent(typeof(DialogManager))]
    public class CharacterH2 : Interaction
    {
        private DialogManager dialogManager;

        private void Awake()
        {
            dialogManager = GetComponent<DialogManager>();
        }

        protected override void OnItemClick()
        {
            base.OnItemClick();
            //完成事件
            dialogManager.PopDialogFinish();
        }

        public override void CheckEmpty()
        {
            if (isDone)
            {
                dialogManager.PopDialogFinish();
            }
            else
            {
                dialogManager.PopDialogEmpty();
            }
        }
    }
