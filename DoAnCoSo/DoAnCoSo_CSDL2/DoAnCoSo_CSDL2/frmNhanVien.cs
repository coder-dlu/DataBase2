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

namespace DoAnCoSo_CSDL2
{
    public partial class frmNhanVien : Form
    {
        string A;
        SqlConnection cnn;
        public frmNhanVien()
        {
            InitializeComponent();
            cnn = new SqlConnection("Data Source=MyLaptop;Initial Catalog=QuanLyThuVienSachCaNhan;Integrated Security=True");

        }
        //dinh nghia cac ham su dung
        //tao ma nhan vien ngau nhien
        private string taomanv()
        {
            string manv;
            Random r = new Random();
            manv = "NV" + r.Next(50, 999).ToString();
            return manv;
        }
        //doc du lieu len file
        private DataTable docdulieu()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_LOADNHANVIEN";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            DataTable nhanvien = new DataTable();
            cnn.Open();
            nhanvien.Load(cmd.ExecuteReader());
            cnn.Close();
            return nhanvien;
        }
        //load nhan vien len luoi
        private void loadnvlenluoi()
        {
            dgvttnv.DataSource = docdulieu();
        }
        // khai bao hien tieu de bang du lieu nhan vien
        private void Hientieudecot()
        {
            dgvttnv.Columns[0].HeaderText = "Mã SV";
            dgvttnv.Columns[1].HeaderText = "Tên SV";
            dgvttnv.Columns[2].HeaderText = "Ngày Sinh";
            dgvttnv.Columns[3].HeaderText = "Ngày Vào Làm";
            dgvttnv.Columns[4].HeaderText = "Giới Tính";
            dgvttnv.Columns[5].HeaderText = "Chức Vụ";
            dgvttnv.Columns[6].HeaderText = "Địa Chỉ";
            dgvttnv.Columns[7].HeaderText = "ĐiệnThoại";
        }
        //xu ly su kien load from
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            txtManv.Enabled = false;
            loadnvlenluoi();
            A = label9.Text;
            label9.Text = "";
            timer1.Start();
            data_bingding();
            Hientieudecot();
        }
        #region xử lý bingding
        private void data_bingding()
        {
            txtManv.DataBindings.Add("Text", dgvttnv.DataSource, "MaNV");
            txtTennv.DataBindings.Add("Text", dgvttnv.DataSource, "TenNV");
            cboGioitinhnv.DataBindings.Add("Text", dgvttnv.DataSource, "Gioitinh");
            txtchucvunv.DataBindings.Add("Text", dgvttnv.DataSource, "ChucVuNV");
            txtDiachinv.DataBindings.Add("Text", dgvttnv.DataSource, "DiaChiNV");
            txtDienthoainv.DataBindings.Add("Text", dgvttnv.DataSource, "DienThoai");
        }
        private void Huy_bingding()
        {
            if (txtManv.DataBindings != null)
                txtManv.DataBindings.Clear();
            if (txtTennv.DataBindings != null)
                txtTennv.DataBindings.Clear();
            if (txtDiachinv.DataBindings != null)
                txtDiachinv.DataBindings.Clear();
            if (txtchucvunv.DataBindings != null)
                txtchucvunv.DataBindings.Clear();
            if (dtpNgaysinhnv.DataBindings != null)
                dtpNgaysinhnv.DataBindings.Clear();
            if (dtngayvaolam.DataBindings != null)
                dtngayvaolam.DataBindings.Clear();
            if (txtDienthoainv.DataBindings != null)
                txtDienthoainv.DataBindings.Clear();
            if (cboGioitinhnv.DataBindings != null)
                cboGioitinhnv.DataBindings.Clear();

        }
        #endregion
        //xu ly su kien cac nut
        private void btnthemnv_Click(object sender, EventArgs e)
        {
            txtManv.Clear();
            txtTennv.Clear();
            txtDiachinv.Clear();
            txtDienthoainv.Clear();
            txtchucvunv.Clear();
            txtManv.Text = taomanv();
        }
        //dinh nghia ham xu ly luu nhan vien vao danh sach
        private void luunhanvien()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_LUUNHANVIEN";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            string manv, tennv, chucvunv, diachi, dienthoai;
            bool gioitinh;
            if (txtTennv.Text == "")
            {
                MessageBox.Show("Tên kg được trống!");
                txtTennv.Focus();
                return;
            }
            if (txtchucvunv.Text == "")
            {
                MessageBox.Show("chức vụ kg duoc trong!");
                txtchucvunv.Focus();
                return;
            }
            if (txtDiachinv.Text == "")
            {
                MessageBox.Show("Địa chỉ kg được trống!");
                txtDiachinv.Focus();
                return;
            }
            if (txtDienthoainv.Text.Length < 0 || txtDienthoainv.Text.Length > 12)
            {
                MessageBox.Show("Điện thoại kg hợp lệ!");
                txtDienthoainv.Focus();
                return;
            }
            DateTime ngaysinh, ngayvl;
            if (DateTime.Now.Year - dtpNgaysinhnv.Value.Year < 18 || DateTime.Now.Year - dtpNgaysinhnv.Value.Year > 85)
            {
                {
                    MessageBox.Show("Tuoi >=18 va <=85!");
                    dtpNgaysinhnv.Focus();
                    return;
                }
            }
            manv = txtManv.Text;
            tennv = txtTennv.Text;
            ngaysinh = DateTime.Parse(dtpNgaysinhnv.Value.ToString());
            ngayvl = DateTime.Parse(dtngayvaolam.Value.ToString());
            if (cboGioitinhnv.Text == "Nam")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            chucvunv = txtchucvunv.Text;
            diachi = txtDiachinv.Text;
            dienthoai = txtDienthoainv.Text;
            cmd.Parameters.AddWithValue("@MaNV", manv);
            cmd.Parameters.AddWithValue("@TenNV", tennv);
            cmd.Parameters.AddWithValue("@NgaySinh", ngaysinh);
            cmd.Parameters.AddWithValue("@Ngayvl", ngayvl);
            cmd.Parameters.AddWithValue("@Gioitinh", gioitinh);
            cmd.Parameters.AddWithValue("@ChucVuNV", chucvunv);
            cmd.Parameters.AddWithValue("@DiaChiNV", diachi);
            cmd.Parameters.AddWithValue("@DienThoai", dienthoai);
            try
            {
                cmd.Parameters.Add("@kq",
                SqlDbType.Int).Direction =
                    ParameterDirection.ReturnValue;
                cnn.Open();
                cmd.ExecuteNonQuery();
                int kq = (int)cmd.Parameters["@kq"].Value;
                if (kq == 1)
                {
                    lblthongbaonv.ForeColor = Color.Red;
                    lblthongbaonv.Text = "Mã Trùng";
                    return;
                }
                else
                {
                    lblthongbaonv.ForeColor = Color.Red;
                    lblthongbaonv.Text = "Lưu Thành Công";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi kg them duoc vi" + ex.Message);
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }
        }
        //xu ly su kien click luu nhan vien
        private void btnluunv_Click(object sender, EventArgs e)
        {
            Huy_bingding();
            luunhanvien();
            loadnvlenluoi();
            data_bingding();
        }
        //dinh nghia ham xu ly xoa nhan viên
        private void XoaNhanVien()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_XOANHANVIEN";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            string manv;
            manv = txtManv.Text;
            cmd.Parameters.Add("@MaNV", manv);
            DialogResult kq1;
            kq1 = MessageBox.Show("Bạn Thật Sự Muốn Xóa", "Chú Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq1 == DialogResult.Yes)
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            try
            {
                cmd.Parameters.Add("@kq",
                SqlDbType.Int).Direction =
                    ParameterDirection.ReturnValue;
                cnn.Open();
                cmd.ExecuteNonQuery();
                int kq = (int)cmd.Parameters["@kq"].Value;
                if (kq == 1)
                {
                    lblthongbaonv.ForeColor = Color.Red;
                    lblthongbaonv.Text = "Mã đã tồn tại trong Phiếu nhắc trả";
                    return;
                }
                else
                {
                    
                    lblthongbaonv.ForeColor = Color.Red;
                    lblthongbaonv.Text = "Xóa Thành Công";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi kg them duoc vi" + ex.Message);
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }
        }
        //xu ly su kien click nut xoa nv
        private void btnxoanv_Click(object sender, EventArgs e)
        {
            Huy_bingding();
            XoaNhanVien();
            loadnvlenluoi();
            data_bingding();
        }
        //dinh nghia ham xu ly sua nhan viên
        private void suanv()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SUANHANVIEN";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            string manv, tennv, chucvunv, diachi, dienthoai;
            bool gioitinh;
            if (txtTennv.Text == "")
            {
                MessageBox.Show("Tên kg được trống!");
                txtTennv.Focus();
                return;
            }
            if (txtchucvunv.Text == "")
            {
                MessageBox.Show("chức vụ kg duoc trong!");
                txtchucvunv.Focus();
                return;
            }
            if (txtDiachinv.Text == "")
            {
                MessageBox.Show("Địa chỉ kg được trống!");
                txtDiachinv.Focus();
                return;
            }
            if (txtDienthoainv.Text.Length < 0 || txtDienthoainv.Text.Length > 12)
            {
                MessageBox.Show("Điện thoại kg hợp lệ!");
                txtDienthoainv.Focus();
                return;
            }
            DateTime ngaysinh, ngayvl;
            if (DateTime.Now.Year - dtpNgaysinhnv.Value.Year < 18 || DateTime.Now.Year - dtpNgaysinhnv.Value.Year > 85)
            {
                {
                    MessageBox.Show("Tuoi >=18 va <=85!");
                    dtpNgaysinhnv.Focus();
                    return;
                }
            }
            manv = txtManv.Text;
            tennv = txtTennv.Text;
            ngaysinh = DateTime.Parse(dtpNgaysinhnv.Value.ToString());
            ngayvl = DateTime.Parse(dtngayvaolam.Value.ToString());
            if (cboGioitinhnv.Text == "Nam")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            chucvunv = txtchucvunv.Text;
            diachi = txtDiachinv.Text;
            dienthoai = txtDienthoainv.Text;
            cmd.Parameters.AddWithValue("@MaNV", manv);
            cmd.Parameters.AddWithValue("@TenNV", tennv);
            cmd.Parameters.AddWithValue("@NgaySinh", ngaysinh);
            cmd.Parameters.AddWithValue("@Ngayvl", ngayvl);
            cmd.Parameters.AddWithValue("@Gioitinh", gioitinh);
            cmd.Parameters.AddWithValue("@ChucVuNV", chucvunv);
            cmd.Parameters.AddWithValue("@DiaChiNV", diachi);
            cmd.Parameters.AddWithValue("@DienThoai", dienthoai);
            try
            {
                cmd.Parameters.Add("@kq",
                SqlDbType.Int).Direction =
                    ParameterDirection.ReturnValue;
                cnn.Open();
                cmd.ExecuteNonQuery();
                int kq = (int)cmd.Parameters["@kq"].Value;
                if (kq == 1)
                {
                    lblthongbaonv.ForeColor = Color.Red;
                    lblthongbaonv.Text = "Mã không tồn tại";
                    return;
                }
                else
                {
                    lblthongbaonv.ForeColor = Color.Red;
                    lblthongbaonv.Text = "Sửa Thành Công";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi kg them duoc vi" + ex.Message);
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }
        }
        //goi ham xy ly nut sua nhan vien
        private void btnsuanv_Click(object sender, EventArgs e)
        {
            Huy_bingding();
            suanv();
            loadnvlenluoi();
            data_bingding();
        }
        //xu ly nut thoat
        private void btnthoatnv_Click(object sender, EventArgs e)
        {
            DialogResult ketqua;
            ketqua = MessageBox.Show("Bạn Có  Muốn Thoát Không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ketqua == DialogResult.Yes)
            {
                this.Close();
            }
        }
        //thoi gian chay chu
        private void timer1_Tick(object sender, EventArgs e)
        {
            int d = 0, x;
            x = A.Length;
            d++;
            string a = A.Substring(0, 1);
            A = A.Substring(1, A.Length - 1);
            label9.Text = label9.Text + a;
            if (d == x)
            {
                timer1.Stop();
            }
        }
    }
}
