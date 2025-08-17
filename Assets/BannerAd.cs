using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.AdManager;

public class BannerAd : MonoBehaviour
{
    private BannerView bannerView;

    void Start()
    {
        // MobileAds.Initialize(initStatus => { });

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

        string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

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
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}
