using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        ProductDB db = new ProductDB();
        List<Product> dtProduct;

        private void btnNew_Click(object sender, EventArgs e)
        {
            int ID = 1;
            string Name = string.Empty;
            float Price = 0;
            int Quantity = 0;
            if (dtProduct.Count > 0)
            {
                ID = dtProduct.Count+1;
            }
            Product pro = new Product { ProductID = ID, ProductName = Name, Quantity = Quantity, UnitPrice = Price };
            frmProductDetail productDetail = new frmProductDetail(true, pro);
            DialogResult r = productDetail.ShowDialog();
            if (r == DialogResult.OK)
            {
                pro = productDetail.Product;
                dtProduct.Add(new Product(pro.ProductID, pro.ProductName, pro.UnitPrice, pro.Quantity));
            }
            LoadData();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void LoadData()
        {
            dtProduct = db.GetProducts();
            bsProduct.DataSource = dtProduct;

            txtProductID.DataBindings.Clear();
            txtProductName.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtUnitPrice.DataBindings.Clear();

            txtProductID.DataBindings.Add("Text", bsProduct, "ProductID");
            txtProductName.DataBindings.Add("Text", bsProduct, "ProductName");
            txtQuantity.DataBindings.Add("Text", bsProduct, "Quantity");
            txtUnitPrice.DataBindings.Add("Text", bsProduct, "UnitPrice");

            dgvProduct.DataSource = bsProduct;
            bsProduct.Sort = "ProductID DESC";
            bnProductList.BindingSource = bsProduct;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtProductID.Text);
            string name = txtProductName.Text;
            float price = float.Parse(txtUnitPrice.Text);
            int quantity = int.Parse(txtQuantity.Text);

            Product pro = new Product { ProductID = ID, ProductName = name, UnitPrice = price, Quantity = quantity };
            frmProductDetail productDetail = new frmProductDetail(false, pro);
            DialogResult r = productDetail.ShowDialog();
            if (r == DialogResult.OK)
            {
                db.UpdateProduct(pro);
                LoadData();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtProductID.Text);
            Product p = db.FindProduct(ID);
            if (db.RemoveProduct(p))
            {
                MessageBox.Show("Successful.");
                LoadData();
            }
            else
            {
                MessageBox.Show("Fail.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtSearch.Text);
            Product p = db.FindProduct(ID);
            if (p != null)
            {
                frmSearchDetail frmSearch = new frmSearchDetail(p);
                frmSearch.Show();
            }
            else
            {
                MessageBox.Show("Product not found");
            }
        }
    }
}
