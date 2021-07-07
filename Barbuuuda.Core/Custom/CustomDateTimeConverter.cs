using Newtonsoft.Json.Converters;

namespace Barbuuuda.Core.Custom
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter() 
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
