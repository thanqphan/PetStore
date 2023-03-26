﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PetStore.Models;

namespace PetStore.Controllers
{
    public class NguoiDungController : Controller
    {
        private DataContext data = new DataContext();

        // GET: NguoiDung
        // GET: NguoiDung/Details/5
        //[HttpGet]
        //public ActionResult Dangky()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        //{
        //    var hoten = collection["TenKH"];
        //    var tendn = collection["TenDangNhap"];
        //    var matkhau = collection["MatKhau"];
        //    var nhaplaimatkhau = collection["NhaplaiMatkhau"];
        //    var email = collection["Email"];
        //    var dienthoai = collection["SĐT"];
        //    var diachi = collection["DiaChi"];

        //    var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
        //    KHACHHANG tempt = data.KHACHHANGs.SingleOrDefault(n => n.TenDangNhap.Trim() == tendn.Trim());

        //    if (tempt != null)
        //    {
        //        ViewData["Loi2"] = "Username đã tồn tại";
        //    }
        //    if (matkhau != nhaplaimatkhau)
        //    {
        //        ViewData["Loi1"] = "Nhập lại mật khẩu không đúng";
        //    }
        //    else
        //    {
        //        //so sánh ngày hiện tại và ngày sinh
        //        if (DateTime.Compare(DateTime.Now, DateTime.Parse(collection["NgaySinh"])) == -1)
        //        {
        //            ViewData["Loi3"] = "Ngày sinh không được lớn hơn hiện tại";

        //        }
        //        kh.TenKH = hoten;
        //        kh.TenDangNhap = tendn;
        //        kh.MatKhau = matkhau;               
        //        kh.Email = email;
        //        kh.SĐT = dienthoai;
        //        kh.DiaChi = diachi;
        //        kh.NgaySinh = DateTime.Parse(ngaysinh);

        //        data.KHACHHANGs.AddOrUpdate(kh);
        //        data.SaveChanges();
        //        return RedirectToAction("Dangnhap", "Nguoidung");
        //    }
        //    return this.Dangky();
        //}
        [HttpGet]
        // GET: NguoiDung
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["TenKH"];
            var tendn = collection["TenDangNhap"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["NhaplaiMatkhau"];
            var email = collection["Email"];
            var dienthoai = collection["SĐT"];
            var diachi = collection["DiaChi"];

            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            KHACHHANG tempt = data.KHACHHANG.SingleOrDefault(n => n.TenDangNhap.Trim() == tendn.Trim());
            Regex regexMail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match matchMail = regexMail.Match(email);
            Regex regexPhone = new Regex(@"^(84|0[3|5|7|8|9])+([0-9]{8})\b");
            Match matchPhone = regexPhone.Match(dienthoai);
            var checkEmail = data.KHACHHANG.FirstOrDefault(x => x.Email == email);
            if (checkEmail != null)
            {
                ViewData["Loiemail"] = "email đã tồn tại, vui long nha email khac";
                return this.DangKy();
            }
            if (tempt != null)
            {
                ViewData["Loi2"] = "Username đã tồn tại";
            }
            if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["NhapMKXN"] = "phải nhập mật khẩu xác nhận";
            }
            //so sánh ngày hiện tại và ngày sinh
            if (dienthoai.LongCount() < 10 || dienthoai.LongCount() > 10)
            {
                ViewData["loi4"] = "Sdt phải có 10 số";
            }
            else if (dienthoai == kh.SĐT)
            {
                ViewData["loi5"] = "SDT đã có người sử dụng";
            }
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(collection["Ngaysinh"])) == -1)
            {
                ViewData["Loi3"] = "Ngày sinh không được lớn hơn hiện tại";

            }
            else
            {
                if (!matkhau.Equals(nhaplaimatkhau))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";

                }
                else
                {
                    kh.TenKH = hoten;
                    kh.TenDangNhap = tendn;
                    kh.MatKhau = matkhau;
                    kh.Email = email;
                    kh.SĐT = dienthoai;
                    kh.DiaChi = diachi;
                    kh.NgaySinh = DateTime.Parse(ngaysinh);

                    data.KHACHHANG.Add(kh);
                    data.SaveChanges();

                    return RedirectToAction("Index");

                }
            }
            return this.DangKy();
        }
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDangNhap"];
            var mk = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(mk))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANG.SingleOrDefault(n => n.TenDangNhap.Trim() == tendn.Trim() && n.MatKhau.Trim() == mk.Trim());
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    Session["Taikhoandn"] = kh.TenKH;

                    return RedirectToAction("GioHang", "Giohang");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Logout()
        {

            Session["Taikhoan"] = null;
            Session["Taikhoandn"] = null;

            return RedirectToAction("Index", "ShopSecondHand");
        }
    }
}
