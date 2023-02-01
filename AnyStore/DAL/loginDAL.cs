using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class loginDAL
    {
        //სტატიკური სტრინგი ბაზასთან დასაკავშირებლად
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
            // გავაკეთოთ ბულენ ცვლადი მივანიჭოთ მნიშვნელობა და დავაბრინოთ
            bool isSuccess = false;

            //ბაზასთან კავშირი
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // სქლ ქვერი ლოგინის შესამოწმებლად
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                // სქლის ბრძანება მნიშვნელობის გადასაცემად
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //რიგების შემოწმება ცხრილში 
                if(dt.Rows.Count>0)
                {
                    // წარამტებული დალოგინება
                    isSuccess = true;
                }
                else
                {
                    //აპლიკაციაში უშედეგო შესვლა
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
