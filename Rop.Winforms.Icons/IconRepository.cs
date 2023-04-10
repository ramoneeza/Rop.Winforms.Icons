using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Rop.Winforms.Icons
{
    public static class IconRepository
    {
        private static readonly PrivateFontCollection _privateFontCollection = new PrivateFontCollection();
        private static readonly ConcurrentDictionary<string,IEmbeddedIcons> _loadedIconsByName = new (StringComparer.OrdinalIgnoreCase);
        private static readonly ConcurrentDictionary<FontFamily,IEmbeddedIcons> _loadedIcons = new ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle,IEmbeddedIcons> _loadedIconsByType = new ();
        private static byte[] GetResource(Type t,string name)
        {
            var assembly = t.Assembly;
            string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.EndsWith(name,StringComparison.OrdinalIgnoreCase));
            if (resourceName == null) return null;
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null) return null;
            using MemoryStream mstream = new MemoryStream();
            stream.CopyTo(mstream);
            return mstream.ToArray();
        }

        private static FontFamily LoadFont(Type t, string ttfname)
        {
            var memoryfont = GetResource(t, ttfname);
            var length = memoryfont.Length;
            System.IntPtr data = Marshal.AllocCoTaskMem(length);
            Marshal.Copy(memoryfont, 0, data,length);
            var m = _privateFontCollection.Families.Length;
            _privateFontCollection.AddMemoryFont(data,length);
            var fn = _privateFontCollection.Families[0];
            return fn;
        }

        public static IEmbeddedIcons GetEmbeddedIcons<T>() where T : IEmbeddedIcons
        {
            var t = typeof(T);
            return GetEmbeddedIcons(t);
        }
        public static IEmbeddedIcons GetEmbeddedIcons(Type t)
        {
            if (_loadedIconsByType.TryGetValue(t.TypeHandle, out var bank)) return bank;
            bank = (IEmbeddedIcons)Activator.CreateInstance(t, _privateFontCollection);
            _loadedIconsByType[t.TypeHandle] = bank;
            _loadedIconsByName[bank.FontName] = bank;
            _loadedIcons[bank.FontFamily] = bank;
            return bank;
        }
        public static IEmbeddedIcons GetEmbeddedIcons(FontFamily family)
        {
            _loadedIcons.TryGetValue(family, out var bank);
            return bank;
        }
        public static IEmbeddedIcons GetEmbeddedIcons(string name)
        {
            _loadedIconsByName.TryGetValue(name, out var bank);
            return bank;
        }
        public static bool IsLoaded(string name)
        {
            return _loadedIconsByName.ContainsKey(name);
        }
    }
}