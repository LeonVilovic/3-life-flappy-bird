using GoogleMobileAds.Api;
using GoogleMobileAds.Api.AdManager;
using System.IO;
using UnityEngine;

public class AdManager
{
    private static AdManager instance = null;
    private AdManager() { }
    public static AdManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AdManager();
            return instance;
        }
    }

    private InterstitialAd interstitialAd;

    private BannerView bannerView;

    public void InitializeMobileAds()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

            Debug.Log("Google Mobile Ads initialization complete.");

            // Google Mobile Ads events are raised off the Unity Main thread. If you need to
            // access UnityEngine objects after initialization,
            // use MobileAdsEventExecutor.ExecuteInUpdate(). For more information, see:
            // https://developers.google.com/admob/unity/global-settings#raise_ad_events_on_the_unity_main_thread
        });
    }
    public void EnableRaiseAdEventsOnUnityMainThread()
    {

        MobileAds.RaiseAdEventsOnUnityMainThread = true;
    }
    public void ShowAdInspector()
    {
        MobileAds.OpenAdInspector(error =>
        {
            if (error != null)
            {
                Debug.LogError("Ad Inspector failed: " + error);
            }
            else
            {
                Debug.Log("Ad Inspector opened successfully.");
            }
        });
    }

    public void InterstitialAdLoad()
    {
        //you can call this function only once
        var adRequest = new AdManagerAdRequest();

        InterstitialAd.Load(AdsConfig.AdUnitIdInterstitial, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError($"Interstitial failed to load: {error.GetMessage()}");
                    return;
                }

                if (ad == null)
                {
                    Debug.LogError("Interstitial failed to load: ad is null");
                    return;
                }

                // Assign the loaded ad to your member variable
                interstitialAd = ad;

                // Register event handlers for this ad
                interstitialAd.OnAdFullScreenContentOpened += () => Debug.Log("Ad opened");
                interstitialAd.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Ad closed, loading a new one...");
                    interstitialAd.Destroy();
                    interstitialAd = null;
                    InterstitialAdLoad(); // preload next ad
                };

                Debug.Log("Interstitial ad loaded successfully!");
            });
    }

    public void InterstitialAdShow()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            Debug.Log("ShowInterstitialAd() - interstitial.Show(); triggerd");
        }
        else
        {
            Debug.Log("Interstitial Ad not ready to show yet.");
        }
    }

    public void BannerAdLoadAndShow()
    {
        bannerView = new BannerView(AdsConfig.AdUnitIdBanner, AdSize.Banner, AdPosition.Top);

        AdManagerAdRequest request = new AdManagerAdRequest();

        // Optional: customize request
        //request.PublisherProvidedId = "example_id";
        //request.CustomTargeting.Add("key", "value");
        //request.CategoryExclusions.Add("category_label");

        bannerView.LoadAd(request);
        
    }
    public void BannerAdDestroy()
    {
        bannerView.Destroy();  
    }
    
}
