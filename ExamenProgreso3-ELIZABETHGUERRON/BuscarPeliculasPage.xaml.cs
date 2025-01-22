using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ExamenProgreso3_ELIZABETHGUERRON
{
    public partial class BuscarPeliculasPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public BuscarPeliculasPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnBuscarPeliculaClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Botón presionado");
            try
            {
                string titulo = criterioBusquedaEntry.Text;
                if (string.IsNullOrWhiteSpace(titulo))
                {
                    await DisplayAlert("Error", "Por favor ingrese un título para buscar.", "OK");
                    return;
                }

                // Llama a la API
                Pelicula resultado = await BuscarPeliculaAsync(titulo);
                if (resultado != null)
                {
                    resultadoLabel.Text = $"Título: {resultado.Titulo}\nGénero: {resultado.Genero}\nActor Principal: {resultado.ActorPrincipal}\nPremios: {resultado.Premios}\nSitio Web: {resultado.SitioWeb}";
                }
                else
                {
                    resultadoLabel.Text = "No se encontró ninguna película.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
            }



        }

        private async Task<Pelicula> BuscarPeliculaAsync(string titulo)
        {
            string url = $"https://freetestapi.com/api/v1/movies?search={Uri.EscapeDataString(titulo)}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Respuesta JSON: {json}");
                try
                {
                    return JsonSerializer.Deserialize<Pelicula>(json);
                }
                catch (JsonException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error de deserialización: {ex.Message}");
                }
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Error: {response.StatusCode}, Contenido: {errorContent}");
            }

            return null;
        }
    }
}
