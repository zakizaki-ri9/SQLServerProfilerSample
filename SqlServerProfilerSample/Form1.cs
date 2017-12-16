using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SqlServerProfilerSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowGrid(ExecProcedure());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowGrid(ExecProcedure("AAA"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowGrid(ExecProcedure(optionA:"a"));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowGrid(ExecSql());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowGrid(ExecSql("AAA"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowGrid(ExecSql(optionA: "a"));
        }

        private void ShowGrid(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
        }

        private string CreateConnectionString()
        {
            // 接続文字列生成
            var conStrBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "localhost",
                InitialCatalog = "SampleDB",
                UserID = "sa",
                Password = "pass"
            };
            return conStrBuilder.ToString();
        }

        private DataTable ExecProcedure(string sampleName = null, string optionA = null)
        {
            DataTable dataTable = new DataTable();
            using (var con = new SqlConnection(CreateConnectionString()))
            {
                // SQLServerへの接続
                con.Open();

                // ストアドプロシージャ設定
                var cmd = new SqlCommand("GetSampleTable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // パラメータ指定
                if (!string.IsNullOrEmpty(sampleName))
                    cmd.Parameters.Add(new SqlParameter("sample_name", sampleName));
                if (!string.IsNullOrEmpty(optionA))
                    cmd.Parameters.Add(new SqlParameter("option_a", optionA));

                // 実行、結果取得
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        private DataTable ExecSql(string sampleName = null, string optionA = null)
        {
            DataTable dataTable = new DataTable();
            using (var con = new SqlConnection(CreateConnectionString()))
            {
                // SQLServerへの接続
                con.Open();

                // SQL設定
                var sql = @"SELECT * FROM SampleTable WHERE 1=1";
                if (!string.IsNullOrEmpty(sampleName))
                    sql += string.Format(" AND sample_name = '{0}'", sampleName);
                if (!string.IsNullOrEmpty(optionA))
                    sql += string.Format(" AND option_a = '{0}'", optionA);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;

                // 実行、結果取得
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}
