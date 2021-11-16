using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UKK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Barang' table. You can move, or remove it, as needed.
            this.barangTableAdapter.Fill(this.appData.Barang);
            barangBindingSource.DataSource = this.appData.Barang;
            btnLogout.Enabled = true;

        }

        

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                    dataGridView1.DataSource = barangBindingSource;
                else
                {
                    var query = from o in this.appData.Barang
                                where o.NamaBarang.Contains(txtSearch.Text) || o.TanggalProduksi == txtSearch.Text || o.TanggalKedaluwarsa == txtSearch.Text || o.Jumlah.Contains(txtSearch.Text)
                                select o;
                    dataGridView1.DataSource = query.ToList();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                txtNama.Focus();
                this.appData.Barang.AddBarangRow(this.appData.Barang.NewBarangRow());
                barangBindingSource.MoveLast();
                btnReset.Enabled = true;
                btnEdit.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtNama.Focus();
            btnReset.Enabled = true;
            btnNew.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNama.Clear();
            txtProduksi.Clear();
            txtKedaluwarsa.Clear();
            txtJumlah.Clear();
            pictureBox1.Image = null;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                barangBindingSource.EndEdit();
                barangTableAdapter.Update(this.appData.Barang);
                panel1.Enabled = false;
                btnEdit.Enabled = true;
                btnNew.Enabled = true;
                btnReset.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have been successfully log out ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                barangBindingSource.RemoveCurrent();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something
            }
        }
    }
}
