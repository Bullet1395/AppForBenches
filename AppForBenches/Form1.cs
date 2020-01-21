using AppForBenches.Models;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForBenches
{
    public partial class Form1 : Form
    {
        public List<GroupBox> listDevices;

        public Form1()
        {
            listDevices = new List<GroupBox>();

            InitializeComponent();
        }

        public void RepaintGroupBoxes()
        {
            var refreshedList = new List<GroupBox>();

            foreach (var device in listDevices)
            {
                if (refreshedList.Count() == 0)
                {
                    device.Location = new Point(12, 27);
                    refreshedList.Add(device);
                }
                else
                {
                    var lastLocation = refreshedList.Last().Location;

                    if (refreshedList.Count % 3 == 0)
                    {
                        device.Location = new Point(12, lastLocation.Y + 6 + device.Size.Height);
                    }
                    else
                    {
                        device.Location = new Point(lastLocation.X + 6 + device.Size.Width, lastLocation.Y);
                    }

                    refreshedList.Add(device);
                }
            }
        }

        public void CloseGroupBox(object sender, EventArgs e)
        {
            var closeButton = (Button)sender;

            var groupBox = listDevices.First(t => t.Name.Equals(closeButton.Name));
            groupBox.Dispose();
            listDevices.Remove(groupBox);
            RepaintGroupBoxes();
        }

        public void CreateNewControlForDevice(string NameDevice, List<Games> games)
        {
            var group = new GroupBox();
            var widthGroup = 391 + 10;
            var heightGroup = 211;

            group.Text = NameDevice.Split('\\').Last().Replace(".txt", "");
            group.Size = new Size(widthGroup, heightGroup);
            group.Name = $"groupBox{listDevices.Count + 1}";

            if (listDevices.Count == 0)
            {
                group.Location = new Point(12, 27);
            }
            else
            {
                var lastLocation = listDevices.Last().Location;

                if (listDevices.Count % 3 == 0)
                {
                    group.Location = new Point(12, lastLocation.Y + 6 + heightGroup);
                }
                else
                {
                    group.Location = new Point(lastLocation.X + 6 + widthGroup, lastLocation.Y);
                }
            }

            var closeButtonForGoup = new Button();
            closeButtonForGoup.Name = group.Name;
            closeButtonForGoup.Text = "X";
            closeButtonForGoup.Size = new Size(43, 25);
            closeButtonForGoup.Location = new Point(317 + 35, 19);
            closeButtonForGoup.Click += CloseGroupBox;

            var dataGrid = new DataGridView();
            dataGrid.Size = new Size(354 + 35, 151);
            dataGrid.Location = new Point(6, 50);
            dataGrid.RowHeadersVisible = false;
            dataGrid.ReadOnly = true;

            dataGrid.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Название игры",
                    Width = 118,
                    Name = "NameGame"

                });
            dataGrid.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Max FPS",
                    Width = 50,
                    Name = "MaxFps"
                });
            dataGrid.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Avg FPS",
                    Width = 50,
                    Name = "AvgFps"
                });
            dataGrid.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Min FPS",
                    Width = 50,
                    Name = "MinFps"
                });
            dataGrid.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "1%",
                    Width = 50,
                    Name = "LowFps"
                });
            dataGrid.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    HeaderText = "0.1%",
                    Width = 50,
                    Name = "VeryLowFps"
                });

            foreach (var game in games)
            {
                dataGrid.Rows.Add(game.NameGame, game.BenchmarkResults.MaxFramerate, game.BenchmarkResults.AvgFramerate, game.BenchmarkResults.MinFramerate, game.BenchmarkResults.LowFramerate, game.BenchmarkResults.VeryLowFranerate);
            }

            group.Controls.Add(closeButtonForGoup);
            group.Controls.Add(dataGrid);
            this.Controls.Add(group);

            listDevices.Add(group);
        }

        private void AddBecnhamrksFile_toolTipMenu_Click(object sender, EventArgs e)
        {
            AddBenchmark_openFileDialog.Filter = "Text files(*.txt)|*.txt";
            AddBenchmark_openFileDialog.Multiselect = true;
            AddBenchmark_openFileDialog.Title = "Выберите файлы";

            if (AddBenchmark_openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var files = AddBenchmark_openFileDialog.FileNames;

            foreach (var file in files)
            {
                var fileText = System.IO.File.ReadAllLines(file);

                var games = new List<Games>();

                var reg = new Regex(@"\s+");

                for (int i = 0; i < fileText.Count(); i += 6)
                {
                    if (fileText[i] != "")
                    {
                        games.Add(
                            new Games()
                            {
                                NameGame = fileText[i].Split(' ')[2],
                                BenchmarkResults = new BenchmarkResult()
                                {
                                    DateBench = DateTime.Parse($"{fileText[i].Split(' ')[0].Replace(',', ' ')}{fileText[i].Split(' ')[1]}"),
                                    MaxFramerate = Convert.ToDouble(reg.Replace(fileText[i + 3], @" ").Split(' ')[4].Replace('.', ',')),
                                    AvgFramerate = Convert.ToDouble(reg.Replace(fileText[i + 1], @" ").Split(' ')[4].Replace('.', ',')),
                                    MinFramerate = Convert.ToDouble(reg.Replace(fileText[i + 2], @" ").Split(' ')[4].Replace('.', ',')),
                                    LowFramerate = Convert.ToDouble(reg.Replace(fileText[i + 4], @" ").Split(' ')[5].Replace('.', ',')),
                                    VeryLowFranerate = Convert.ToDouble(reg.Replace(fileText[i + 5], @" ").Split(' ')[5].Replace('.', ','))
                                }
                            });
                    }
                }

                CreateNewControlForDevice(file, games);

                CreateNameEqualsSettings(games);
            }
        }

        public void CreateNameEqualsSettings(List<Games> games)
        {
            var pathSettings = Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt";

            var currentSettings = new List<NameEquals>();

            if (File.ReadAllText(pathSettings) != "")
            {
                currentSettings = JsonSerializer.Deserialize<List<NameEquals>>(File.ReadAllText(pathSettings));
            }             

            foreach (var game in games)
            {
                if (currentSettings.FirstOrDefault(t => t.OldName == game.NameGame) == null)
                {
                    currentSettings.Add(
                        new NameEquals()
                        {
                            Id = currentSettings.Count() > 0 ? currentSettings.LastOrDefault().Id + 1 : 0,
                            OldName = game.NameGame,
                            NewName = game.NameGame
                        });
                }
            }

            var newSettings = JsonSerializer.Serialize<List<NameEquals>>(currentSettings);

            File.WriteAllText(pathSettings, newSettings);
        }

        private void СохранитьВWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //create a WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //fill cell data with a loop, note that row and column indexes start at 1
                Random rnd = new Random();
                for (int i = 2; i <= 11; i++)
                {
                    worksheet.Cells[1, i].Value = "Value " + (i - 1);
                    worksheet.Cells[2, i].Value = rnd.Next(5, 25);
                    worksheet.Cells[3, i].Value = rnd.Next(5, 25);
                }
                worksheet.Cells[2, 1].Value = "Age 1";
                worksheet.Cells[3, 1].Value = "Age 2";

                //create a new piechart of type Line
                ExcelLineChart lineChart = worksheet.Drawings.AddChart("lineChart", eChartType.Line) as ExcelLineChart;

                //set the title
                lineChart.Title.Text = "LineChart Example";

                //create the ranges for the chart
                var rangeLabel = worksheet.Cells["B1:K1"];
                var range1 = worksheet.Cells["B2:K2"];
                var range2 = worksheet.Cells["B3:K3"];

                //add the ranges to the chart
                lineChart.Series.Add(range1, rangeLabel);
                lineChart.Series.Add(range2, rangeLabel);

                //set the names of the legend
                lineChart.Series[0].Header = worksheet.Cells["A2"].Value.ToString();
                lineChart.Series[1].Header = worksheet.Cells["A3"].Value.ToString();

                //position of the legend
                lineChart.Legend.Position = eLegendPosition.Right;

                //size of the chart
                lineChart.SetSize(600, 300);

                //add the chart at cell B6
                lineChart.SetPosition(5, 0, 1, 0);

                //create a SaveFileDialog instance with some properties
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Excel sheet";
                saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                saveFileDialog1.FileName = "ExcelSheet_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                //check if user clicked the save button
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Get the FileInfo
                    FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                    //write the file to the disk
                    excelPackage.SaveAs(fi);
                }
            }
        }

        private void ЗаменитьНазванияИгрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var pathSettings = Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt";

            if (!File.Exists(pathSettings))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Options");
                File.Create(pathSettings);
            }
        }
    }
}
