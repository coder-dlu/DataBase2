﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CSDL.HeThongGiaoDien;

namespace CSDL
{
    public partial class TraSach : Form
    {
        SqlConnection cnn;
        public TraSach()
        {
            InitializeComponent();
            cnn = new SqlConnection("Data Source=MyLaptop;Initial Catalog=QuanLyNhaSach;Integrated Security=True");
        }
        //tao ma Phieu Tra ngau nhien
        private string taomaPT()
        {
            string maPT;
            Random r = new Random();
            maPT = "NV" + r.Next(50, 999).ToString();
            return maPT;
        }
        //doc du lieu len file
        private DataTable docdulieu()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_RELOADPHIEUTRASACH";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            DataTable TraSach = new DataTable();
            cnn.Open();
            TraSach.Load(cmd.ExecuteReader());
            cnn.Close();
            return TraSach;
        }
        //load nhan vien len luoi
        private void loadnvlenluoi()
        {
            dgvSachDaTra.DataSource = docdulieu();
        }
        //Hien Tieu De Cot Da Tra Sach
        private void HientieudecotDaTra()
        {
            dgvSachDaTra.Columns[0].HeaderText = "Mã PT";
            dgvSachDaTra.Columns[1].HeaderText = "Mã ĐG";
            dgvSachDaTra.Columns[2].HeaderText = "Mã Sách";
            dgvSachDaTra.Columns[3].HeaderText = "Số Lượng";
            dgvSachDaTra.Columns[4].HeaderText = "Đơn giá phạt";
            dgvSachDaTra.Columns[5].HeaderText = "Ngày Trả";
            dgvSachDaTra.Columns[6].HeaderText = "Cần Thanh Toán";
        }
        //Load Trả Sách
        private void TraSach_Load(object sender, EventArgs e)
        {
            txtMaPT.Enabled = false;
            loadnvlenluoi();
            data_bingding();
            HientieudecotDaTra();
            
        }
        #region xử lý bingding
        private void data_bingding()
        {
            txtMaPT.DataBindings.Add("Text", dgvSachDaTra.DataSource, "MaPT");
            txtMaDG.DataBindings.Add("Text", dgvSachDaTra.DataSource, "MaDG");
            txtMaSach.DataBindings.Add("Text", dgvSachDaTra.DataSource, "MaSach");
            NbDonGiaPhat.DataBindings.Add("Text", dgvSachDaTra.DataSource, "DonGiaPhat");
            NbSoLuong.DataBindings.Add("Text", dgvSachDaTra.DataSource, "SoLuong");
            NbCanThanhToan.DataBindings.Add("Text", dgvSachDaTra.DataSource, "CanThanhToan");
        }
        private void Huy_bingding()
        {
            if (txtMaPT.DataBindings != null)
                txtMaPT.DataBindings.Clear();
            if (txtMaDG.DataBindings != null)
                txtMaDG.DataBindings.Clear();
            if (txtMaSach.DataBindings != null)
                txtMaSach.DataBindings.Clear();
            if (NbDonGiaPhat.DataBindings != null)
                NbDonGiaPhat.DataBindings.Clear();
            if (NbSoLuong.DataBindings != null)
                NbSoLuong.DataBindings.Clear();
            if (dtpNgayMuon.DataBindings != null)
                dtpNgayMuon.DataBindings.Clear();
            if (NbCanThanhToan.DataBindings != null)
                NbCanThanhToan.DataBindings.Clear();
        }
        #endregion
       
        //xu ly nut thanh toan
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            PhieuThuTien f = new PhieuThuTien();
            f.ShowDialog();
        }
       
        //================================Muốn Trả=============================
        //Hien Tieu De Cot Da Tra Sach
        

    }
}
