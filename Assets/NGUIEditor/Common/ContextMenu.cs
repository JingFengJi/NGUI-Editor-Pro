using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace NGUIEditorPro
{
	public static class ContextMenu 
	{
        private static List<string> mEntries = new List<string>();
		private static GenericMenu menu;

        static public void AddItem(string item, bool isChecked, GenericMenu.MenuFunction callback)
        {
            if (callback != null)
            {
                if (menu == null) 
					menu = new GenericMenu();
                int count = 0;

                for (int i = 0; i < mEntries.Count; ++i)
                {
                    string str = mEntries[i];
                    if (str == item) ++count;
                }
                mEntries.Add(item);

                if (count > 0) item += " [" + count + "]";
                menu.AddItem(new GUIContent(item), isChecked, callback);
            }
            else
				AddDisabledItem(item);
        }

        static public void AddItemWithArge(string item, bool isChecked, GenericMenu.MenuFunction2 callback, object arge)
        {
            if (callback != null)
            {
                if (menu == null) 
					menu = new GenericMenu();
                int count = 0;

                for (int i = 0; i < mEntries.Count; ++i)
                {
                    string str = mEntries[i];
                    if (str == item) ++count;
                }
                mEntries.Add(item);

                if (count > 0) item += " [" + count + "]";
                	menu.AddItem(new GUIContent(item), isChecked, callback, arge);
            }
            else
				AddDisabledItem(item);
        }

        static public void Show()
        {
            if (menu != null)
            {
                menu.ShowAsContext();
                menu = null;
                mEntries.Clear();
            }
        }

		static public void AddDepthMenu()
		{
            //AddItem("Depth/Max Depth ↑↑↑", false, null);//PriorityTool.MoveToTopWidget
            //AddItem("Depth/Mix Depth ↓↓↓", false, null);//PriorityTool.MoveToBottomWidget
            AddItem("Depth/Depth + 1 ↑", false, null);//PriorityTool.MoveUpWidget
            AddItem("Depth/Depth - 1 ↓", false, null);//PriorityTool.MoveDownWidget
		}

		static public void AddShowOrHideMenu()
		{
            bool allHide = true;
            bool allShow = true;
            foreach (var item in Selection.gameObjects)
            {
                allHide = allHide && !item.activeSelf;
                allShow = allShow && item.activeSelf;
            }
            if (allHide)
                AddItem("显示", false, TransformTool.ShowAllSelection);
            else if(allShow)
                AddItem("隐藏", false, TransformTool.HideAllSelection);
            else
            {
                AddItem("显示", false, TransformTool.ShowAllSelection);
                AddItem("隐藏", false, TransformTool.HideAllSelection);
            }
		}

        static public void AddTransformMenu()
        {
            AddItem("Tranform/坐标归零", false, TransformTool.ResetPosition);//PriorityTool.MoveToTopWidget
            AddItem("Tranform/旋转归零", false, TransformTool.ResetRotation);//PriorityTool.MoveToBottomWidget
            AddItem("Tranform/缩放归一", false, TransformTool.ResetScale);//PriorityTool.MoveUpWidget
        }

		static public void AddCommonItems(GameObject[] selections)
		{
            if(selections == null || selections.Length <= 0)
            {
                AddItem("打开界面",false,null);//UIEditorHelper.LoadPrefab
                AddItem("添加示例图",false,null);
                AddItem("移除示例图",false,null);
            }
            if(selections != null && selections.Length > 0)
            {
                if(selections.Length == 1)
                {
                    AddTransformMenu();
                    AddDepthMenu();
                    Image image = selections[0].GetComponent<Image>();
                    if(image != null)
                    {
                        AddItem("Set Native Size",false,null);
                    }
                    // UISprite sprite = selections[0].GetComponent<UISprite>();
                    // if(sprite != null)
                    // {
                    //     AddItem("Snap", false, null);
                    //     AddItem("添加组件/BoxCollider",false,null);
                    //     AddItem("添加组件/UIButtonSx",false,null);
                    // }

                    if(selections[0].gameObject.hideFlags == HideFlags.NotEditable)
                    {
                        AddItem("解锁",false,TransformTool.Unlock);
                    }
                    else
                    {
                        AddItem("锁定",false,TransformTool.Lock);
                    }
                }
                else if(selections.Length > 1)
                {
                    
                }
                AddShowOrHideMenu();
            }
		}

        static public void AddSeparator(string path)
        {
            if (menu == null)
				menu = new GenericMenu();
            if (Application.platform != RuntimePlatform.OSXEditor)
                menu.AddSeparator(path);
        }

        static public void AddDisabledItem(string item)
        {
            if (menu == null)
                menu = new GenericMenu();
            menu.AddDisabledItem(new GUIContent(item));
        }
    }
}
