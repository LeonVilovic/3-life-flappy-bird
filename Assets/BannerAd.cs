using GoogleMobileAds.Api;
using GoogleMobileAds.Api.AdManager;
using System.IO;
using UnityEngine;


[System.Serializable]
public class AdConfig2
{
    public string adUnitId;
}

public class BannerAd : MonoBehaviour
{
    private BannerView bannerView;
    private AdConfig2 adConfig;

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
        // Ensure all ad events are raised on Unity main thread
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        // Load JSON from StreamingAssets
        string path = Path.Combine(Application.streamingAssetsPath, "adConfig.json");
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            adConfig = JsonUtility.FromJson<AdConfig2>(jsonString);
            Debug.Log("Loaded AdConfig JSON: " + jsonString);
        }
        else
        {
            Debug.LogError("adConfig.json not found at path: " + path);
            return;
        }

        //string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

        bannerView = new BannerView(adConfig.adUnitId, AdSize.Banner, AdPosition.Bottom);

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
