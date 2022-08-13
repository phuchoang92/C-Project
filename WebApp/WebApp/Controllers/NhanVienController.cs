using NhanVienAPI.CreatePDF;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http.Controllers;
using WebApp.DataConnect;
using WebApp.AuthManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Controllers
{
    [Route("NhanVien")]
    public class NhanVienController : Controller
    {
        private readonly NhanVienDBContext dbContext;
        
        public NhanVienController(NhanVienDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Report")]
        public IActionResult Report()
        {
          
            string token = HttpContext.Request.Cookies["access_token"] == null ? "": HttpContext.Request.Cookies["access_token"].ToString(); 
            if (token != "") {
                MemoryStream ms = new MemoryStream();

                var userName = TokenManipulate.ValidateJWToken(token);

                var nhanVien = dbContext.NhanViens.FirstOrDefault(nv => nv.TenDangNhap == userName);

                if (nhanVien != null)
                {
                    CreateWatermarkPDF createWatermarkPDF = new CreateWatermarkPDF();
                    createWatermarkPDF.CreatePDFFile(ms, nhanVien);

                    byte[] byteInfo = ms.ToArray();
                    ms.Write(byteInfo, 0, byteInfo.Length);
                    ms.Position = 0;

                    FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
                    fileStreamResult.FileDownloadName = "Output.pdf";

                    return fileStreamResult;
                }
            }
            return Unauthorized();
        }
    }
}
