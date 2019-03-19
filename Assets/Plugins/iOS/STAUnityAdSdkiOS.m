//  Unity Plugin 2.0
//  Created by StartApp on 04/11/18.
//  Copyright (c) 2018 StartApp. All rights reserved.


#import "STAUnityAdSdkiOS.h"
#import "STAStartAppSDK.h"


@implementation STAUnityAdSdkiOS


void sta_initilize(const char* appId, const char* devId) {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    sdk.isUnityEnvironment = YES;
    sdk.appID = [NSString stringWithUTF8String:appId];
    sdk.devID = [NSString stringWithUTF8String:devId];
}

void sta_setUserConsent(bool consent, const char* consentType, long timestamp) {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk setUserConsent:consent forConsentType:[NSString stringWithUTF8String:consentType] withTimestamp:timestamp];
}

void sta_disableReturnAd() {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk disableReturnAd];
}

void sta_enterBackground() {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk unityAppDidEnterBackground];
}

void sta_enterForeground() {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk unityAppWillEnterForeground];
}

void sta_setUnityVersion(const char* unityVersion) {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk setUnityVersion:[NSString stringWithUTF8String:unityVersion]];
}

void sta_setUnitySupportedOrientations(int supportedOrientations) {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk setUnitySupportedOrientations:supportedOrientations];
}

void sta_setUnityAutoRotation(int autoRotation) {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk setUnityAutoRotation:autoRotation];
}

void sta_showSplashAd(const char* appName, const char* logoImageName, int theme, int orientation) {
    STASplashPreferences* pref = [[STASplashPreferences alloc] init];
    pref.splashMode = STASplashModeTemplate;
    pref.splashTemplateTheme = theme;
    
    if (logoImageName) {
        pref.splashTemplateIconImageName = [NSString stringWithUTF8String:logoImageName];
    }
    
    if (appName) {
        pref.splashTemplateAppName = [NSString stringWithUTF8String:appName];
    }
    
    pref.isLandscape = orientation == 1;
    
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk showSplashAdWithPreferences:pref];
    [sdk unitySDKInitialize];
}

void sta_showDefaultSplashAd() {
    STAStartAppSDK* sdk = [STAStartAppSDK sharedInstance];
    [sdk showSplashAd];
    [sdk unitySDKInitialize];
}

@end

