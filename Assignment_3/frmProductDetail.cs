using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    public partial class frmProductDetail : Form
    {
        public frmProductDetail()
        {
            InitializeComponent();
        }
        private bool addOrEdit; 

        public Product Product { get; set; }
        
        public frmProductDetail (bool flag, Product p) : this()
        {
            addOrEdit = flag;
            Product = p;
            InitData();
        }
        private void InitData()
        {
            txtProductID.Text = Product.ProductID.ToString();
            txtProductName.Text = Product.ProductName;
            txtUnitPrice.Text = Product.UnitPrice.ToString();
            txtQuantity.Text = Product.Quantity.ToString();
        }
        private bool checkData()
        {
            bool result = false;
            if (txtProductID.Text.Equals("") || txtProductName.Text.Equals("") || txtQuantity.Text.Equals("") || txtUnitPrice.Text.Equals(""))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            Product.ProductID = int.Parse(txtProductID.Text);
            Product.ProductName = txtProductName.Text;
            Product.UnitPrice = float.Parse(txtUnitPrice.Text);
            Product.Quantity = int.Parse(txtQuantity.Text);
            ProductDB dB = new ProductDB();
            if (addOrEdit == true)
            {
                flag = dB.AddNewProduct(Product);
            }
            else if (addOrEdit == false)
            {
                flag = dB.UpdateProduct(Product);
            }
            if (flag == false)
            {
                MessageBox.Show("Save fail");
            }
            else
            {
                MessageBox.Show("Save Successful.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
