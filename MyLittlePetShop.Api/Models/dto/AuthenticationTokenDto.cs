using System.Text.Json.Serialization;

namespace MyLittlePetShop.Api.Models.dto
{
    public class AuthenticationTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("access_token_expires_in")]
        public int AccessTokenExpiresIn { get; set; }
    }
}