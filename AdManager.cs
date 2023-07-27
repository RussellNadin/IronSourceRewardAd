using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    //The IronSource demo does not show the buttons or scripts attatched to the buttons in Unity's hierarchy or scene windows.
    //The IronSource guide needs to be referanced to find the content in the unavailible demo button scripts.
    //The IronSource guide is found here: https://developers.is.com/ironsource-mobile/unity/unity-plugin/#step-6
    //The IronSource ad rewards guide is here: https://developers.is.com/ironsource-mobile/unity/rewarded-video-integration-unity/#step-1


    [SerializeField] private TextMeshProUGUI adButtonText;

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void Start()
    {
        IronSource.Agent.init("1acd0d105", IronSourceAdUnits.REWARDED_VIDEO);

        IronSource.Agent.validateIntegration();
    }

    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        //AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    private void SdkInitializationCompletedEvent()
    {
        Debug.Log("SdkInitializationCompletedEvent");
    }

    //Rewarded buttons
    public void ShowRewardedAd()
    {
        //Pause game.
        Time.timeScale = 0f;

        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            Debug.Log("ShowRewardedAd succeded");

            //Show the ad. It should be available.
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("ShowRewardedAd failed");

            //Try to show the ad even if unavailable.
            IronSource.Agent.showRewardedVideo();
        }
    }

    //Rewarded Ad Callbacks

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        //This is the most important Ad callback. This is where to give the player a reward.
        Debug.Log("##################### AdManager - REWARD PLAYER #####################");
        adButtonText.text = "The Ad worked! Have a reward.";

        //Unpause game.
        Time.timeScale = 1f;
    }














    /************* RewardedVideo AdInfo Delegates *************/
    // Indicates that there’s an available ad.
    // The adInfo object includes information about the ad that was loaded successfully
    // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("RewardedVideoOnAdAvailable");
    }
    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
        Debug.Log("RewardedVideoOnAdUnavailable");
    }
    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("RewardedVideoOnAdOpenedEvent");
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("RewardedVideoOnAdClosedEvent");
    }
    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
        Debug.Log("RewardedVideoOnAdShowFailedEvent");
    }
    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        Debug.Log("RewardedVideoOnAdClickedEvent");
    }




}
