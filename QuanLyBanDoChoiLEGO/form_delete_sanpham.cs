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

namespace QuanLyBanDoChoiLEGO
{
    public partial class form_delete_sanpham : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-C7M7T04D\SQLEXPRESS;Initial Catalog=CNPM_NHOM_1;Integrated Security=True");
        //SqlCommand cmd, cmd2;

        public form_delete_sanpham()
        {
            InitializeComponent();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string search_string = textbox_product_id.Text;
            if (string.IsNullOrEmpty(search_string))
            {
                MessageBox.Show("Sản phẩm không tồn tại.");
            }
            else
            {
                CNPM_DataClassesDataContext db = new CNPM_DataClassesDataContext();

                con.Open();
                string query = "delete from STORAGE_HISTORY where id_product = @id";
                string query2 = "delete from PURCHASE_BILL_DETAIL where id_product = @id2";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlCommand cmd2 = new SqlCommand(query2, con);

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(search_string);
                cmd2.Parameters.Add("@id2", SqlDbType.Int).Value = Convert.ToInt32(search_string);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                con.Close();

                IEnumerable<PRODUCT> product = (from products in db.PRODUCTs
                                                where products.id == Convert.ToInt32(search_string)
                                                select products);
                db.PRODUCTs.DeleteAllOnSubmit(product);
                db.SubmitChanges();
                MessageBox.Show("Hoàn thành");
                this.Close();
            }
        }
    }
}
