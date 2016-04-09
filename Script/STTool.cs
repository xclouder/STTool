/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: STTool.cs
 *  Author: Mogoson   Version: 1.0   Date: 11/7/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve it's function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         STEditor                 Ignore.
 *     2.         STUpdater                Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     11/7/2015       1.0        Build this file.
 *************************************************************************/

using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Edit ScriptTemplate.
/// </summary>
public class STEditor : EditorWindow
{
    #region Enum
    //Private enum.
    private enum EditTarget
    {
        CsharpScriptTemplate,
        JavaScriptTemplate
    }//enum_end
    #endregion

    #region Field
    private static STEditor instance;
    private static EditTarget editTarget;
    private static string sTText;
    private static Vector2 scrollPos;
    #endregion

    #region Method
    //Show the window in unity editor menu.
    [MenuItem("Tool/STEditor")]
    private static void ShowSTEditor()
    {
        instance = EditorWindow.GetWindow<STEditor>();
        instance.Show();
    }//ShowS...()_end
    
    //Draw the window.
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        editTarget = (EditTarget)EditorGUILayout.EnumPopup("EditTarget", editTarget);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("ScriptTemplate");
        EditorGUILayout.Space();
        if (GUILayout.Button("Current", GUILayout.Width(60)))
            sTText = EditorPrefs.GetString(editTarget.ToString());
        if (GUILayout.Button("Save", GUILayout.Width(60)))
            ReplaceScriptTemplate();
        EditorGUILayout.EndHorizontal();

        using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, GUILayout.ExpandHeight(true)))
        {
            sTText = EditorGUILayout.TextArea(sTText, GUILayout.ExpandHeight(true));
            scrollPos = scrollView.scrollPosition;
        }
        EditorGUILayout.EndVertical();
    }//OnGUI()_end

    /// <summary>
    /// Replace ScriptTemplate.
    /// </summary>
    private void ReplaceScriptTemplate()
    {
        EditorPrefs.SetString(editTarget.ToString(), sTText);
        string editorPath = string.Empty;
        switch (editTarget)
        {
            case EditTarget.CsharpScriptTemplate:
                editorPath = "81-C# Script-NewBehaviourScript.cs.txt";
                break;
            case EditTarget.JavaScriptTemplate:
                editorPath = "82-Javascript-NewBehaviourScript.js.txt";
                break;
        }//switch_end
        editorPath = EditorApplication.applicationContentsPath +
            "/Resources/ScriptTemplates/" + editorPath;
        File.WriteAllText(editorPath, sTText, Encoding.Default);
        bool closeEditor = EditorUtility.DisplayDialog(
            "Save Template",
            "Your edit content is already save to unity3d editor's script template!",
            "Close",
            "Continue"
            );
        if(closeEditor)
            instance.Close();
    }//ReplaceS...()_end
    #endregion
}//Class_end

/// <summary>
/// Update ScriptTemplate.
/// </summary>
public class STUpdater : UnityEditor.AssetModificationProcessor
{
    /// <summary>
    /// This is called by Unity when it is about to create an asset
    /// not imported by the user, eg. ".meta" files.
    /// </summary>
    private static void OnWillCreateAsset(string assetPath)
    {
        assetPath = assetPath.Replace(".meta", string.Empty);
        var fileSuffix = Path.GetExtension(assetPath);
        if (fileSuffix != ".cs" && fileSuffix != ".js")
            return;

        //Get time.
        var nowTime = DateTime.Now;
        var createTime = nowTime.ToShortDateString();
        var copyrightTime = nowTime.Year.ToString() +
            "-" + (nowTime.Year + 1).ToString();

        //Update ScriptTemplate.
        var content = File.ReadAllText(assetPath);
        content = content.Replace("#CreateTime#", createTime);
        content = content.Replace("#CopyrightTime#", copyrightTime);
        File.WriteAllText(assetPath, content);

        //Refresh AssetDatabase.
        AssetDatabase.Refresh();
    }//OnW...()_end
}//Class_end