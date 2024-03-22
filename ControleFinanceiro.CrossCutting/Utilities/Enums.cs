using System.ComponentModel;

namespace ControleFinanceiro.CrossCutting.Utilities
{
    public static class Enums
    {
        public const string defaultDescription = "Nenhum";

        public enum ExpenseStatus
        {
            [Description("Selecione")]
            None = 0,
            [Description("Pendente")]
            Pending = 1,
            [Description("Quitado")]
            Paid = 2,
        }

        public static string? GetDescription<T>(T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static IEnumerable<string> GetDescriptions<T>()
        {
            var attributes = typeof(T).GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .ToList();

            attributes.Remove(attributes.Find(x => x.Description == defaultDescription));

            return attributes.Select(x => x.Description);
        }

        public static int? GetIndexByDescription<T>(string description)
        {
            if (!typeof(T).IsEnum)
                return null;

            Array EnumValues = Enum.GetValues(typeof(T));

            foreach (var item in EnumValues)
            {
                var fieldInfo = typeof(T).GetType().GetField(item.ToString());

                if (fieldInfo != null)
                {
                    var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

                    if (attrs != null && attrs.Length > 0)
                    {
                        if (((DescriptionAttribute)attrs[0]).Description == description)
                            return (int?)fieldInfo.GetRawConstantValue();
                    }
                }
            }

            return null;
        }
    }
}
