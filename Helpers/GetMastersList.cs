using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;

namespace ProyectoMascotas.Helpers
{
    public static class GetMastersList
    {
        public static async Task<List<Categoria>> GetListCategorias()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:52714/api/categorias");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Categoria>>(responseBody);
            return list;
        }

        public static async Task<List<Mascota>> GetListMascotas()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:52714/api/mascotas");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Mascota>>(responseBody);
            return list;
        }
    }
}
