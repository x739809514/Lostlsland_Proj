
    using System;
    using UnityEngine;

    public class Interaction : MonoBehaviour
    {
        public ItemEnums targetItem;
        public bool isDone;
        private ItemEnums curItem;
        

        public void CheckItem(ItemEnums itemName)
        {
            curItem = itemName;
            if (itemName==targetItem)
            {
                isDone = true;
                OnItemClick();
            }
            else
            {
                isDone = false;
                CheckEmpty();
            }
        }

        /// <summary>
        /// 子类实现各类点击效果
        /// </summary>
        protected virtual void OnItemClick()
        {
            //使用物品
            EventSystem.CallItemUseClick(curItem);
        }

        public virtual void CheckEmpty() { }
    }
