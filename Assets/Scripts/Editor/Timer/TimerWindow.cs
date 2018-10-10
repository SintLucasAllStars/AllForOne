using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class TimerWindow : EditorWindow
{
    private string _test = "Hoi";

    private static DateTime old;

    private DateTime now;

    private static TimeSpan interval;
    private static Stopwatch _stopwatch;

    private string totalTime;
    private void Awake()
    {
        //SetTimeFile();
    }

 

    [MenuItem("Timer/Show Window")]
    public static void ShowWindow()
    {
        old = new DateTime();
        _stopwatch = new Stopwatch();
        _stopwatch.Start();    
        
   
        GetWindow(typeof(TimerWindow)).Show();
        
        interval = new TimeSpan();
       
    }

    private void Update()
    {
        interval = _stopwatch.Elapsed;
        totalTime = string.Format("{0}:{1}:{2}", interval.Hours, interval.Minutes, interval.Seconds);
        FocusWindowIfItsOpen(typeof(TimerWindow));
       
    }

    private void OnGUI()
    {
        
        GUI.Label(new Rect(0,40,100,40),GUI.tooltip );

     
        
        
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Timer", GUILayout.Width(EditorGUIUtility.labelWidth - 4));
            EditorGUILayout.SelectableLabel(totalTime, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        }
        EditorGUILayout.EndHorizontal();
    }
}
