using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormQuanLyVatTu : Form
    {
        public FormQuanLyVatTu()
        {
            InitializeComponent();
        }

        private void FormQuanLyVatTu_Load(object sender, EventArgs e)
        {
            try
            {
                LiveStockContextDB context = new LiveStockContextDB();
                List<VatTu> listVattu = context.VatTus.ToList();
                BindGrid(listVattu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<VatTu> listVattu)
        {
            dvgVattu.Rows.Clear();
            foreach (var item in listVattu)
            {
                int index = dvgVattu.Rows.Add();
                dvgVattu.Rows[index].Cells[0].Value = item.MaVatTu;
                dvgVattu.Rows[index].Cells[1].Value = item.TenVatTu;
                dvgVattu.Rows[index].Cells[2].Value = item.DonViTinh;
                dvgVattu.Rows[index].Cells[3].Value = item.SoLuong;
                dvgVattu.Rows[index].Cells[4].Value = item.TrangThai;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }
    }
}