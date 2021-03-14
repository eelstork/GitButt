using System.IO;
using UnityEditor; using UnityEngine;
using RemoveOpt = UnityEditor.RemoveAssetOptions;
using static UnityEditor.AssetMoveResult;
using static UnityEditor.AssetDeleteResult;
using ADB = UnityEditor.AssetDatabase;

namespace GitTools{
public class ModificationProcessor : UnityEditor.AssetModificationProcessor{

    public static bool warnings = true;

    static string[] OnWillSaveAssets(string[] paths){
        GitHelper.MarkDirty();
        //Debug.Log("OnWillSaveAssets");
        //foreach (string path in paths)
        //    Debug.Log(path);
        return paths;
    }

}}
