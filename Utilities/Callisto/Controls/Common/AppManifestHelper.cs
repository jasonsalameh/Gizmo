﻿﻿//
// Copyright (c) 2012 Tim Heuer
//
// Licensed under the Microsoft Public License (Ms-PL) (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Callisto.Controls.Common
{
	/// <summary>
	/// A helper API that retrieves some data from your app's AppxManifest.xml file for you.
	/// </summary>
    public class AppManifestHelper
    {
        public async static Task<VisualElement> GetManifestVisualElementsAsync()
        {
            // the path for the manifest
            XDocument xmldoc;
            using (Stream manifestStream = await Windows.ApplicationModel.Package.Current.InstalledLocation.OpenStreamForReadAsync("AppxManifest.xml"))
            {
                xmldoc = XDocument.Load(manifestStream);
            }

            // set the XNamespace and name for the VisualElements node we want
            var xn = XName.Get("VisualElements", "http://schemas.microsoft.com/appx/2013/manifest");

            // parse the VisualElements node only, pulling out what we need
            // NOTE: This will get only the first Application (which is the mainstream case)
            // TODO: Need to take into account that DisplayName/Description may be localized using ms-resource:{foo}
            var visualElementNode = (from vel in xmldoc.Descendants(xn)
                                     select new VisualElement
                                     {
                                         DisplayName = vel.Attribute("DisplayName").Value,
                                         Description = vel.Attribute("Description").Value,
                                         LogoUri = new Uri(string.Format("ms-appx:///{0}", vel.Attribute("Square150x150Logo").Value.Replace(@"\", @"/"))),
                                         SmallLogoUri = new Uri(string.Format("ms-appx:///{0}", vel.Attribute("Square30x30Logo").Value.Replace(@"\", @"/"))),
                                         BackgroundColorAsString = vel.Attribute("BackgroundColor").Value
                                     }).FirstOrDefault();
            if (visualElementNode == null) throw new ArgumentNullException("Could not parse the VisualElements from the app manifest.");

            return visualElementNode;
        }
    }

    public class VisualElement
    {
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
        public string DisplayName { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
        public string Description { get; set; }
		/// <summary>
		/// Gets or sets the logo URI.
		/// </summary>
        public Uri LogoUri { get; set; }
		/// <summary>
		/// Gets or sets the small logo URI.
		/// </summary>
        public Uri SmallLogoUri { get; set; }
		/// <summary>
		/// Gets or sets the background color as string.
		/// </summary>
        public string BackgroundColorAsString { get; set; }
		/// <summary>
		/// Gets the color of the background.
		/// </summary>
        public Windows.UI.Color BackgroundColor
        {
            get
            {
                return BackgroundColorAsString.ToColor();
            }
        }
    }
}
