using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fietsbaas.Services
{
    [ContentProperty (nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        private static readonly Dictionary<string, ImageSource> cache = new Dictionary<string, ImageSource>();

        public string Source { get; set; } 

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            if (!cache.TryGetValue(Source, out var imageSource))
            {
                imageSource = ImageSource.FromResource( Source, typeof( ImageResourceExtension ).GetTypeInfo().Assembly );
                cache[Source] = imageSource;
            }

            return imageSource;
        }
    }
}
