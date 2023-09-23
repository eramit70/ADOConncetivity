 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace ADoConnectivity
{
    internal class Program : Property
    {


        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-PM1E9OH\\SQLEXPRESS;Initial Catalog=project1;Integrated Security=True ");
        SqlCommand cmd;
        SqlDataReader dr;

        private void UserInterFace()
        {

            Console.WriteLine("Enter The Product Name : ");
            PName = Console.ReadLine();

            Console.WriteLine("Enter The Product Discription : ");
            Pdescription = Console.ReadLine();

            Console.WriteLine("Enter The Product Price : ");
            PPrice = Convert.ToDecimal(Console.ReadLine());





        }  
        private void Filter()
        {
            Console.WriteLine("Enter the Product Name ");
            PName = Console.ReadLine() ;

            Query = "Select * from Product where pname like '%"+PName+ "%' or Pdescription like '%"+Pdescription+ "%' OR Pprice like '%"+PPrice+"%'";
            cmd  = new SqlCommand(Query, conn); 

            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    Console.WriteLine(dr[0] + "\t" + dr[1] + "\t" + dr[2] + "\t" + dr[3]);

                }
            }
            
            else Console.WriteLine("Record Has Not Found !!");

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        protected void Register()
        {

                try
                {
                  do
                {
                    UserInterFace();

                    Query = "insert into Product(pname, pdiscription, pprice) values('" + PName.Trim() + "','" + Pdescription + "','" + PPrice + "')";
                    cmd = new SqlCommand(Query, conn);

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    int res = (int)cmd.ExecuteNonQuery();

                    Console.WriteLine("Record is Inserted ");
                    if (res != 0)
                    {
                        Console.WriteLine("({0} row(s) affected )", res);

                    }

                    Console.WriteLine(" Press X for Exit \t Press any key Add More Product");
                    ch = Console.ReadLine();
                }
                while (ch.ToLower() != "X");
              


                
                


                }
                
            catch(Exception ex) {
            
                Console.WriteLine(ex.ToString());
            }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                   }
        }

        protected void AllData()
        {
            Query = "Select * from product";

            cmd = new SqlCommand(Query,conn);
            // check connection

            if(conn.State == ConnectionState.Closed) {
                
                
                    conn.Open();
            }
            
           SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {


//                Console.WriteLine(dr["Pid"] +"\t"+dr["pname"] + "\t" + dr["pprice"]);
                Console.WriteLine(dr[0] + "\t" + dr[1] + "\t" + dr[2] + "\t" + dr[3]);

            }
         if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }   
        }

        private void Delete()
        {
            try
            {
                do
                {
                    AllData();

                    Console.WriteLine("----------------------------------------");

                    Console.WriteLine(" Enter the Product Id for delete : ");
                    Pid = Convert.ToInt32(Console.ReadLine());

                    Query = " delete from product where Pid ='" + Pid + "'";

                    cmd = new SqlCommand(Query, conn);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmd.ExecuteNonQuery();

                    if (conn.State == ConnectionState.Open)
                    {

                        conn.Close();
                    }
                    Console.WriteLine("Press X for Exit \t Press Any Key For Delete More Product ");
                    ch = Console.ReadLine();
                 
                }
                while (ch.ToLower() != "x");

            } catch(Exception ex)
            {
                Console.WriteLine("Error is : "+ ex.ToString());
            }
        }

        private void ProcedureInsert()
        {
            UserInterFace();
            Query = " insert into product (pname, pdiscription, pprice) values(@name,@discription,price)";
            cmd = new SqlCommand(Query, conn);  


        }
        static void Main(string[] args)
        {

            Program program = new Program();

            /*            program.UserInterFace();
            */        /*    program.Register();*/
          
            program.Delete();
            Console.WriteLine("----------------------------------------");

           /* program.AllData();*/



            Console.WriteLine("Row Has Deleted");

          /*  Console.WriteLine("Last out ");*/


            Console.ReadLine(); 
        }
    }
}
