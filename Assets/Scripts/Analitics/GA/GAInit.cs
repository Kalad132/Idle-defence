using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

namespace Analytics
{
    public class GAInit : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }
    }
}