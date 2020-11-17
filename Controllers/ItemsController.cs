using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using ProyectoMascotas.Helpers;
using System.Text;
using System;

namespace ProyectoMascotas.Controllers
{
    public class ItemsController : Controller
    {
        private readonly string url = "http://localhost:52714/api/items/";

        // GET: Items
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Items>>(responseBody);
            return View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Items>(responseBody);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            Items item = new Items();
            item.listCategorias = await GetMastersList.GetListCategorias();
            item.listMascotas = await GetMastersList.GetListMascotas();
            return View(item);
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Items item)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, stringContent);
                string objResult = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }
            item.listCategorias = await GetMastersList.GetListCategorias();
            item.listMascotas = await GetMastersList.GetListMascotas();
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Items>(responseBody);
            if (item == null)
            {
                return NotFound();
            }
            item.listCategorias = await GetMastersList.GetListCategorias();
            item.listMascotas = await GetMastersList.GetListMascotas();
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Items item)
        {
            if (id != item.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(url + id, stringContent);
                }
                catch (Exception ex)
                {
                    ViewBag.errorEdit = "Ocurrio un error al actualizar el item";
                    return View(item);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Items>(responseBody);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(url + id);
            return Ok();
        }
    }
}
