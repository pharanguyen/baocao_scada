using DAO.Models;
using DAO.Models.PhanQuyen;
using ERP.Libs;
using ERPProject.Libs;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPProject.Services
{
    public class AppUser
    {
        private System.Security.Claims.ClaimsPrincipal user;

        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AppUser(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            user = ((CustomAuthenticationStateProvider)_authenticationStateProvider).user;
        }

        public async void refeshUser()
        {
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public ds_thanhvien nhansu
        {
            get
            {
                try
                {
                    if (user == null) return new ds_thanhvien() { thanhvien_id = -1};
                    var strJson = user.FindFirst("User").Value;
                    return System.Text.Json.JsonSerializer.Deserialize<ds_thanhvien>(strJson);
                }
                catch
                {
                    return new ds_thanhvien();
                }

            }
        }
    }
}
