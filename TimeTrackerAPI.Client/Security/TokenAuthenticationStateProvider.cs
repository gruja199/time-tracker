﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TimeTrackerAPI.Client.Models;

namespace TimeTrackerAPI.Client.Security
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public object JasonSerializer { get; private set; }

        public TokenAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync();
            var user = await GetUserAsync();

            var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(GetClaimsFromTokenAndUser(token, user), "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }


        

        public async Task SetTokenAndUserAsync(string token, UserModel user)
        {
            await _jsRuntime.InvokeAsync<object>("blazorLocalStorage.set", "authToken", token);
            await _jsRuntime.InvokeAsync<object>("blazorLocalStorage.set", "user", user);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        }

        private IEnumerable<Claim> GetClaimsFromTokenAndUser(string token, UserModel user)
        {
            var payload = token.Split('.')[1];

            var jasonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Parse<Dictionary<string, object>>(jasonBytes);


            // We need this claim to fill AuthState.User.Identity.Name (to display current user name)

            keyValuePairs.Add("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.Name);


            return keyValuePairs.Select(x => new Claim(x.Key, x.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {

                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;



            }
            return Convert.FromBase64String(base64);
        }

        public async Task<UserModel> GetUserAsync()
        {
           return await _jsRuntime.InvokeAsync<UserModel>("blazorLocalStorage.get", "user");
        }

        public async Task<string> GetTokenAsync()
        {

          return  await _jsRuntime.InvokeAsync<string>("blazorLocalStorage.get", "authToken");
        }
    }
}
