using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Text.Json;
using GirisimciBulmaPlatform.Shared.Models;

namespace GirisimciBulmaPlatform.Shared
{
    public class Helper
    {

        private  IJSRuntime _jsRuntime;
        
        public Helper(IJSRuntime jSRuntime) { 
        
            _jsRuntime = jSRuntime;
        }
        public string klasor= Directory.GetCurrentDirectory();

        public async Task<List<int>> GetAcceptedIdeas(Kullanici kullanici)
        {
            return JsonSerializer.Deserialize<List<int>>((await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "acceptedIdeas_" + kullanici.Id)) ?? "[]");
        }

        public async Task AcceptIdea(Kullanici kullanici, int id)
        {
            var ideas = await GetAcceptedIdeas(kullanici);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "acceptedIdeas_" + kullanici.Id, JsonSerializer.Serialize(ideas.Concat(new List<int>() { id }).ToArray()));
        }

        public  async Task SetUser(Kullanici user)
        {
            var userJson = JsonSerializer.Serialize(user);
            if (userJson != null) { ;
                await _jsRuntime.InvokeVoidAsync("blazorHelpers.saveToLocalStorage", "USER", userJson);
            }
           
        }
        public  async Task ClearUser()
        {
            await _jsRuntime.InvokeVoidAsync("blazorHelpers.removeFromLocalStorage", "USER");
        }
        public  async Task<Kullanici> GetUser()
        {
            if (_jsRuntime!=null)
            {
                var userJson = await _jsRuntime.InvokeAsync<string>("blazorHelpers.getFromLocalStorage", "USER");
                if (userJson != null)
                {
                    var user = JsonSerializer.Deserialize<Kullanici>(userJson);
                    return user;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
           
        }
    }
}
