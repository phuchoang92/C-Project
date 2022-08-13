using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhanVienAPI.CreatePDF;
using NhanVienAPI.Data;
using NhanVienAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace NhanVienAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : Controller
    {
        private readonly NhanVienAPIDbContext dbContext;
        public NhanVienController(NhanVienAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetNhanViens()
        {

            return Ok(dbContext.NhanViens.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetNhanVien([FromRoute] string id)
        {
            var nhanVien = dbContext.NhanViens.Find(id);
            if (nhanVien != null)
            {
                return Ok(nhanVien);
            }

            return NotFound();
        }

        /* [HttpPost]
         public IActionResult AddNhanVien(NhanVien nhanVien)
         {
             var nhanVienMoi = new NhanVien()
             {
                 NhanVienId = nhanVien.NhanVienId,
                 TenNhanVien = nhanVien.TenNhanVien,
                 TenDangNhap = nhanVien.TenDangNhap,
                 DonVi = nhanVien.DonVi,
                 PhongBan = nhanVien.PhongBan
             };

             dbContext.NhanViens.Add(nhanVienMoi);
             dbContext.SaveChanges();

             return Ok(nhanVienMoi);
         }*/

        [HttpPost]
        [Produces("application/pdf")]
        public async Task<IActionResult> ReturnPDFFile()
        {

            MemoryStream ms = new MemoryStream();

            string userID = this.User.Claims.ToList()[4].Value;

            var nhanVien = dbContext.NhanViens.FirstOrDefault(nv => nv.TenDangNhap == userID);

            if(nhanVien != null)
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

            return BadRequest();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateNhanVien([FromRoute] string id,[FromBody] NhanVien nNhanVien)
        {
            var nhanVien = dbContext.NhanViens.Find(id);

            if(nhanVien != null)
            {
                nhanVien.TenNhanVien = nNhanVien.TenNhanVien;
                nhanVien.DonVi = nNhanVien.DonVi;
                nhanVien.PhongBan = nNhanVien.PhongBan;

                dbContext.SaveChanges();
                return Ok(nhanVien);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteNhanVien([FromRoute] string id)
        {
            var nhanVien = dbContext.NhanViens.Find(id);
            if (nhanVien != null)
            { 
                dbContext.NhanViens.Remove(nhanVien);
                dbContext.SaveChanges();
                return Ok(nhanVien);
            }

            return NotFound();
        }
    }
}
