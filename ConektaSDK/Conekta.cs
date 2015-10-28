﻿using System;
using System.Drawing;
using System.Diagnostics;

#if __IOS__
using Foundation;
using ObjectiveC;
using ObjCRuntime;
using UIKit;
#endif

#if __ANDROID__
using Android;
#endif

namespace ConektaSDK {

	public abstract class Conekta {

		public static string ApiVersion { get; set; }
		public const string BaseUri = "https://api.conekta.io";
		public static string PublicKey { get; set; }
		#if __IOS__
		public static UIViewController _delegate { get; set; }
		#endif

		public static void collectDevice() {
			string SessionId = Conekta.DeviceFingerPrint ();
			string PublicKey = Conekta.PublicKey;

			string html = "<html style=\"background: blue;\"><head></head><body>";
			html += "<script type=\"text/javascript\" src=\"https://conektaapi.s3.amazonaws.com/v0.5.0/js/conekta.js\" data-public-key=\"" + PublicKey + "\" data-session-id=\"" + SessionId + "\"></script>";
			html += "</body></html>";

			string contentPath = Environment.CurrentDirectory;

			#if __IOS__
			UIWebView web = new UIWebView(new RectangleF(new PointF(0,0), new SizeF(0, 0)));
			web.LoadHtmlString(html, new NSUrl(contentPath, true));
			web.ScalesPageToFit = true;
			Conekta._delegate.View.AddSubview(web);
			#endif

			#if __ANDROID__

			#endif
		}

		public static string DeviceFingerPrint() {
			string uuidString = "";

			#if __IOS__
				//NSUuid uuid = new NSUuid();
				//uuidString = uuid.AsString();
				uuidString = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
				uuidString = uuidString.Replace("-", "");
			#elif __ANDROID__
				uuidString = Android.OS.Build.Serial;
			#elif WINDOWS_PHONE
				uuidString = Windows.Phone.System.Analytics.HostInformation.PublisherHostId;
			#endif

			return uuidString;
		}
	}
}