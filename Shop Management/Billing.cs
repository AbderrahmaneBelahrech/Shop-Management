using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Management
{
    public partial class Billing : Form
    {
        // public static List<string> billing = new List<string>();
        private List<string> billing = new List<string>();
        public Billing()
        {
            InitializeComponent();
            Con = new Functions();
            ShowItems();
            SetupDataGridView();
        }
        Functions Con;
        private void ShowItems()
        {
            string Query = "SELECT ItemTbl.ItCode as ID, ItemTbl.ItName as Item, CategoryTbl.CatName as Category , ItemTbl.Price as Price, ItemTbl.Stock as Stock, ItemTbl.Manufacturer as Manufacturer " +
                               "FROM ItemTbl " +
                               "JOIN CategoryTbl ON ItemTbl.ItCategory = CategoryTbl.CatCode";
            ItemList.DataSource = Con.GetData(Query);
            ItemList.ColumnHeadersHeight = 30;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            items It = new items();
            It.Show();
            this.Hide();
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
            /*Billing Bill = new Billing();
            Bill.Show();
            this.Hide();*/
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            Recup_Cat();
        }
        int key = 0;
        int Stock = 0;
        private void ItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameB.Text = ItemList.SelectedRows[0].Cells[1].Value.ToString();
            Price.Text = ItemList.SelectedRows[0].Cells[3].Value.ToString();
            Stock = Convert.ToInt32(ItemList.SelectedRows[0].Cells[4].Value.ToString());

            if ( NameB.Text == "")
            {
                key = 0;
            }
            else
            {

                key = Convert.ToInt32(ItemList.SelectedRows[0].Cells[0].Value.ToString()); ;
       
            }


        }


       
      
        private void SetupDataGridView()
        {
            ClientBill.ColumnHeadersHeight = 30;

           // ClientBill.Columns.Clear(); 

            ClientBill.Columns.Add("ID", "ID");
            ClientBill.Columns[0].ValueType = typeof(int);

            ClientBill.Columns.Add("Item", "Item");
            ClientBill.Columns[1].ValueType = typeof(string);

            ClientBill.Columns.Add("Price", "Prix");
            ClientBill.Columns[2].ValueType = typeof(string); 

           
            ClientBill.Columns.Add("Quantity", "Qty");
            ClientBill.Columns[3].ValueType = typeof(string); 

           
            ClientBill.Columns.Add("Total", "Total");
            ClientBill.Columns[4].ValueType = typeof(int);
        }

        private void UpdateStock()
        {
          
                try
                {
                int NewStock = Stock - Convert.ToInt32(Quantity.Text);
                    string query = "UPDATE ItemTbl SET Stock = '{0}' WHERE ItCode = {1}";
                    query = string.Format(query, NewStock, key);
                    Con.SetData(query);
                    MessageBox.Show(key.ToString());
                    ShowItems();
                    MessageBox.Show("Stock Updated !!");
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
          
        }
        /* private void Recup_Cat()
         {
             string req = "select * from CategoryTbl";
             DataTable dt = Con.GetData(req);

             foreach (DataRow row in dt.Rows)
             {
                 Billing.billing.Add(row["Name"].ToString());
             }
         }*/

        private void Recup_Cat()
        {
            string req = "select * from CategoryTbl";
            DataTable dt = Con.GetData(req);

            foreach (DataRow row in dt.Rows)
            {
                billing.Add(row["Name"].ToString());
            }
        }
        string PMethod = "";
        int n = 0;
        int GrdTotal = 0;
        private void AddBtn_Click(object sender, EventArgs e)
        {
            string query2 = "SELECT CustCode FROM CustomerTbl WHERE Name = '" + Customer.Text + "'";
            DataTable dt = Con.GetData(query2);

            int cattest = Convert.ToInt32(dt.Rows[0][0]); // Initialize cattest to a default value


            if (dt.Rows.Count > 0)
            {
                cattest = Convert.ToInt32(dt.Rows[0]["CustCode"]);
                Debug.WriteLine(cattest);
            }
            else
            {
                Debug.WriteLine("Category not found");
                // Handle the case where the category is not found
            }
            if (Price.Text == "" || Customer.Text == "" | Quantity.Text == "" | NameB.Text == "")
            {
                MessageBox.Show("Missing Data !!");
            }
            else if (Stock<Convert.ToInt32(Quantity.Text))
            {
                MessageBox.Show("Stock not enough !!");
            }else
            {
                int Qte = Convert.ToInt32(Quantity.Text);
                int total = Convert.ToInt32(Price.Text) * Qte;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ClientBill);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = NameB.Text;
                newRow.Cells[2].Value = Price.Text;
                newRow.Cells[3].Value = Quantity.Text;
                newRow.Cells[4].Value = "Rs" + total;
                ClientBill.Rows.Add(newRow);
                n++;
                GrdTotal += total;
                GrdTotalTbl.Text = "Rs" + GrdTotal;
                UpdateStock();
                ShowItems();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NameB.Text == "" || Customer.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    if (MobileRadio.Checked)
                    {
                        PMethod = "Mobile";
                    }
                    else if (CardRadio.Checked)
                    {
                        PMethod = "Card";
                    }
                    else
                    {
                        PMethod = "Cash";
                    }

                    string query = "INSERT INTO BillingTbl VALUES ('{0}', '{1}', '{2}', '{3}')";
                    query = string.Format(query, DateTime.Today.Date.ToString("yyyy-MM-dd"), Customer.Text, GrdTotal, PMethod);

                    Con.SetData(query);

                    ShowItems();
                    MessageBox.Show("Billing Added !!");
                    StringBuilder billDetails = new StringBuilder();

                    foreach (DataGridViewRow row in ClientBill.Rows)
                    {
                        if (!row.IsNewRow) // Exclure la ligne pour la nouvelle entrée
                        {
                            // Extraction des données de chaque cellule
                            string item = row.Cells["Item"].Value?.ToString() ?? string.Empty;
                            string price = row.Cells["Price"].Value?.ToString() ?? string.Empty;
                            string quantity = row.Cells["Quantity"].Value?.ToString() ?? string.Empty;
                            string total = row.Cells["Total"].Value?.ToString() ?? string.Empty;

                            // Construction d'une chaîne de détails pour la facture
                            billDetails.AppendLine($"Article: {item}, Prix: {price}, Quantité: {quantity}, Total: {total}");
                        }
                    }

                    // Affichage des détails de la facture
                    // Remplacez cette partie par votre logique d'affichage ou d'impression
                    MessageBox.Show(billDetails.ToString(), "Détails de la Facture");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ResetBtn_Click_1(object sender, EventArgs e)
        {
            NameB.Text = "";
            Price.Text = "";
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
