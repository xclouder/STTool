# STTool
一个用来修改Unity3D创建脚本文件时脚本模板的工具

代码源于mogoson在CSDN上开放下载的工具，链接地址：
http://download.csdn.net/detail/mogoson/9359959

为了方便继续优化、代码重用，我将其挪到了github上。

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
