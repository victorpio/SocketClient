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
using System.Net.Sockets;
using System.Threading;


namespace Petrix_Socket_Server
{
    public partial class Form1 : Form
    {
        private NetworkStream networkStream;
        private BinaryWriter binaryWriter;
        private BinaryReader binaryReader;
        private TcpClient tcpClient;
        private Thread thread;


        public Form1()
        {
            InitializeComponent();
        }

        public void RunClient()
        {
            try
            {
                tcpClient = new TcpClient();
                //conectando ao servidor
                tcpClient.Connect("127.0.0.1", 10005);

                networkStream = tcpClient.GetStream();
                binaryWriter = new BinaryWriter(networkStream);
                binaryReader = new BinaryReader(networkStream);
                binaryWriter.Write("Conexão requisitada pelo cliente");
                String message = "";

                #region laço para receber mensagem do servidor
                do
                {
                    try
                    {
                        message = binaryReader.ReadString();
                        Invoke(new MethodInvoker(
                          delegate { listBox1.Items.Add(message); }
                          ));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro");
                        message = "FIM";
                    }
                } while (message != "FIM");
                #endregion

                binaryWriter.Close();
                binaryReader.Close();
                networkStream.Close();
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                binaryWriter.Write("1");
            }
            catch (SocketException socketEx)
            {
                MessageBox.Show(socketEx.Message, "Erro");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                binaryWriter.Write("2");
            }
            catch (SocketException socketEx)
            {
                MessageBox.Show(socketEx.Message, "Erro");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                binaryWriter.Write("3");
                
            }
            catch (SocketException socketEx)
            {
                MessageBox.Show(socketEx.Message, "Erro");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Ligado" || button4.Text == "Conectar!")
            {
                thread = new Thread(new ThreadStart(RunClient));
                thread.Start();
                button4.Text = "Desconectar!";
                
            }
            else
                thread.Abort();
            button4.Text = "Conectar!";
        }
    }
}
