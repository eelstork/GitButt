using System.Linq;
using UnityEngine;
﻿using UnityEditor;
using UnityToolbarExtender;
//using Active.Howl;

namespace GitTools{
[InitializeOnLoad] public class GitHelper{

	const  bool allowCodeChanges = false;
	const  float  Delay = 120f;
	static float  stamp = -100f;
	static string status = null;

	static GitHelper()
	=> ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);

	static void ProcessChanges() {
		string @out = null;
		if(NeedsUpdate()){
			@out = GitRunner.Cmd("pull");
			Debug.Log(@out);
			stamp = 0;
		}else if(HasCodeChanges() && !allowCodeChanges){
			Debug.LogWarning(
				$"Committing code changes is not allowed\n{status}");
		}else if(HasChanges()){
			GitMessageInput.ShowDialog();
		}else{
			Debug.Log("Git repository up to date; no local changes.");
		}
	}

	public static void CommitWithMessage(string msg){
		string @out = null;
		@out += GitRunner.Cmd("add --all");
		@out += GitRunner.Cmd($"commit -m \"{msg}\"");
		// Git push seems to be lacking good sense in deciding what
		// goes to stderr/stdout; silencing until we figure what
		// should be reported as an error.
		@out += GitRunner.Cmd("push -q");
		Debug.Log(@out);
		stamp = 0;
	}

	public static void MarkDirty(){
		stamp = 0f;
	}

	static void OnToolbarGUI(){
		//GUILayout.FlexibleSpace();
		string label = GetLabelText();
		//if(HasChanges(forceCheck: false)) label = "↑";

		if(GUILayout.Button(
			new GUIContent(label, StatusString()),
			GetStyle()))
		{
			ProcessChanges();
		}
	}

	// TODO this
	static bool HasConflicts(bool forceCheck = true) => false;

	static bool NeedsUpdate(bool forceCheck = true)
	=> GetStatus(forceCheck).Contains("Your branch is behind");

	// Such as `Changes not staged for commit` or
	//         `Changes to be committed`
	static bool HasChanges(bool forceCheck = true){
		var s = GetStatus(forceCheck);
		return s.Contains("Changes")
			|| s.Contains("untracked files present");
	}

	static bool HasCodeChanges(bool forceCheck = true)
	=> HasChanges(forceCheck)
	&& GetStatus(forceCheck).Contains(".cs");

	static string GetStatus(bool forceCheck){
		var t = Time.realtimeSinceStartup;
		var δ = t - stamp;
		if(δ > Delay || forceCheck || status == null){
			//ebug.Log("Get status");
			status = GitRunner.Cmd("status");
			stamp = t;
		}
		return status;
	}

	static string StatusString(){
		if(status==null) return
			"repository status unknown";
		if(HasCodeChanges(forceCheck: false)) return
			"Use your own git front-end to commit code changes";
		if(HasChanges(forceCheck: false)) return
			"Commit changes";
		if(!status.Contains("\n")) return status;
		var lines = status.Split('\n');
		if(lines.Length == 1) return lines[0];
		return $"{lines[0]}. {lines[1]}";
	}

	static GUIStyle GetStyle(){
		     if(HasConflicts  (forceCheck: false)) return Styles.alert;
		else if(NeedsUpdate   (forceCheck: false)) return Styles.alert;
		else if(HasCodeChanges(forceCheck: false)) return Styles.normal;
		else if(HasChanges    (forceCheck: false)) return Styles.normal;
		else return Styles.normal;
	}

	static string GetLabelText(){
		     if(HasConflicts  (forceCheck: false)) return "!";
		else if(NeedsUpdate   (forceCheck: false)) return "⇣";
		else if(HasCodeChanges(forceCheck: false)) return "ℂ";
		else if(HasChanges    (forceCheck: false)) return "↑";
		else return "☁️";
	}

}

// ==================================================================

static class Styles{

	public static readonly GUIStyle normal;
	public static readonly GUIStyle alert;
	static Texture2D redTex;

	static Styles(){
		normal = MakeStyle(Color.black);
		alert = MakeStyle(Color.red);
	}

	static GUIStyle MakeStyle(Color col){
		var style = new GUIStyle("Command"){
			fontSize = 14,
			alignment = TextAnchor.MiddleCenter,
			imagePosition = ImagePosition.ImageAbove,
			fontStyle = FontStyle.Bold
		};
		style.normal.textColor = col;
		return style;
	}


	static Texture2D MakeTexture(){
		Color color = Color.red;
		var tx = new Texture2D(16, 16);
		Color[] pixels = Enumerable.Repeat(color, 16*16).ToArray();
		tx.SetPixels(pixels);
		tx.Apply();
		return tx;
	}

}

} // namespace GitTools
