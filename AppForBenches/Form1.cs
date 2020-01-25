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
        public List<GroupBox> listDevicesGroupBox;
        public List<Devices> listDevices;

        public Form1()
        {
            listDevicesGroupBox = new List<GroupBox>();
            listDevices = new List<Devices>();

            InitializeComponent();
        }

        public void RepaintGroupBoxes()
        {
            var refreshedList = new List<GroupBox>();

            foreach (var device in listDevicesGroupBox)
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

            var groupBox = listDevicesGroupBox.First(t => t.Name.Equals(closeButton.Name));
            var device = listDevices.First(t => t.NameDevice == groupBox.Text);
            groupBox.Dispose();
            listDevicesGroupBox.Remove(groupBox);
            listDevices.Remove(device);
            RepaintGroupBoxes();
        }

        public void CreateNewControlForDevice(string NameDevice, List<Games> games)
        {
            var currentSettings = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt");
            var listSettings = JsonSerializer.Deserialize<List<NameEquals>>(currentSettings);

            var group = new GroupBox();
            var widthGroup = 391 + 10;
            var heightGroup = 211;

            group.Text = NameDevice.Split('\\').Last().Replace(".txt", "");
            group.Size = new Size(widthGroup, heightGroup);
            group.Name = $"groupBox{listDevicesGroupBox.Count + 1}";

            listDevices.Add(
                new Devices()
                {
                    IdDevice = listDevices.Count != 0 ? listDevices.Last().IdDevice + 1 : 1,
                    NameDevice = group.Text,
                    Games = games
                });

            if (listDevicesGroupBox.Count == 0)
            {
                group.Location = new Point(12, 27);
            }
            else
            {
                var lastLocation = listDevicesGroupBox.Last().Location;

                if (listDevicesGroupBox.Count % 3 == 0)
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
                if (listSettings != null)
                {
                    dataGrid.Rows.Add(listSettings.Exists(t => t.OldName == game.NameGame) ? listSettings.First(t => t.OldName == game.NameGame).NewName : game.NameGame , game.BenchmarkResults.MaxFramerate, game.BenchmarkResults.AvgFramerate, game.BenchmarkResults.MinFramerate, game.BenchmarkResults.LowFramerate, game.BenchmarkResults.VeryLowFranerate);

                }
                else
                {
                    dataGrid.Rows.Add(game.NameGame, game.BenchmarkResults.MaxFramerate, game.BenchmarkResults.AvgFramerate, game.BenchmarkResults.MinFramerate, game.BenchmarkResults.LowFramerate, game.BenchmarkResults.VeryLowFranerate);

                }
            }

            var copyTextChartForOverClockers = new LinkLabel();
            copyTextChartForOverClockers.Text = "Скопировать код для вставки графика на Overclockers.ru";
            copyTextChartForOverClockers.Location = new Point(6, 24);
            copyTextChartForOverClockers.Width = 300;
            copyTextChartForOverClockers.Name = group.Name;
            copyTextChartForOverClockers.Enabled = false;
            copyTextChartForOverClockers.Click += CopyHtmlTextForOver(games);

            group.Controls.Add(copyTextChartForOverClockers);
            group.Controls.Add(closeButtonForGoup);
            group.Controls.Add(dataGrid);
            this.Controls.Add(group);

            listDevicesGroupBox.Add(group);
        }

        private EventHandler CopyHtmlTextForOver(List<Games> games)
        {
            var filteredList = games.OrderBy(t => t.BenchmarkResults.MaxFramerate).ToList();

            var bufferText = 
                "<div class=\"graphic\" align=\"center\">";
            bufferText += 
                "<b> (текст: Уровень шума) </b><br><br>" +
                "<b> (текст: видеокарта, дб) </ b >< br >" +
                "<b> Меньше / Больше – лучше / хуже </ b >< br > ";
            bufferText += "<font color=\"#800000\" id=\"c1\">< table style = \"width:auto\" cellpadding = \"4\" border = \"0\" cellspacing = \"0\" onmouseout = \"unmark(1)\" >< tbody > ";

            foreach (var game in games)
            {
                bufferText += "<tr id=\"c1r0\" onmouseover=\"mark(1, 0, false, 1, 0)\" class=\"\">" +
                    "                    < td id = \"c1d0\" style = \"vertical-align:middle;padding-right:4px;padding-top:4px;padding-bottom:4px;border-right-style: solid; border-right-width: 1px; border-right-color: #A0A0A0;\" align = \"right\" >" +
                    $"< nobr >< font color = \"#000000\" > {game.NameGame} </ font >" +
                    $"</ nobr >" +
                    $"</ td >" +
                    $" < td style = \"vertical-align:middle;padding-left:0\" >" +
                    $"< table style = \"width:320px\" border = \"0\" cellpadding = \"2\" cellspacing = \"0\" >" +
                    $"< tbody >" +
                    $"< tr >" +
                    $"< td style = \"padding-left:0;padding-right:0;padding-top:2px;padding-bottom:2px\" width = \"80%\" align = \"right\" bgcolor = \"#e00000\" >" +
                    $"< font color = \"#FFFFFF\" id = \"c1d0d0\" > 36 </ font >" +
                    $"</ td >" +
                    $"< td style = \"\" > &nbsp;</ td >" +
                    $"</ tr >" +
                    $"</ tbody >" +
                    $"</ table >" +
                    $"</ td >" +
                    $"</ tr >";                                              
            }

            bufferText +=
                "<tr style=\"font - size:1px\"><td style=\"border - right - style: solid; border - right - width: 1px; border - right - color: #A0A0A0;padding-top:0;padding-bottom:0\">&nbsp;</td>" +
                "<td style=\"padding-left:0;padding-top:0;padding-bottom:0\">" +
                "<table style=\"font-size:4px;width:100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                "<tbody><tr><td style=\"border-right-style: solid; border-right-width: 1px; border-right-color: #A0A0A0;border-top-style: solid; border-top-width: 1px; border-top-color: #A0A0A0;\" width=\"22%\">&nbsp;</td>" +
                "<td style=\"border-right-style: solid; border-right-width: 1px; border-right-color: #b3b3b3;border-top-style: solid; border-top-width: 1px; border-top-color: #b3b3b3;\" width=\"22%\">&nbsp;</td>" +
                "<td style=\"border-right-style: solid; border-right-width: 1px; border-right-color: #c6c6c6;border-top-style: solid; border-top-width: 1px; border-top-color: #c6c6c6;\" width=\"23%\">&nbsp;</td>" +
                "<td style=\"border-right-style: solid; border-right-width: 1px; border-right-color: #d9d9d9;border-top-style: solid; border-top-width: 1px; border-top-color: #d9d9d9;\" width=\"22%\">&nbsp;</td>" +
                "<td style=\"border-top-style: solid; border-top-width: 1px; border-top-color: #ececec;\">&nbsp;</td>" +
                "</ tr >" +
                "</ tbody >" +
                "</ table >" +
                "</ td >" +
                "</ tr >" +
                " < tr >" +
                "< td style = \"padding-top:0;padding-bottom:0\" > &nbsp;" +
                "</ td >" +
                "< td style = \"padding-left:0;padding-top:0;padding-bottom:0\" >" +
                "< table border = \"0\" cellpadding = \"0\" cellspacing = \"0\" style = \"width:100%\" >" +
                "< tbody >" +
                "< tr >" +
                "< td style = \"color:#808080\" align = \"center\" width = \"4.5%\" > 0 </ td >" +
                "< td width = \"17.5%\" > &nbsp;</ td >" +
                " < td style = \"color:#999999\" align = \"center\" width = \"4.5%\" > 10 </ td >" +
                "< td width = \"17.5%\" > &nbsp;</ td >< td style = \"color:#b2b2b2\" align = \"center\" width = \"4.5%\" > 20 </ td >" +
                "< td width = \"18.5%\" > &nbsp;</ td >< td style = \"color:#cbcbcb\" align = \"center\" width = \"4.5%\" > 30 </ td >" +
                "< td width = \"17.5%\" > &nbsp;</ td >< td style = \"color:#e4e4e4\" align = \"center\" width = \"4.5%\" > 40 </ td >" +
                "< td width = \"17.5%\" > &nbsp;</ td ></ tr ></ tbody ></ table ></ td ></ tr ></ tbody ></ table ></ font >" +
                "< script language = \"javascript\" > chartdraw(1, 320, { axisMin: 0});</ script ></ div >";

            Clipboard.SetText(bufferText);
            return null;
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
            var currentSettings = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt");
            var listSettings = JsonSerializer.Deserialize<List<NameEquals>>(currentSettings);

            var listGames = new List<string>();

            foreach (var device in listDevices)
            {
                foreach (var game in device.Games)
                {
                    if (!listGames.Contains(game.NameGame))
                    {
                        listGames.Add(game.NameGame);
                    }                    
                }
            }

            listGames = listGames.OrderBy(t => t).ToList();

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //create a WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Бенчмарки");

                worksheet.Column(1).Width = 26;

                worksheet.Cells[1, 2].Value = "0.1%";
                worksheet.Cells[1, 3].Value = "1%";
                worksheet.Cells[1, 4].Value = "Low";
                worksheet.Cells[1, 5].Value = "Average";
                worksheet.Cells[1, 6].Value = "Max";

                var currentRow = 3;

                foreach (var game in listGames)
                {
                    var startDeviceRow = currentRow;

                    var lineChart = worksheet.Drawings.AddChart($"lineChartFor{game}", eChartType.BarClustered) as ExcelBarChart;

                    lineChart.Style = eChartStyle.Style7;
                    lineChart.DataLabel.ShowValue = true;
                    lineChart.YAxis.RemoveGridlines();

                    lineChart.Title.Text = listSettings.Exists(t => t.OldName == game) ? listSettings.First(t => t.OldName == game).NewName : game;

                    var rangeLabel = worksheet.Cells[1, 2, 1, 6];

                    worksheet.Cells[currentRow - 1, 1].Value = listSettings.Exists(t => t.OldName == game) ? listSettings.First(t => t.OldName == game).NewName : game;

                    foreach (var device in listDevices)
                    {
                        if (device.Games.Exists(t => t.NameGame == game))
                        {
                            var deviceGame = device.Games.First(t => t.NameGame == game);

                            worksheet.Cells[currentRow, 1].Value = device.NameDevice;
                            worksheet.Cells[currentRow, 2].Value = deviceGame.BenchmarkResults.VeryLowFranerate;
                            worksheet.Cells[currentRow, 3].Value = deviceGame.BenchmarkResults.LowFramerate;
                            worksheet.Cells[currentRow, 4].Value = deviceGame.BenchmarkResults.MinFramerate;
                            worksheet.Cells[currentRow, 5].Value = deviceGame.BenchmarkResults.AvgFramerate;
                            worksheet.Cells[currentRow, 6].Value = deviceGame.BenchmarkResults.MaxFramerate;

                            lineChart.Series.Add(worksheet.Cells[currentRow, 2, currentRow, 6], rangeLabel);
                            lineChart.Series[lineChart.Series.Count - 1].Header = device.NameDevice;

                            currentRow++;
                        }
                    }

                    //position of the legend
                    lineChart.Legend.Position = eLegendPosition.Bottom;

                    //size of the chart
                    lineChart.SetSize(600, 700);

                    //add the chart at cell B6
                    lineChart.SetPosition(2, 0, listGames.IndexOf(game) == 0 ? 6 : 10 * (listGames.IndexOf(game) + 1)-4, 0);

                    currentRow += 3;
                }

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

                //foreach (var device in listDevices)
                //{
                //    var startDeviceRow = currentRow;

                //    //create a new piechart of type Line
                //    var lineChart = worksheet.Drawings.AddChart($"lineChartFor{device.NameDevice}", eChartType.BarClustered) as ExcelBarChart;

                //    lineChart.Style = eChartStyle.Style7;
                //    lineChart.DataLabel.ShowValue = true;

                //    //set the title
                //    lineChart.Title.Text = device.NameDevice;

                //    //create the ranges for the chart
                //    var rangeLabel = worksheet.Cells[1, 2, 1, 6];

                //    //worksheet.Cells[currentRow - 1, 6].Merge = true;
                //    worksheet.Cells[currentRow - 1, 1].Value = device.NameDevice;

                //    foreach (var game in device.Games.OrderBy(t => t.NameGame))
                //    {
                //        if (listSettings != null)
                //        {
                //            worksheet.Cells[currentRow, 1].Value = listSettings.Exists(t => t.OldName == game.NameGame) ? listSettings.First(t => t.OldName == game.NameGame).NewName : game.NameGame;
                //        }
                //        else
                //        {
                //            worksheet.Cells[currentRow, 1].Value = game.NameGame;
                //        }

                //        worksheet.Cells[currentRow, 2].Value = game.BenchmarkResults.VeryLowFranerate;
                //        worksheet.Cells[currentRow, 3].Value = game.BenchmarkResults.LowFramerate;
                //        worksheet.Cells[currentRow, 4].Value = game.BenchmarkResults.MinFramerate;
                //        worksheet.Cells[currentRow, 5].Value = game.BenchmarkResults.AvgFramerate;
                //        worksheet.Cells[currentRow, 6].Value = game.BenchmarkResults.MaxFramerate;

                //        lineChart.Series.Add(worksheet.Cells[currentRow, 2, currentRow, 6], rangeLabel);
                //        lineChart.Series[lineChart.Series.Count-1].Header = listSettings.Exists(t => t.OldName == game.NameGame) ? listSettings.First(t => t.OldName == game.NameGame).NewName : game.NameGame;

                //        currentRow++;
                //    }

                //    //position of the legend
                //    lineChart.Legend.Position = eLegendPosition.Bottom;

                //    //size of the chart
                //    lineChart.SetSize(600, 700);

                //    //add the chart at cell B6
                //    lineChart.SetPosition(2, 0, listDevices.IndexOf(device) == 0 ? 6 : 15 * listDevices.IndexOf(device)+1, 0);

                //    currentRow += 3;
                //}

                ////create a SaveFileDialog instance with some properties
                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //saveFileDialog1.Title = "Save Excel sheet";
                //saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                //saveFileDialog1.FileName = "ExcelSheet_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                ////check if user clicked the save button
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    //Get the FileInfo
                //    FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                //    //write the file to the disk
                //    excelPackage.SaveAs(fi);
                //}
            }
        }

        private void ЗаменитьНазванияИгрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.listDevices = listDevices;
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
