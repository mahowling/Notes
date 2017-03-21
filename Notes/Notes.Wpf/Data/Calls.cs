using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Wpf.Data
{
    internal class Calls : IDataCalls
    {

        /// <summary>
        /// Returns the Base URL for the Notes API
        /// </summary>
        /// <returns></returns>
        private Uri ApiBaseAddress()
        {
            return new Uri(Properties.Settings.Default.ApiUrl);
        }

        public async Task<bool> DeleteNoteAsync(INote note)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.DeleteAsync($"/api/note/{note.Id}");
                    response.EnsureSuccessStatusCode();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns all the availble notes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<INote>> GetAllNotesAsync()
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.GetAsync("/api/note");
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<IEnumerable<Note>>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient()
            {

                BaseAddress = ApiBaseAddress()
            };

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<INote> CreateNoteAsync(INote note)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PostAsJsonAsync("/api/note/", note);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<Note>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateNoteAsync(INote note)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PutAsJsonAsync($"/api/note/{note.Id}", note);
                    response.EnsureSuccessStatusCode();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

