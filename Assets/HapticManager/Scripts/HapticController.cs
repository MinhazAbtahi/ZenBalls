using System;
using System.Collections;
using UnityEngine;
#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine.iOS;
#endif

public static class HapticController
{

    public enum HapticType { Light, Medium, Heavy, Soft, Rigid, Fail, Success, Warning }
    public static bool HapticEnabled = true;

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _GenerateHapticFromPattern(string pattern);
        [DllImport("__Internal")]
        private static extern void _GenerateLightImpact();
        [DllImport("__Internal")]
        private static extern void _GenerateMediumImpact();
        [DllImport("__Internal")]
        private static extern void _GenerateHeavyImpact();
        [DllImport("__Internal")]
        private static extern void _GenerateSoftImpact();
        [DllImport("__Internal")]
        private static extern void _GenerateRigidImpact();
        
        [DllImport("__Internal")]
        private static extern void _StartHapticEngine();
        [DllImport("__Internal")]
        private static extern void _PlayCustomHaptic(float intensity, float sharpness, double duration, double startDelay);

        [DllImport("__Internal")]
        private static extern void _PlayTapHaptic();

#endif

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        StartHapticEngine();
        Vibration.Init();
    }


    #region iOS General Haptics
    public static void GenerateHapticFromPattern(string pattern)
    {
#if UNITY_IOS && !UNITY_EDITOR
            if(HapticEnabled)_GenerateHapticFromPattern(pattern);
#else
        Debug.Log("AshLog: Only Works on iOS.");
#endif
    }

    internal static void StartHapticEngine()
    {
#if UNITY_IOS && !UNITY_EDITOR
            if(HapticEnabled)_StartHapticEngine();
#else
        Debug.Log("AshLog: StartHapticEngine Only Works on iOS.");
#endif
    }

    internal static void PlayCustomHaptic(float intensity, float sharpness, double duration, double startDelay)
    {
#if UNITY_IOS && !UNITY_EDITOR
            if(HapticEnabled)_PlayCustomHaptic(intensity, sharpness, duration, startDelay);
#else
        Debug.Log($"AshLog: PlayCustomHaptic Only Works on iOS. {intensity}-{sharpness}-{duration}-{startDelay}");
#endif
    }
    internal static void PlayDiscreteHaptic(float intensity, float sharpness, double startDelay = 0)
    {
        Debug.Log($"AshLog: PlayDiscreteHaptic Only Works on iOS. {intensity}-{sharpness}-{startDelay}");
        PlayCustomHaptic(intensity, sharpness, 0, startDelay);
    }
    internal static void PlayContinuousHaptic(float intensity, float sharpness, double duration, double startDelay = 0)
    {
        Debug.Log($"AshLog: PlayContinuousHaptic Only Works on iOS. {intensity}-{sharpness}-{duration}-{startDelay}");
        PlayCustomHaptic(intensity, sharpness, duration, startDelay);
    }

    private static bool ShouldReturn()
    {
#if UNITY_EDITOR
        return true;
#else
        return !HapticEnabled;
#endif
    }

    private static void L(string caller)
    {
        Debug.Log($"AshLog: {caller} Only Works on iOS/Android.");
    }

    #endregion iOS General Haptics

    #region Game Specific Haptics

    internal static void PlayTapHaptic(float alpha)
    {
        if (ShouldReturn()) { L("TapHaptic"); return; }

#if UNITY_IOS
            _PlayTapHaptic();
#elif UNITY_ANDROID
        Vibration.Vibrate();
#endif
    }
    #endregion Game Specific Haptics

    #region Generic Haptics
    internal static void PlayHaptic(HapticType type)
    {
        if (ShouldReturn()) { /*L("PlayHaptic");*/ return; }

        switch (type)
        {
            case HapticType.Light:
#if UNITY_IOS
                    _GenerateLightImpact();
#elif UNITY_ANDROID
                Vibration.Vibrate(20);
#endif
                break;


            case HapticType.Medium:
#if UNITY_IOS
                    _GenerateMediumImpact();
#elif UNITY_ANDROID
                Vibration.Vibrate(30);
#endif
                break;


            case HapticType.Heavy:
#if UNITY_IOS
                    _GenerateHeavyImpact();
#elif UNITY_ANDROID
                Vibration.Vibrate(40);

#endif
                break;

            case HapticType.Soft:
#if UNITY_IOS
                    _GenerateSoftImpact();
#elif UNITY_ANDROID
                Vibration.Vibrate(15);

#endif
                break;
            case HapticType.Rigid:
#if UNITY_IOS
                    _GenerateRigidImpact();
#elif UNITY_ANDROID
                Vibration.Vibrate(45);
#endif
                break;


            case HapticType.Success:
                Vibration.VibratePop();
                break;


            case HapticType.Warning:
#if UNITY_IOS
                    Vibration.VibratePeek();
#elif UNITY_ANDROID
                Vibration.Vibrate(40);
#endif
                break;

            case HapticType.Fail:
                Vibration.VibrateNope();
                break;


            default:
                break;
        }

    }

    #endregion Generic Haptics
}
