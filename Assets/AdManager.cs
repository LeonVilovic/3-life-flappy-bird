using GoogleMobileAds.Api;
using GoogleMobileAds.Api.AdManager;
using System.IO;
using UnityEngine;

public class AdManager : MonoBehaviour
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

    private AdsConfig adsConfig;

    private InterstitialAd interstitial;

    private BannerView bannerView;

    private const string adUnitIdInterstitial = "ca-app-pub-3940256099942544/1033173712"; // Test ID
    private const string adUnitIdBanner = "ca-app-pub-3940256099942544/6300978111"; // Test ID
    //private const string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

    public void InitializeMobileAdsAndRaiseAdEvents()
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
        // Ensure all ad events are raised on Unity main thread
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
    }

    public void LoadInterstitialAd()
    {
        var adRequest = new AdRequest();

        InterstitialAd.Load(adUnitIdInterstitial, adRequest,
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
                interstitial = ad;

                // Register event handlers for this ad
                interstitial.OnAdFullScreenContentOpened += () => Debug.Log("Ad opened");
                interstitial.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Ad closed, loading a new one...");
                    interstitial.Destroy();
                    interstitial = null;
                    LoadInterstitialAd(); // preload next ad
                };

                Debug.Log("Interstitial ad loaded successfully!");
            });
    }

    public void ShowInterstitialAd()
    {
        //if (interstitial != null)
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
            Debug.Log("ShowInterstitialAd() - interstitial.Show(); triggerd");
        }
        else
        {
            Debug.Log("Interstitial Ad not ready to show yet.");
        }
    }

    public void LoadAndShowBannerAd()
    {
        bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, AdPosition.Bottom);

        // Instead of Builder, just create a plain AdManagerAdRequest
        AdManagerAdRequest request = new AdManagerAdRequest();

        // Optional: customize request
        //request.PublisherProvidedId = "example_id";
        //request.CustomTargeting.Add("key", "value");
        //request.CategoryExclusions.Add("category_label");

        bannerView.LoadAd(request);
    }

    private void OnDestroy()
    {
        interstitial?.Destroy();
    }
}
