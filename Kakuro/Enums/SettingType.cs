using System.Runtime.Serialization;

namespace Kakuro.Enums
{
    public enum SettingType
    {
        [EnumMember(Value = "Show correct values (+1 minute, post-cleaning)")]
        ShowCorrectValues
    }
}
