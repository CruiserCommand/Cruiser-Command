using UnityEngine;
using System.Collections;
using CC.eLog;

// Awake will be run at app startup, which is the only use of this class.

public class OnStartup : MonoBehaviour {

    // Will be run when the app starts.
    public void Start()
    {
        Log.Fatal("general", "Up and running");
        Log.SetLogLevel(ELogLevel.Info);
        Log.AddModules("all");
    }
}