using System.ComponentModel;

namespace CSM.Bynder.Settings;

public class BynderFieldSettings
{
    public string Hint { get; set; }
    public bool Required { get; set; }

    [DefaultValue(true)]
    public bool Multiple { get; set; } = true;
}
