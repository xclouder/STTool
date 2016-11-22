# STTool
一个用来修改Unity3D创建脚本文件时脚本模板的工具

代码源于mogoson在CSDN上开放下载的工具，链接地址：
http://download.csdn.net/detail/mogoson/9359959

为了方便继续优化，我将其挪到了github上。

# How To Use：
1、将插件clone到unity3d工程的Assets/Plugins/目录下  
2、将STTool/Template/80-c# Formal Sript-NewBehaviourScript.cs拷贝到*Unity3d安装目录*的Resources/ScriptTemplates/ 目录下  
3、在Unity3D编辑器菜单中选择Tools/STTool，打开设置窗口  
4、填写Author、Company为相应的值。点击"Save Params"  
>比如 Author:xClouder  Company:EZFun  

5、重启Unity3D  
6、在Project面板右键选择创建脚本时会多出一项“c# Formal Sript”，用这种方式创建的cs文件会自动带有文件头信息：  

```
/************************************************************
//     文件名      : EquipmentUpgradeMgr.cs
//     功能描述    : 
//     负责人      : xClouder
//     参考文档    : 无
//     创建日期    : 11/21/2016
//     Copyright   : Copyright 2016-2017 EZFun.
**************************************************************/
```


#Summary
The STTool is used to edit and update Unity3D editor’s script template.  
###Use
(1) Find the menu item “Tool/STEditor” in Unity3D’s menu bar and click it to open the STEditor;  
(2) Select the EditTarget, and edit or copy your expected script template into the STEditor’s text area;  
(3) Use the “#CreateTime#” and “#CopyrightTime#” string to mark the created time and copyright time anywhere that you want to insert into the new script;  
(4) Click the “Save” button to sava your edit content to Unity3D’s script template.  
> Unity3D editor will automatically update your mark of script’s created time and copyright time when you create a CSharp (or other) script once you have STTool configured correctly.  
If you have edited and saved Unity3D’s script template by STEditor,you can browse it by click the “Current” button.   

###Other
The files in the path “Editor/STTool/Template/” are my script templates, you can refer them to create your script templates.
Contact me
If you have any suggestions or comments,please feel free to contact me at mogoson@outlook.com.
