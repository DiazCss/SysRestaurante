using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public class PayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _secret;

        public PayPalService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _clientId = configuration["PayPal:ClientId"];
            _secret = configuration["PayPal:Secret"];
        }

        public async Task<string> ObtenerToken()
        {
            try
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_secret}"));
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                var contenido = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", contenido);

                respuesta.EnsureSuccessStatusCode();

                var datos = await respuesta.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<JsonElement>(datos);

                if (json.TryGetProperty("access_token", out var token))
                {
                    return token.GetString();
                }

                throw new Exception("No se pudo obtener el token de acceso de PayPal.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el token de PayPal: " + ex.Message);
            }
        }

        public async Task<string> CrearOrden(decimal total, string moneda = "USD")
        {
            try
            {
                var token = await ObtenerToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var cuerpo = new
                {
                    intent = "CAPTURE",
                    purchase_units = new[]
                    {
                        new
                        {
                            amount = new
                            {
                                currency_code = moneda,
                                value = total.ToString("F2")
                            }
                        }
                    },
                    application_context = new
                    {
                        return_url = "https://localhost:7245/Pagos/Confirmacion", 
                        cancel_url = "https://localhost:7245/Pagos/Cancelar" // Crear o redigir a una vista despues de cancelar la confirmacion
                    }
                };

                var contenido = new StringContent(JsonSerializer.Serialize(cuerpo), Encoding.UTF8, "application/json");
                var respuesta = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", contenido);

                respuesta.EnsureSuccessStatusCode();

                var datos = await respuesta.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<JsonElement>(datos);

                foreach (var link in json.GetProperty("links").EnumerateArray())
                {
                    if (link.GetProperty("rel").GetString() == "approve")
                    {
                        return link.GetProperty("href").GetString();
                    }
                }

                throw new Exception("No se encontró el enlace de aprobación en la respuesta de PayPal.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la orden de PayPal: " + ex.Message);
            }
        }

        public async Task<JsonElement> CapturarOrden(string orderId)
        {
            try
            {
                var token = await ObtenerToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var contenido = new StringContent("{}", Encoding.UTF8, "application/json");
                var respuesta = await _httpClient.PostAsync($"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}/capture", contenido);

                respuesta.EnsureSuccessStatusCode(); 

                var datos = await respuesta.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<JsonElement>(datos);

                if (json.GetProperty("status").GetString() == "COMPLETED")
                {
                    return json; 
                }

                throw new Exception("La orden no se completó correctamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al capturar la orden de PayPal: " + ex.Message);
            }
        }
        
        public async Task<JsonElement> ObtenerDetallesOrden(string orderId, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var respuesta = await _httpClient.GetAsync($"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}");
                respuesta.EnsureSuccessStatusCode();

                var datos = await respuesta.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<JsonElement>(datos);

                return json; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los detalles de la orden de PayPal: " + ex.Message);
            }
        }


    }
}
