using System.Runtime.Serialization;

namespace Kakuro.Enums
{
    public enum SettingType
    {
        [EnumMember(Value = "Show correct answers (+1 minute, post-cleaning)")]
        ShowCorrectAnswers
    }
}
