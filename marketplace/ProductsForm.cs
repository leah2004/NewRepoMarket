using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace marketplace
{
    public partial class ProductsForm : Form
    {
        private List<Product> products;
        private List<Product> cart;
/*        private decimal totalCartAmount;*/
        private int totalPrice=0;
        private List<Product> allProducts = new List<Product>();

        public Action<object, EventArgs> ProductControl_AddToCartClicked { get; private set; }

        public ProductsForm()
        {
            InitializeComponent();
            LoadProducts();
          /*  LoadProducts2();*/


        }
     

        private void LoadProducts()
        {
            string[] lines = File.ReadAllLines("products.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6) // Предполагаем, что формат: название, цена, описание
                {
                    var productControl = new ProductControl
                    {
                        ProductNname = parts[1].Trim(),
                        ProductPrice = int.Parse(parts[2].Trim()),
                        ImageName = parts[3].Trim()
                  };

                    productControl.LoadImage();
                    productControl.AddToCartClicked += ProductControl_AddToCart_Click;
                    flowLayoutPanelProducts.Controls.Add(productControl);
                   
                    // Добавляем продукт в общий список
                }
            }
        }
      /*  private void LoadProducts2()
        {
            string[] lines = File.ReadAllLines("products.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6) // Предполагаем, что формат: название, цена, имя изображения
                {
                    var product = new Product
                    {
                        Name = parts[0].Trim(),
                        Price = int.Parse(parts[1].Trim()),
                       *//* ImageName = parts[3].Trim()*//*
                    };

                    allProducts.Add(product); // Добавляем продукт в общий список
                }
            }
        }*/
        private void ProductControl_AddToCart_Click(object sender, EventArgs e)
        {
            var productControl = sender as ProductControl;
            if (productControl != null)
            {
                
                MessageBox.Show($"Товар '{productControl.ProductNname}' добавлен в корзину по цене {productControl.ProductPrice}.");
                listBoxCart.Items.Add($"{productControl.ProductNname} - {productControl.ProductPrice}");
                totalPrice += productControl.ProductPrice;
                labelTotalAmount.Text = $"Общая цена корзины: {totalPrice:F2}";
            }

        }
        private void DisplayProducts()
        {
        }

      /*  private void AddToCart(Product product)
        {
            if (cart == null)
                cart = new List<Product>();

            cart.Add(product);
            totalCartAmount += product.Price;
        }
*/
       /* private void RemoveFromCart(Product product)
        {
            if (cart != null && cart.Contains(product))
            {
                cart.Remove(product);
                totalCartAmount -= product.Price;
            }
        }*/

     /*   private void UpdateCartDisplay()
        {
            listBoxCart.Items.Clear();
            foreach (var item in cart)
            {
                listBoxCart.Items.Add($"{item.Name} - {item.Price:C}");
            }

            labelTotalAmount.Text = $"Общая сумма: {totalCartAmount:C}";
        }*/
      /*  private void ClearCart()
        {
            cart.Clear();
            totalCartAmount = 0;
            UpdateCartDisplay();
        }
*/
        private void ProductsForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadCart()
        {
            // Загрузка товаров из файла корзины
            if (File.Exists("cart.txt"))
            {
                var lines = File.ReadAllLines("cart.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        string productName = parts[0].Trim();
                        int productPrice = int.Parse(parts[1].Trim());
                        listBoxCart.Items.Add($"{productName} - {productPrice:F2}");
                        totalPrice += productPrice;
                    }
                }
                UpdateTotalPriceLabel();
            }
        }

        private void buttonRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (listBoxCart.SelectedIndex != -1)
            {
                int selectedIndex = listBoxCart.SelectedIndex;
             
            }

            if (listBoxCart.SelectedItem != null)
            {
                // Получение выбранного товара
                string selectedItem = listBoxCart.SelectedItem.ToString();
                string[] parts = selectedItem.Split('-');
                if (parts.Length == 2)
                {
                    string productName = parts[0].Trim();
                    int productPrice = int.Parse(parts[1].Trim());

                    // Удаление товара из списка
                    listBoxCart.Items.Remove(selectedItem);
                    totalPrice -= productPrice;
                    UpdateTotalPriceLabel();

                    // Перезапись файла корзины
                    SaveCart();
                }
            }
        }

        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            if (listBoxCart.Items.Count > 0)
            {
                MessageBox.Show($"Спасибо за покупку!");
                /*MessageBox.Show($"Вы купили следующие товары на сумму: {totalPrice:F2}");*/
                listBoxCart.Items.Clear();
                totalPrice = 0;
                UpdateTotalPriceLabel();
                listBoxCart.Items.Clear();
                // Очистка файла корзины
                File.WriteAllText("cart.txt", string.Empty);
            }
            else
            {
                MessageBox.Show("Ваша корзина пуста.");
            }

            
           
        }
        private void UpdateTotalPriceLabel()
        {
            labelTotalAmount.Text = $"Общая цена корзины: {totalPrice:F2}";
        }
        private void SaveCart()
        {
            using (StreamWriter sw = new StreamWriter("cart.txt"))
            {
                foreach (var item in listBoxCart.Items)
                {
                    string[] parts = item.ToString().Split('-');
                    if (parts.Length == 2)
                    {
                        string productName = parts[0].Trim();
                        string productPrice = parts[1].Trim();
                        sw.WriteLine($"{productName},{productPrice}");
                    }
                }
            }
        }
        private void labelTotalAmount_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();
            FilterProducts(searchTerm);
        }
        private void FilterProducts(string searchTerm)
        {
            flowLayoutPanelProducts.Controls.Clear();
            string[] lines = File.ReadAllLines("products.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6) // Предполагаем, что формат: название, цена, описание
                {
                    var productControl = new ProductControl
                    {
                        ProductNname = parts[1].Trim(),
                        ProductPrice = int.Parse(parts[2].Trim()),
                        ImageName = parts[3].Trim()
                    };
                    
                    productControl.LoadImage();
                    if (productControl.ProductNname == txtSearch.Text) {
                        flowLayoutPanelProducts.Controls.Add(productControl);

                    }
                    

                
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanelProducts.Controls.Clear();
            LoadProducts();
        }
    }
}
