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
        CsharpBehaviour,
        JavascriptBehaviour,
        StateMachineBehaviour,
        SubStateMachineBehaviour,
        SurfaceShader,
        UnlitShader,
        ImageEffectShader,
        ComputeShader
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
            GetScriptTemplateText();
        if (GUILayout.Button("Save", GUILayout.Width(60)))
            SaveScriptTemplate();
        EditorGUILayout.EndHorizontal();

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		sTText = EditorGUILayout.TextArea(sTText, GUILayout.ExpandHeight(true));
		EditorGUILayout.EndScrollView();

        EditorGUILayout.EndVertical();
    }//OnGUI()_end

    /// <summary>
    /// Get editer's script template path.
    /// </summary>
    private string GetScriptTemplatePath()
    {
        string scriptPath = string.Empty;
        switch (editTarget)
        {
            case EditTarget.CsharpBehaviour:
                scriptPath = "81-C# Script-NewBehaviourScript.cs.txt";
                break;
            case EditTarget.JavascriptBehaviour:
                scriptPath = "82-Javascript-NewBehaviourScript.js.txt";
                break;
            case EditTarget.StateMachineBehaviour:
                scriptPath = "86-C# Script-NewStateMachineBehaviourScript.cs.txt";
                break;
            case EditTarget.SubStateMachineBehaviour:
                scriptPath = "86-C# Script-NewSubStateMachineBehaviourScript.cs.txt";
                break;
            case EditTarget.SurfaceShader:
                scriptPath = "83-Shader__Standard Surface Shader-NewSurfaceShader.shader.txt";
                break;
            case EditTarget.UnlitShader:
                scriptPath = "84-Shader__Unlit Shader-NewUnlitShader.shader.txt";
                break;
            case EditTarget.ImageEffectShader:
                scriptPath = "85-Shader__Image Effect Shader-NewImageEffectShader.shader.txt";
                break;
            case EditTarget.ComputeShader:
                scriptPath = "90-Shader__Compute Shader-NewComputeShader.compute.txt";
                break;
        }//switch_end
        return EditorApplication.applicationContentsPath +
            "/Resources/ScriptTemplates/" + scriptPath;
    }//GetS...()_end

    /// <summary>
    /// Get script template text.
    /// </summary>
    private void GetScriptTemplateText()
    {
        var scriptPath = GetScriptTemplatePath();
        if (File.Exists(scriptPath))
            sTText = File.ReadAllText(scriptPath, Encoding.Default);
        else
            sTText = string.Empty;
    }//GetS...()_end

    /// <summary>
    /// Save ScriptTemplate.
    /// </summary>
    private void SaveScriptTemplate()
    {
        File.WriteAllText(GetScriptTemplatePath(), sTText, Encoding.Default);
        bool closeEditor = EditorUtility.DisplayDialog(
            "Save Template",
            "Your edit content is already save to unity3d editor's script template!",
            "Close",
            "Continue"
            );
        if(closeEditor)
            instance.Close();
    }//SaveS...()_end
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
        //Get extension.
        assetPath = assetPath.Replace(".meta", string.Empty);
        var fileSuffix = Path.GetExtension(assetPath);
        if (fileSuffix != ".cs" && fileSuffix != ".js" && fileSuffix != ".shader" &&
		    fileSuffix != ".compute")
            return;

        //Get time.
        var nowTime = DateTime.Now;
        var createTime = nowTime.ToShortDateString();
        var copyrightTime = nowTime.Year.ToString() +
            "-" + (nowTime.Year + 1).ToString();

        //Update scripttemplate.
        var content = File.ReadAllText(assetPath);
        content = content.Replace("#CreateTime#", createTime);
        content = content.Replace("#CopyrightTime#", copyrightTime);
        File.WriteAllText(assetPath, content);

        //Refresh asset database.
        AssetDatabase.Refresh();
    }//OnW...()_end
}//Class_end