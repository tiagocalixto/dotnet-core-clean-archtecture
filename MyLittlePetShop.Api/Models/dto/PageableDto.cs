using System.Text.Json.Serialization;

namespace MyLittlePetShop.Api.Models.dto
{
    public class PageableDto<T>
    {
        public T Data { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
    }
}
