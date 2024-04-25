using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Management
{
    public partial class Categories : Form
    {
        private Functions Con;

        public Categories()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCategories();
            
        }

       
        private void ShowCategories()
        {
            string Query = "SELECT CatCode as ID,CatName as Caterory FROM CategoryTbl";
            CategoriesList.DataSource = Con.GetData(Query);
            CategoriesList.ColumnHeadersHeight = 30;
        }


        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string categoryName = NameTb.Text;
                    string query = "INSERT INTO CategoryTbl VALUES ('{0}')";
                    query = string.Format(query, categoryName);
                    Con.SetData(query);
                    ShowCategories();
                    MessageBox.Show("Category Added !!");
                    NameTb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;
        private void CategoriesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            NameTb.Text = CategoriesList.SelectedRows[0].Cells[1].Value.ToString();
            //IdBox.Text = CategoriesList.SelectedRows[0].Cells[0].Value.ToString();
            if (NameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CategoriesList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string categoryName = NameTb.Text;
                    string query = "Delete from CategoryTbl where CatCode={0}";
                    query = string.Format(query, key);
                    Con.SetData(query);
                    ShowCategories();
                    MessageBox.Show("Category deleted !!");
                    NameTb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    string categoryName = NameTb.Text;
                    string query = "Update CategoryTbl set CatName='{0}' where Catcode ={1}";
                    query = string.Format(query, categoryName,key);
                    Con.SetData(query);
                    ShowCategories();
                    MessageBox.Show("Category updated !!");
                    NameTb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            items It = new items();
            It.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           /* Categories Cat = new Categories();
            Cat.Show();
            this.Hide();*/
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers Cus = new Customers();
            Cus.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billing Bill = new Billing();
            Bill.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void items_Paint(object sender, PaintEventArgs e)
        {


        }

        private void Categoriess_Paint(object sender, PaintEventArgs e)
        {


        }

        private void Customers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Billing_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Logout_Paint(object sender, PaintEventArgs e)
        {


        }

        private void label12_Click(object sender, EventArgs e)
        {
            // Event handler logic goes here
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            // Event handler logic goes here
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Event handler logic goes here
        }
    }
}
