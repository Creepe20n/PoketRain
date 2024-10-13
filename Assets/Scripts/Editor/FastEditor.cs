using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
namespace FastEditor {
    public class F_GUI {
        public static bool DisplayButton(string DisplayText,float width = 110,float height = 30) {
            if(GUILayout.Button(DisplayText,GUILayout.Width(width),GUILayout.Height(height))) {
                return true;
            }
            return false;
        }
        public static string ExplorerField(string loadedPath = "",string textFieldName = "path") {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            string pathText = loadedPath;
            EditorGUIUtility.labelWidth = textFieldName.Length * 6.4f;
            pathText = EditorGUILayout.TextField(textFieldName,pathText,GUILayout.Width(230));
            EditorGUIUtility.labelWidth = 0;
            if(DisplayButton("Open Explorer",height: 17,width: 90)) {
                pathText = EditorUtility.OpenFolderPanel("Select Folder","Assets/","");
                bool take = false;
                int takeI = 0;
                for(int i = 0; i < pathText.Length; i++) {
                    string chars = "";
                    if(!take)
                        for(int j = 0;j <= 6;j++) {
                            chars += pathText[i + j];
                            if(chars == "Assets") {
                                take = true;
                                takeI = i;
                                break;
                            }
                        }
                    else
                        break;
                }
                string finalText = "";
                for(int i = takeI; i< pathText.Length;i++) {
                    finalText += pathText[i];
                }
                pathText = finalText;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            return pathText;
        }
        public static void DrawSeparatorLine() {
            GUILayout.Space(5f);
            Rect rect = EditorGUILayout.GetControlRect(false,1f);
            EditorGUI.DrawRect(rect,new Color(0.6f,0.6f,0.6f,1f));
            GUILayout.Space(5f);
        }
        public static string textField(string textName,string baseText = "") {
            EditorGUIUtility.labelWidth = textName.Length * 7f;
            baseText = EditorGUILayout.TextField(textName,baseText,GUILayout.Width(230));
            EditorGUIUtility.labelWidth = 0;
            return baseText;
        }
    }
    public class F_Creator {
        public static AnimationClip BuildAnimationClip(string assetPath,string clipName,List<Sprite> frames,float timeBetweenFrames) {
            //AnimatorController aniController = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(assetPath+".controller");

            //Create Animation Clip
            AnimationClip clip = new AnimationClip();
            clip.frameRate = timeBetweenFrames;
            clip.name = clipName;

            //Create Animation
            ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[frames.Count];
            for(int i = 0; i < keyframes.Length;i++) {
                keyframes[i] = new ObjectReferenceKeyframe();
                keyframes[i].time = i / timeBetweenFrames;
                keyframes[i].value = frames[i];
            }
            AnimationUtility.SetAnimationClipSettings(clip,new AnimationClipSettings { loopTime = true });
            AnimationUtility.SetObjectReferenceCurve(clip,EditorCurveBinding.PPtrCurve("",typeof(SpriteRenderer),"m_Sprite"),keyframes);
            AssetDatabase.CreateAsset(clip,assetPath + clipName + ".anim");

            return clip;
        }

        public static UnityEditor.Animations.AnimatorController BuildAnimatorController(string assetPath,string animationName,List<AnimationClip> clips) {
            UnityEditor.Animations.AnimatorController aniController = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(assetPath+animationName+".controller");

            for(int i = 0; i < clips.Count;i++) {
                AnimatorState state = aniController.layers[0].stateMachine.AddState(clips[i].name);
                state.motion = clips[i];

            }
            return aniController;
        }
    }
}
