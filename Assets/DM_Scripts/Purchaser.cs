/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Gley.EasyIAP;
using EasyUI.Toast;
using Gley.MobileAds;
using UnityEngine.UI;

namespace Gley.EasyIAP.Internal
{
    public class Purchaser : MonoBehaviour
    {

        public class DM_StoreProducts
        {
            public ShopProductNames name;
            public bool bought;

            public DM_StoreProducts(ShopProductNames name, bool bought)
            {
                this.name = name;
                this.bought = bought;
            }
        }
        public Text[] listPrice;
        private bool purchaseInProgress;
        private bool initializationInProgress;
        public Button RestoreButton;
        private List<DM_StoreProducts> consumableProducts = new List<DM_StoreProducts>();

        private void Awake()
        {
            /// Debug.Log("INIT :" + IAPManager.Instance.IsInitialized());
            if (!API.IsInitialized())
            {
                if (initializationInProgress == false)
                {

                    initializationInProgress = true;
                    //Initialize IAP
                    API.Initialize(InitializeResult);
                }
            }
            else
            {
                    GetListPrice();
            }

#if GLEY_IAP_IOS
		RestoreButton.gameObject.SetActive(true);
#else
            RestoreButton.gameObject.SetActive(false);
#endif

        }





        //#if GLEY_IAP_IOS

        public void Restore()
        {
            Gley.EasyIAP.API.RestorePurchases(ProductRestoredCallback, RestoreDone);
        }

        private void ProductRestoredCallback(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                Debug.Log("Restore product success!: " + message);
            }
            else
            {
                //an error occurred in the buy process, log the message for more details
                Debug.Log("Restore product failed: " + message);
            }
        }

        private void RestoreDone()
        {
            Debug.Log("Restore done");
        }

        //#endif


        private void GetListPrice()
        {
            for (int i = 0; i <= listPrice.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            listPrice[0].text = API.GetLocalizedPriceString(ShopProductNames.product_1);
                            break;
                        }
                    case 1:
                        {
                            listPrice[1].text = API.GetLocalizedPriceString(ShopProductNames.product_2);
                            break;
                        }
                    case 2:
                        {
                            listPrice[2].text = API.GetLocalizedPriceString(ShopProductNames.product_3);
                            break;
                        }
                    case 3:
                        {
                            listPrice[3].text = API.GetLocalizedPriceString(ShopProductNames.removeads);
                            break;
                        }
                

                }
            }
        }


        private void InitializeResult(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
        {
            // Debug.Log("Chay vao day");
            initializationInProgress = false;
            consumableProducts = new List<DM_StoreProducts>();
            if (status == IAPOperationStatus.Success)
            {
                GetListPrice();
                //IAP was successfully initialized
                //loop through all products and check which one are bought to update our variables
                for (int i = 0; i < shopProducts.Count; i++)
                {
               

                    //construct a different list of each category of products, for an easy display purpose, not required
                    switch (shopProducts[i].productType)
                    {
                        case ProductType.Consumable:
                            consumableProducts.Add(new DM_StoreProducts(IAPManager.Instance.ConvertNameToShopProduct(shopProducts[i].productName), shopProducts[i].active));
                            break;

                    }
                }


            }

            if (IAPManager.Instance.debug)
            {
                Debug.Log("Init status: " + status + " message " + message);
                //Debug.Log("List products: " + consumableProducts.Count);

            }
        }

        public void MakeBuyProduct(int indexProduct)
        {

            switch (indexProduct)
            {
                case 1:
                    {
                        API.BuyProduct(ShopProductNames.product_1, ProductBought);
                        break;
                    }
                case 2:
                    {
                        API.BuyProduct(ShopProductNames.product_2, ProductBought);
                        break;
                    }
                case 3:
                    {
                        API.BuyProduct(ShopProductNames.product_3, ProductBought);
                        break;
                    }
                case 4:
                    {
                        API.BuyProduct(ShopProductNames.removeads, ProductBought);
                        break;
                    }
               

            }

        }


        /// <summary>
        /// automatically called after one product is bought
        /// </summary>
        /// <param name="status">The purchase status: Success/Failed</param>
        /// <param name="message">Error message if status is failed</param>
        /// <param name="product">the product that was bought, use the values from shop product to update your game data</param>
        private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
        {
            purchaseInProgress = false;
            if (status == IAPOperationStatus.Success)
            {
                if (IAPManager.Instance.debug)
                {
                    Debug.Log("Buy product completed: " + product.localizedTitle + " receive value: " + product.value);
                    ScreenWriter.Write("Buy product completed: " + product.localizedTitle + " receive value: " + product.value);
                }

                //each consumable gives coins in this example
                if (product.productType == ProductType.Consumable)
                {
                 
                        
                    if(product.productName == "product_1")
                    {
                       
                        RewardScriptableObject.instance.tipRemoveCount += product.value;
                        Toast.Show("Successful purchase " + product.value + " Remove Tip!", 3f, ToastColor.Green);
                        MenuPanel.instance.HideIapPanel();
                       
                    }

                    if (product.productName == "product_2")
                    {

                        RewardScriptableObject.instance.tipLightCount += product.value;
                        Toast.Show("Successful purchase " + product.value + " Hint Tip!", 3f, ToastColor.Green);
                        MenuPanel.instance.HideIapPanel();

                    }

                    if (product.productName == "product_3")
                    {

                        RewardScriptableObject.instance.tipUndoCount += product.value;
                        Toast.Show("Successful purchase " + product.value + " Undo Tip!", 3f, ToastColor.Green);
                        MenuPanel.instance.HideIapPanel();

                    }
                    Base._instance.UpdateCount();

                }

                else if (product.productType == ProductType.NonConsumable)
                {
                    //TODO: remove ads
                    MobileAds.API.RemoveAds(true);
                    Toast.Show("Successful remove ads !", 3f, ToastColor.Green);
                }



            }
            else
            {
                //en error occurred in the buy process, log the message for more details
                if (IAPManager.Instance.debug)
                {
                    Debug.Log("Buy product failed: " + message);
                    ScreenWriter.Write("Buy product failed: " + message);
                }
            }
        }


        public void OnClickRewardAd()
        {

            if (Gley.MobileAds.API.IsRewardedVideoAvailable())
            {
                Gley.MobileAds.API.ShowRewardedVideo(completeMethod);
            }

        }

        private void completeMethod(bool s)
        {
            if (s)
            {
                MenuPanel.instance.HideIapPanel();
                Base._instance.randomRewardindex = UnityEngine.Random.Range(0, 3);
                Base._instance.randomRewardCount = UnityEngine.Random.Range(1, 2);
                switch (Base._instance.randomRewardindex)
                {
                    case 0:
                        RewardScriptableObject.instance.tipRemoveCount += Base._instance.randomRewardCount;
                        break;
                    case 1:
                        RewardScriptableObject.instance.tipLightCount += Base._instance.randomRewardCount;
                        break;
                    case 2:
                        RewardScriptableObject.instance.tipUndoCount += Base._instance.randomRewardCount;
                        break;
                }
                UIManager.selfInstance.rewardPopup.gameObject.SetActive(true);
                Base._instance.UpdateCount();

            }
            else
            {
                Debug.Log("NO REWARD");
            }
        }

    }
}

