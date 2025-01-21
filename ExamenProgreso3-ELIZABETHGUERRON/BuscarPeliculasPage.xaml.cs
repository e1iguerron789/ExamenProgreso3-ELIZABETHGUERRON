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

        private async void OnBuscarClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(criterioBusquedaEntry.Text))
                {
                    await DisplayAlert("Error", "Por favor, ingrese el título de la película.", "OK");
                    return;
                }

                string titulo = criterioBusquedaEntry.Text;
                var pelicula = await BuscarPeliculaAsync(titulo);

                if (pelicula != null)
                {
                    resultadoLabel.Text = $"Título: {pelicula.Titulo}\n" +
                                          $"Género: {pelicula.Genero}\n" +
                                          $"Actor Principal: {pelicula.ActorPrincipal}\n" +
                                          $"Premios: {pelicula.Premios}\n" +
                                          $"Sitio Web: {pelicula.SitioWeb}";
                }
                else
                {
                    resultadoLabel.Text = "No se encontró ninguna película con ese título.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async Task<Pelicula> BuscarPeliculaAsync(string titulo)
        {
            string url = $"https://api.ejemplo.com/peliculas?titulo={Uri.EscapeDataString(titulo)}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Pelicula>(json);
            }

            return null;
        }
    }

   
}