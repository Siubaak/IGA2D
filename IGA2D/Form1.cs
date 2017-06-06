using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace IGA2D
{
    public partial class Form1 : Form
    {
        private string Input;
        private string FilePath;
        private Instance Ins;
        private DataProcess DataPro;
        private Boolean IsCalculate;
        private void RadioButtonCheck()
        {
            if (IsCalculate)
                if (Disp.Checked) Picture.Image = DataPro.DispPicture;
                else if (SigmaX.Checked) Picture.Image = DataPro.SxPicture;
                else if (SigmaY.Checked) Picture.Image = DataPro.SyPicture;
                else Picture.Image = DataPro.TxyPicture;
        }
        public Form1()
        {
            InitializeComponent();
            Input = null;
            FilePath = null;
            IsCalculate = false;
        }
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsCalculate = false;
            Picture.Image = null;
            Input = null;
            FilePath = null;
            InputBox.Clear();
            OutputBox.Clear();
            Text = "IGA2D";
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "打开";
            fileDialog.Filter = "IGA2D模型文件(*.iga)|*.iga";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                IsCalculate = false;
                Picture.Image = null;
                StreamReader sr = new StreamReader(fileDialog.FileName, Encoding.Default);
                FilePath = fileDialog.FileName;
                Input = sr.ReadToEnd();
                sr.Close();
                InputBox.Text = Input;
                OutputBox.Clear();
                Text = "IGA2D (" + FilePath + ")";
            }
        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilePath == null)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "保存";
                fileDialog.Filter = "IGA2D模型文件(*.iga)|*.iga";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(fileDialog.FileName, false, Encoding.Default);
                    FilePath = fileDialog.FileName;
                    sw.Write(InputBox.Text);
                    sw.Close();
                    Text = "IGA2D (" + FilePath + ")";
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default);
                sw.Write(InputBox.Text);
                sw.Close();
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "另存为";
            fileDialog.Filter = "IGA2D模型文件(*.iga)|*.iga";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(fileDialog.FileName, false, Encoding.Default);
                FilePath = fileDialog.FileName;
                sw.Write(InputBox.Text);
                sw.Close();
                Text = "IGA2D (" + FilePath + ")";
            }
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }
        private void 计算ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IsCalculate = true;
            保存ToolStripMenuItem_Click(sender, e);
            DataPro = new DataProcess();
            try { Ins = DataPro.CreateInstance(InputBox.Text); }
            catch (Exception vErr) { MessageBox.Show(vErr.Message); }
            finally { GC.Collect(); }
            Ins.Calculate();
            DataPro.Draw(Ins, Convert.ToDouble(ScaleBox.Text));
            RadioButtonCheck();
            OutputBox.Text = DataPro.Output(Ins);
        }
        private void 优化ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出计算结果";
            fileDialog.Filter = "文本文件(*.txt)|*.txt|逗号分隔值文件(*.csv)|*.csv";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(fileDialog.FileName, false, Encoding.Default);
                sw.Write(OutputBox.Text);
                sw.Close();
            }
        }
        private void Disp_CheckedChanged(object sender, EventArgs e) { RadioButtonCheck(); }
        private void SigmaX_CheckedChanged(object sender, EventArgs e) { RadioButtonCheck(); }
        private void SigmaY_CheckedChanged(object sender, EventArgs e) { RadioButtonCheck(); }
        private void TauXY_CheckedChanged(object sender, EventArgs e) { RadioButtonCheck(); }
    }
}
