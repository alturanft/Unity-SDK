using System;
using UnityEngine;

namespace AlturaNFT.Editor
{
	public class Readme_AlturaNFT : ScriptableObject {
		public Texture2D icon;
		public string title;
		public Section[] sections;
		public bool loadedLayout;
		public bool notFirstload = false;
	
		[Serializable]
		public class Section {
			public string heading, text, linkText, url;
		}
	}
}
