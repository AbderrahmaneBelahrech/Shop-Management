using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Shop_Management
{
    public partial class items : Form
    {
        private Functions Con;
        private int key = 0;

        public items()
        {
            InitializeComponent();
            Con = new Functions();
            ShowItems();
        }

        private void ShowItems()
        {
            try
            {
                string Query = "SELECT ItemTbl.ItCode as ID, ItemTbl.ItName as Item, CategoryTbl.CatName as Category , ItemTbl.Price as Price, ItemTbl.Stock as Stock, ItemTbl.Manufacturer as Manufacturer " +
                               "FROM ItemTbl " +
                               "JOIN CategoryTbl ON ItemTbl.ItCategory = CategoryTbl.CatCode";

                ItemList.DataSource = Con.GetData(Query);
                ItemList.ColumnHeadersHeight = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void items_Load(object sender, EventArgs e)
        {
            Recup_Cat();
        }

       

      

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Item.Text = ItemList.SelectedRows[0].Cells[1].Value.ToString();
            Categorie.Text = ItemList.SelectedRows[0].Cells[2].Value.ToString();
            Price.Text = ItemList.SelectedRows[0].Cells[3].Value.ToString();
            stk.Text = ItemList.SelectedRows[0].Cells[4].Value.ToString();
            manf.Text = ItemList.SelectedRows[0].Cells[5].Value.ToString();

            if (Item.Text == "" || Categorie.Text == "" || Price.Text == "" || stk.Text == "" || manf.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ItemList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        
        private void Recup_Cat()
        {
            string req = "select * from CategoryTbl";
            DataTable dt = Con.GetData(req);

            foreach (DataRow row in dt.Rows)
            {
                Categorie.Items.Add(row["CatName"].ToString());
            }
        }

        private void AddBtn_Click_1(object sender, EventArgs e)
        {
            if (Item.Text==""|| Categorie.Text=="" || Price.Text=="" || stk.Text=="" || manf.Text=="")
            {
                MessageBox.Show("Missing Data !!");
            }
            else
            {
                try
                {
                    // Retrieve CatCode based on the category name
                    string query2 = "SELECT CatCode FROM CategoryTbl WHERE CatName = '" + Categorie.Text + "'";
                    DataTable dt = Con.GetData(query2);
                   /* DataTable dt = Con.GetData(query2);

                    int cattest = Convert.ToInt32(dt.Rows[0][0]); // Initialize cattest to a default value*/

                    int cattest = 0; // Initialize cattest to a default value

                    if (dt.Rows.Count > 0)
                    {
                        cattest = Convert.ToInt32(dt.Rows[0]["CatCode"]);
                        Debug.WriteLine(cattest);
                    }
                    else
                    {
                        Debug.WriteLine("Category not found");
                        // Handle the case where the category is not found
                    }

                    // Use parameters in the query to avoid SQL injection attacks
                    string itemName = Item.Text;
                    int category = cattest;
                    int price = Convert.ToInt32(Price.Text);
                    int stock = Convert.ToInt32(stk.Text);
                    string manufacturer = manf.Text;

                    string query = "INSERT INTO ItemTbl (ItName, ItCategory, Price, Stock, Manufacturer) VALUES ('{0}', {1}, {2}, {3}, '{4}')";
                    query = string.Format(query, itemName, category, price, stock, manufacturer);
                    Con.SetData(query);
                    ShowItems();
                    MessageBox.Show("Item Added !!");

                    // Clear the fields after a successful insertion
                    Item.Text = "";
                    Categorie.Text = "";
                    Price.Text = "";
                    stk.Text = "";
                    manf.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (Item.Text == "" || Categorie.Text == "" || Price.Text == "" || stk.Text == "" || manf.Text == "")
            {
                MessageBox.Show("Data missing !!");
                return;
            }

            try
            {
                string query = "DELETE FROM ItemTbl WHERE ItCode = {0}";
                query = string.Format(query, key);
                Con.SetData(query);
                ShowItems();
                MessageBox.Show("Item Deleted !!");
              //  key = 0; // Reset key after deletion
                Item.Text = "";
                Categorie.Text = "";
                Price.Text = "";
                stk.Text = "";
                manf.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
          
            if (Item.Text == "" || Categorie.Text == "" || Price.Text == "" || stk.Text == "" || manf.Text == "")
            {
                MessageBox.Show("Data missing !!.");
                return;
            }

            try
            {
                // Retrieve CatCode based on the category name
                string query2 = "SELECT CatCode FROM CategoryTbl WHERE CatName = '" + Categorie.Text + "'";
                DataTable dt = Con.GetData(query2);

                int cattest = 0; // Initialize cattest to a default value

                if (dt.Rows.Count > 0)
                {
                    cattest = Convert.ToInt32(dt.Rows[0]["CatCode"]);
                    Debug.WriteLine(cattest);
                }
                else
                {
                    Debug.WriteLine("Category not found");
                }

                try
                {
                    string query = "UPDATE ItemTbl SET ItName = '{0}', ItCategory = {1}, Price = {2}, Stock = {3}, Manufacturer = '{4}' WHERE ItCode = {5}";
                    query = string.Format(query, Item.Text, cattest, Convert.ToInt32(Price.Text), Convert.ToInt32(stk.Text), manf.Text, key);
                    Con.SetData(query);
                    ShowItems();
                    MessageBox.Show("Item Updated !!");
                   // key = 0; // Reset key after update
                    Item.Text = "";
                    Categorie.Text = "";
                    Price.Text = "";
                    stk.Text = "";
                    manf.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
            /*items It = new items();
            It.Show();
            this.Hide();*/
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Categories Cat = new Categories();
            Cat.Show();
            this.Hide();
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
