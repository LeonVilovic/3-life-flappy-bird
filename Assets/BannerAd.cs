using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.AdManager;

public class BannerAd : MonoBehaviour
{
    private BannerView bannerView;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Instead of Builder, just create a plain AdManagerAdRequest
        AdManagerAdRequest request = new AdManagerAdRequest();

        // Optional: customize request
        request.PublisherProvidedId = "example_id";
        request.CustomTargeting.Add("key", "value");
        request.CategoryExclusions.Add("category_label");

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
