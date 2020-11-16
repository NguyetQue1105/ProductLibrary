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
    public partial class frmSearchDetail : Form
    {
        public frmSearchDetail()
        {
            InitializeComponent();
        }
        public Product Product { get; set; }
        public frmSearchDetail(Product p): this() 
        {
            Product = p;
            txtProductID.Text = Product.ProductID.ToString();
            txtProductName.Text = Product.ProductName;
            txtUnitPrice.Text = Product.UnitPrice.ToString();
            txtQuantity.Text = Product.Quantity.ToString();
        }
    }
}
