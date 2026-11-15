using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BloodBankManagementSystem.Models
{
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            return member?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? value.ToString();
        }
    }
}