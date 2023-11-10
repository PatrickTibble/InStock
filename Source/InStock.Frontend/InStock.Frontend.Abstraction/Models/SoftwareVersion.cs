namespace InStock.Frontend.Abstraction.Models
{
    public class SoftwareVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }

        public SoftwareVersion()
        {

        }

        public SoftwareVersion(int major, int minor, int build, int revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public SoftwareVersion(string appVersion)
        {
            var components = appVersion.Split('.');
            for (int i = 0; i < components.Length; i++)
            {
                var component = components[i];
                if (int.TryParse(component, out int value))
                {
                    switch (i)
                    {
                        case 0:
                            Major = value;
                            break;
                        case 1:
                            Minor = value;
                            break;
                        case 2:
                            Build = value;
                            break;
                        case 3:
                            Revision = value;
                            break;
                        default: break;
                    }
                }
            }
        }

        public override string ToString()
            => $"{Major}.{Minor}{GetStringOrEmpty(Build)}{GetStringOrEmpty(Revision)}";

        private string GetStringOrEmpty(int component)
        {
            if (component > 0)
            {
                return $".{component}";
            }
            return string.Empty;
        }
    }
}