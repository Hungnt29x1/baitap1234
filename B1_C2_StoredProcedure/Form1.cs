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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace B1_C2_StoredProcedure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập hoặc Mật khẩu để trống. Đề nghị nhập lại.");
            }
            else
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=baiTap1;Integrated Security=True");

                try
                {
                    connection.Open();

                    // Tạo command để gọi stored procedure
                    SqlCommand command = new SqlCommand("CheckLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số vào command
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Thực thi command và lấy kết quả trả về
                    int result = (int)command.ExecuteScalar();

                    if (result == 1)
                    {
                        MessageBox.Show($"Xin chào, {txtUsername.Text}");
                    }
                    else
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            
        }
    }
}
