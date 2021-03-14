using UnityEngine; using UnityEngine.UI; using UnityEditor; using static UnityEngine.GUILayout;

public class GitMessageInput : EditorWindow
{
    const int width = 320, height = 64;
    string message = "";
    static GitMessageInput instance = null;

    public static void ShowDialog(){
        if (instance == null){
            instance
                = ScriptableObject.CreateInstance<GitMessageInput>();
        }
        #if UNITY_2019_1_OR_NEWER
            var x = Screen.currentResolution.width / 4 - width/2;
            var y = Screen.currentResolution.height / 4 - height/2;
        #else
            // Just give up centering this popup.
            var x = 320 - width/2;
            var y = 200 - height/2;
        #endif
        //ebug.Log($"Show window at {x}, {y}");
        instance.position
            = new Rect(x, y, width, height);
        instance.ShowPopup();
    }

    void OnGUI(){

        Label("Enter commit message:");
        BeginHorizontal();
        Space(16);
        message = TextField(message);
        string diagnostic = null;
        if (ValidateMessage(out diagnostic) && Button("OK", Width(32)))
        {
            GitTools.GitHelper.CommitWithMessage(message);
            this.Close();
            instance = null;
        }
        if (Button("Cancel", Width(64))){
            this.Close();
            instance = null;
        }
        Space(8);
        EndHorizontal();
        if (diagnostic != null){
            BeginHorizontal();
            Space(16);
            Label(diagnostic);
            EndHorizontal();
        }
    }

    bool ValidateMessage(out string diagnostic){
        if (message == null || message.Length == 0){
            diagnostic = "(cannot be empty)"; return false;
        }
        var parts = message.Split();
        if (parts.Length < 2 || parts[1].Length < 1){
            diagnostic = "(must include at least two words"; return false;
        }
        diagnostic = null;
        return true;
    }

}
