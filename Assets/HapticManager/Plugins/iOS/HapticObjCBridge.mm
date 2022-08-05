#include "UnityFramework/UnityFramework-Swift.h"

extern "C" {
    void _GenerateHapticFromPattern (const char* pattern) {
        NSLog(@"Ashlog: Native iOS _GenerateHapticFromPattern");
        
        NSString *nsPattern = [NSString stringWithUTF8String:pattern];
        [[HapticSwiftBridge shared]generatePattern: nsPattern];
    }



    void _GenerateLightImpact () {
        NSLog(@"Ashlog: Native iOS _GenerateLightImpact");
        [[HapticSwiftBridge shared]impactLight];
    }
    void _GenerateMediumImpact () {
        NSLog(@"Ashlog: Native iOS _GenerateMediumImpact");
        [[HapticSwiftBridge shared]impactMedium];
    }
    void _GenerateHeavyImpact () {
        NSLog(@"Ashlog: Native iOS _GenerateHeavyImpact");
        [[HapticSwiftBridge shared]impactHeavy];
    }
    void _GenerateSoftImpact () {
        NSLog(@"Ashlog: Native iOS _GenerateSoftImpact");
        [[HapticSwiftBridge shared]impactSoft];
    }
    void _GenerateRigidImpact () {
        NSLog(@"Ashlog: Native iOS _GenerateRigidImpact");
        [[HapticSwiftBridge shared]impactRigid];
    }

    void _StartHapticEngine () {
        NSLog(@"Ashlog: Native iOS _StartHapticEngine");
        [[HapticSwiftBridge shared]startEngineIfNotRunning];
    }
    void _PlayCustomHaptic(float intensity, float sharpness, double duration, double startDelay){
        NSLog(@"Ashlog: Native iOS _PlayCustomHaptic");
        [[HapticSwiftBridge shared]playCustomHaptic:intensity sharpness:sharpness duration:duration startDelay:startDelay];
    }

    void _PlayTapHaptic(){
        NSLog(@"Ashlog: Native iOS _PlayTapHaptic");
//        if([[HapticSwiftBridge shared]haveHapticSupport]){
//            [[HapticSwiftBridge shared]playCustomHaptic:0.7 sharpness:0.4 duration:0 startDelay:0];
//        }
//        else{
            [[HapticSwiftBridge shared]impactHeavy];
//        }
    }
    void _PlayFatLadyLandHaptic(){
        NSLog(@"Ashlog: Native iOS _PlayFatLadyLandHaptic");
//        if([[HapticSwiftBridge shared]haveHapticSupport]){
//            [[HapticSwiftBridge shared]playCustomHaptic:0.8 sharpness:0.3 duration:0.2 startDelay:0];
//        }
//        else{
            [[HapticSwiftBridge shared]generatePattern: @"OOOOO"];
//        }
    }
    void _PlaySharkLandHaptic(){
        NSLog(@"Ashlog: Native iOS _PlaySharkLandHaptic");
//        if([[HapticSwiftBridge shared]haveHapticSupport]){
//            [[HapticSwiftBridge shared]playCustomHaptic:0.6 sharpness:0.3 duration:0 startDelay:0];
//        }
//        else{
            [[HapticSwiftBridge shared]generatePattern: @"OOOOO"];
//        }
    }


    void _PlayHitJellyFishHaptic(double duration){
        NSLog(@"Ashlog: Native iOS _PlayHitJellyFishHaptic");
        [[HapticSwiftBridge shared]playCustomHaptic:0.8 sharpness:0.5 duration:duration startDelay:0];
    }
    void _PlayHitSmallFishHaptic(){
        NSLog(@"Ashlog: Native iOS _PlayHitSmallFishHaptic");
        [[HapticSwiftBridge shared]impactSoft];
    }
    void _PlayHitSharkHaptic(){
        NSLog(@"Ashlog: Native iOS _PlayHitSharkHaptic");
        [[HapticSwiftBridge shared]playCustomHaptic:0.5 sharpness:0.6 duration:0.3 startDelay:0];
    }
    void _PlayHitBirdHaptic(){
        NSLog(@"Ashlog: Native iOS _PlayHitBirdHaptic");
        [[HapticSwiftBridge shared]playCustomHaptic:0.3 sharpness:0.4 duration:0.10 startDelay:0];
    }
    void _PlayJumpHaptic(double duration){
        NSLog(@"Ashlog: Native iOS _PlayJumpHaptic");
        [[HapticSwiftBridge shared]playCustomHaptic:0.5 sharpness:0.5 duration:duration startDelay:0];
    }

    void _PlayEvolveHaptic(){
        NSLog(@"Ashlog: Native iOS _PlayEvolveHaptic");
        [[HapticSwiftBridge shared]playCustomHaptic:0.5 sharpness:0.5 duration:.25 startDelay:0];
    }

}
