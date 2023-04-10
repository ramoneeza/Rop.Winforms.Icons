namespace Rop.Winforms.DuotoneIcons.GoogleMaterialBuild
{
    public record Data
    {
        public string Root { get; init; }
        public string Asset_Url_Pattern { get; init; }
        public DataIcon[] Icons { get; init; } 
    }

    public record DataIcon
    {
        public string Name { get; init; }
        public string[] Unsupported_Families { get; init; }=Array.Empty<string>();
        public string[] Categories { get; init; } = Array.Empty<string>();
        public int Version { get; init; }
        public string[] Tags { get; init; } = Array.Empty<string>();
    }
}
