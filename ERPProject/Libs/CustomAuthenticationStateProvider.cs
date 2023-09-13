using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using DAO.Services.PhanQuyen;
using DAO.Models.PhanQuyen;
using DAO.Models.CommonModels;


namespace ERP.Libs
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public System.Security.Claims.ClaimsPrincipal user;
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorageService.ContainKeyAsync("Token"))
            {
                var accessKey = await _localStorageService.GetItemAsync<string>("Token");
                accessKey = MsUtils.Encryption.DecryptString(accessKey);
                var strInfo = accessKey.Split("@MS@");
                if (strInfo.Length != 5) return new AuthenticationState(new ClaimsPrincipal());
                //check thời gian
                if (Convert.ToBoolean(strInfo[3]) == false)
                {
                    var ticks = Convert.ToInt64(strInfo[4]);
                    if ((DateTime.UtcNow.AddHours(7) - new DateTime(ticks)).TotalSeconds >= 30 )
                        return new AuthenticationState(new ClaimsPrincipal());
                }
                var rsModel = new ResultModel<DAO.Models.PhanQuyen.ds_thanhvien>();
                await Task.Run(() => { rsModel = ds_thanhvienService.GetByUserName(strInfo[1], strInfo[2]); });
                if (!rsModel.isThanhCong) return new AuthenticationState(new ClaimsPrincipal());
                //chuyển thông tin user về string json
                var strClaim = System.Text.Json.JsonSerializer.Serialize(rsModel.Data);
                var claims = new[] { new Claim("User", strClaim) };
                var identity = new ClaimsIdentity(claims, "BearerToken");
                user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
