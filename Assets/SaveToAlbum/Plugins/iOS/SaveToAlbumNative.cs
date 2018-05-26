//	
//	SaveToAlbumNative.Create();
//	SaveToAlbumNative.SaveImage()
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AOT;
using System.Runtime.InteropServices;
using System.Text;
using System;

public class SaveToAlbumNative : MonoBehaviour {

		#if UNITY_IOS

			[DllImport("__Internal")]
			private static extern void initialize();

			[DllImport("__Internal")]
			private static extern void deInitialize();

			[DllImport ("__Internal")]
			private static extern void saveImageToAlbum(byte[] imagebytes, int dataSize);

			[DllImport("__Internal")]
			private static extern void setOnCompleteCallback(OnCompleteDelegateMessage callback);

			private delegate void OnCompleteDelegateMessage();
			private static OnCompleteDelegateMessage onCompleteDelegate;

			public static UnityEvent ReceivedOnCompleteEvent;

		#endif

		///<Summary>
		/// Initialize the class in Objective-c to save album to photo album
		///</Summary>
		public static void Init() {
			#if UNITY_IOS 
				if (Application.platform == RuntimePlatform.IPhonePlayer) {

					if(ReceivedOnCompleteEvent == null) {
						ReceivedOnCompleteEvent = new UnityEvent();
					}
					
					initialize();
					
					onCompleteDelegate = delegateOnCompleteReceived;
					setOnCompleteCallback(onCompleteDelegate);
					
				}
			#else
				Debug.Log("Called Create on Save To Album");
			#endif
		}


		///<Summary>
		/// deinit the camera album objc class
		///</Summary>
		public static void deInit() {
			#if UNITY_IOS 
				if (Application.platform == RuntimePlatform.IPhonePlayer) {

					deInitialize();
				}
			#else
				Debug.Log("Called deInit on Save To Album");
			#endif
		}

		///<Summary>
		/// save album to photo album
		///</Summary>
		public static void SaveImage(byte[] imagebytes) {
			#if UNITY_IOS 
				if (Application.platform == RuntimePlatform.IPhonePlayer) {

					saveImageToAlbum(imagebytes, imagebytes.Length);
				}
			#else
				Debug.Log("Called SaveImage on Save To Album");
			#endif
		}

		[MonoPInvokeCallback(typeof(OnCompleteDelegateMessage))]
		private static void delegateOnCompleteReceived() {
			Debug.Log("delegateOnCompleteReceived");
			ReceivedOnCompleteEvent.Invoke();
		}



}
