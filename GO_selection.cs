using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GO_selection : EditorWindow
{
    [MenuItem("Tools/Hierarchy Navigator")]
    public static void ShowWindow()
    {
        GetWindow<GO_selection>("Hierarchy Navigator");
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChange;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChange;
    }

    private void Update()
    {
        if (Event.current != null && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.control && Event.current.alt)
            {
                if (Event.current.keyCode == KeyCode.RightArrow)
                {
                    ExpandAndSelectFirstChild();
                    Event.current.Use();
                }
                else if (Event.current.keyCode == KeyCode.LeftArrow)
                {
                    SelectParentsOfSelected();
                    Event.current.Use();
                }
                else if (Event.current.keyCode == KeyCode.DownArrow)
                {
                    SelectNextSibling();
                    Event.current.Use();
                }
                else if (Event.current.keyCode == KeyCode.UpArrow)
                {
                    SelectPreviousSibling();
                    Event.current.Use();
                }
            }
        }
    }

    private void OnSelectionChange()
    {
        Repaint();
    }

    [MenuItem("EditorUtil/Selection/Child %&'")]

    private static  void ExpandAndSelectFirstChild()
    {
        List<GameObject> newSelection = new List<GameObject>();
        foreach (GameObject go in Selection.gameObjects)
        {
            if (go.transform.childCount > 0)
            {
                // Ping the parent object in the hierarchy
                // EditorGUIUtility.PingObject(go);
                // EditorUtility.isex
                // Select the first child
                newSelection.Add(go.transform.GetChild(0).gameObject);
            }
        }
        Selection.objects = newSelection.ToArray();
    }

    [MenuItem("EditorUtil/Selection/Parent %&;")]
     private static  void SelectParentsOfSelected()
    {
        HashSet<GameObject> newSelection = new HashSet<GameObject>();
        foreach (GameObject go in Selection.gameObjects)
        {
            if (go.transform.parent != null)
            {
                newSelection.Add(go.transform.parent.gameObject);
            }
        }
        Selection.objects = new GameObject[newSelection.Count];
        newSelection.CopyTo(Selection.gameObjects);
    }

    [MenuItem("EditorUtil/Selection/Next Sibling %&]")]
     private static  void SelectNextSibling()
    {
        List<GameObject> newSelection = new List<GameObject>();
        foreach (GameObject go in Selection.gameObjects)
        {
            Transform parent = go.transform.parent;
            if (parent != null)
            {
                int siblingIndex = go.transform.GetSiblingIndex();
                if (siblingIndex < parent.childCount - 1)
                {
                    newSelection.Add(parent.GetChild(siblingIndex + 1).gameObject);
                }
            }
        }
        Selection.objects = newSelection.ToArray();
    }

    [MenuItem("EditorUtil/Selection/Previous Sibling %&[")]
     private static  void SelectPreviousSibling()
    {
        List<GameObject> newSelection = new List<GameObject>();
        foreach (GameObject go in Selection.gameObjects)
        {
            Transform parent = go.transform.parent;
            if (parent != null)
            {
                int siblingIndex = go.transform.GetSiblingIndex();
                if (siblingIndex > 0)
                {
                    newSelection.Add(parent.GetChild(siblingIndex - 1).gameObject);
                }
            }
        }
        Selection.objects = newSelection.ToArray();
    }
}


// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;

// [CanEditMultipleObjects()]
// public class GO_selection : EditorWindow
// {
//     [MenuItem("Tools/Hierarchy Navigator ")]
//     public static void ShowWindow()
//     {
//         GetWindow<GO_selection>("Hierarchy Navigator");
//     }

//     // private void OnEnable()
//     // {
//     //     Selection.selectionChanged += OnSelectionChange;
//     // }

//     // private void OnDisable()
//     // {
//     //     Selection.selectionChanged -= OnSelectionChange;
//     // }

//     // private void OnSelectionChange()
//     // {
//     //     Repaint();
//     // }

//     // private void Update()
//     // {
//     //     if (Event.current != null && Event.current.type == EventType.KeyDown)
//     //     {
//     //         Debug.Log("1");
//     //         if (Event.current.control && Event.current.alt)
//     //         {
//     //         Debug.Log("2");
//     //             if (Event.current.keyCode == KeyCode.LeftArrow)
//     //             {
//     //         Debug.Log("3");
//     //                 ExpandAndSelectFirstChild();
//     //                 Event.current.Use();
//     //             }
//     //             else if (Event.current.keyCode == KeyCode.RightArrow)
//     //             {
//     //         Debug.Log("4");
//     //                 SelectParentsOfSelected();
//     //                 Event.current.Use();
//     //             }
//     //             else if (Event.current.keyCode == KeyCode.DownArrow)
//     //             {
//     //         Debug.Log("5");
//     //                 SelectNextSibling();
//     //                 Event.current.Use();
//     //             }
//     //             else if (Event.current.keyCode == KeyCode.UpArrow)
//     //             {
//     //         Debug.Log("6");
//     //                 SelectPreviousSibling();
//     //                 Event.current.Use();
//     //             }
//     //         }
//     //     }
//     // }

//     [MenuItem("Tools/SelectChilds %&RIGHT ")]

//     private static void ExpandAndSelectFirstChild()
//     {
//         Debug.Log("ctr alt right");
//         List<GameObject> newSelection = new List<GameObject>();
//         foreach (GameObject go in Selection.gameObjects)
//         {
//             if (go.transform.childCount > 0)
//             {
//                 // Expand the parent
//                 EditorGUIUtility.PingObject(go);
//                 // Select the first child
//                 newSelection.Add(go.transform.GetChild(0).gameObject);
//             }
//         }
//         Selection.objects = newSelection.ToArray();
//     }

//     private void SelectParentsOfSelected()
//     {
//         HashSet<GameObject> newSelection = new HashSet<GameObject>();
//         foreach (GameObject go in Selection.gameObjects)
//         {
//             if (go.transform.parent != null)
//             {
//                 newSelection.Add(go.transform.parent.gameObject);
//             }
//         }
//         Selection.objects = new GameObject[newSelection.Count];
//         newSelection.CopyTo(Selection.gameObjects );
//     }

//     private void SelectNextSibling()
//     {
//         List<GameObject> newSelection = new List<GameObject>();
//         foreach (GameObject go in Selection.gameObjects)
//         {
//             Transform sibling = go.transform.GetSiblingIndex() < go.transform.parent.childCount - 1 ?
//                 go.transform.parent.GetChild(go.transform.GetSiblingIndex() + 1) : null;

//             if (sibling != null)
//             {
//                 newSelection.Add(sibling.gameObject);
//             }
//         }
//         Selection.objects = newSelection.ToArray();
//     }

//     private void SelectPreviousSibling()
//     {
//         List<GameObject> newSelection = new List<GameObject>();
//         foreach (GameObject go in Selection.gameObjects)
//         {
//             Transform sibling = go.transform.GetSiblingIndex() > 0 ?
//                 go.transform.parent.GetChild(go.transform.GetSiblingIndex() - 1) : null;

//             if (sibling != null)
//             {
//                 newSelection.Add(sibling.gameObject);
//             }
//         }
//         Selection.objects = newSelection.ToArray();
//     }
// }
