﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace 登录
{
    public partial class form_longin : Form
    {
        public form_longin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateValidateNumber();
        }

        private void BtnReset_Click(object sender, EventArgs e)//重置
        {
            Clear();
        }

        private void BtnLogin_Click(object sender, EventArgs e)//登陆
        {
            string userID = this.tbUser.Text.Trim();
            string pass = this.tbPassword.Text.Trim();
            string ipyanzheng=this.tbYangzhengip.Text.Trim();
            int role= IsValidUser(userID, pass,ipyanzheng);
            if (role != 0)
            {
                switch (role)
                {
                    case 1: MessageBox.Show("1");
                        break;
                    case 2: MessageBox.Show("2");
                        break;
                    case 3: MessageBox.Show("3");
                        break;
                    case 4: MessageBox.Show("4");
                        break;

                }

            }
            else
                Clear();

        }
        private int IsValidUser(string userid,string password,string yanzheng)//登陆验证
       {
           string _CurYanzheng =LbShow.Text.Trim();
           if (_CurYanzheng == yanzheng)                                       //验证码
           {                                                                   //用户名密码验证
               string strConnection = "Provider=Microsoft.ACE.OLEDB.12.0;";//mdb："Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\Users\\xiaxia\\Desktop\\temp\\Database1.mdb" 
                                                                           //accdb："Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\Users\\xiaxia\\Desktop\\temp\\haha.accdb"

               strConnection += @"Data Source=F:\UserManagement.accdb";
               //strConnection += @"Persist Security Info=true";
               OleDbConnection con = new OleDbConnection(strConnection);
               string cmdText = string.Format("SELECT role FROM UserInfo WHERE UserID='{0}' AND Password='{1}' ", userid, password);
               OleDbCommand cmd = new OleDbCommand(cmdText, con);
               try
               {
                   con.Open();
                   OleDbDataReader dr = cmd.ExecuteReader();
                   if (dr.Read())
                   {
                       int ru = (int)dr["role"];
                       return ru;
                   }
                   else
                   {
                       MessageBox.Show("用户名或密码错误");
                       return 0;
                   }
               }

               catch (Exception)
               {
                   return 00;
               }
               finally
               {
                   con.Close();
               }
           }
           else
           {
               MessageBox.Show("请输入正确的验证码");
               return 0;
           };
           
       }

        private void Clear()
        {
            tbUser.Text = "";
            tbPassword.Text = "";
            tbYangzhengip.Text = "";
        }
       private void CreateValidateNumber()//产生验证码
       {
              int length=4;
              string Vchar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q" +
              ",R,S,T,U,V,W,X,Y,Z";
              string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组
              string[] num=new string[length];
              string str = "";
             
              for(int i=0;i<length;i++)
              {
                  Random rand=new Random();
                  int t = rand.Next(0, 35);
                  num[i]=VcArray[t];
                  str = str + num[i];
                  System.Threading.Thread.Sleep(15);

              }
              //TbYanzheng.Text = str;
              LbShow.Text = str;
           

              
      }

       private void button1_Click(object sender, EventArgs e)
       {
           CreateValidateNumber(); //换一张验证码
       }

       private void LbShow_Click(object sender, EventArgs e)
       {
          LbShow.BringToFront();   //置于顶层
       }
             
    }
}
