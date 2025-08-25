using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter.Audio.Editor
{
    [CustomEditor(typeof(AudioService))]
    public class AudioServiceEditor : UnityEditor.Editor
    {
        private const string ENUM_NAME = "AudioTrack";
        private const string ENUM_PATH = "Assets/Scripts/Audio/";
        
        public override void OnInspectorGUI()
        {
            var service = (AudioService)target;
            base.OnInspectorGUI();

            if (!GUILayout.Button("Save")) return;

            using (var file = File.CreateText($"{ENUM_PATH}{ENUM_NAME}.cs"))
            {
                file.WriteLine("namespace SpaceShooter.Audio \n{");
                file.WriteLine("    public enum " + ENUM_NAME + " \n\t{");
                
                var i = 0;
                foreach (var track in service.AudioTracks)
                {
                    if (track.Clip == null) continue;
                    
                    var line = track.Name.Replace(" ", string.Empty);
                    if (string.IsNullOrEmpty(line)) continue;
                    
                    file.WriteLine($"\t\t{line} = {i},");
                    i++;
                }
                
                file.WriteLine("\t}\n}");
            }
            
            AssetDatabase.ImportAsset($"{ENUM_PATH}{ENUM_NAME}.cs");
        }
    }
}