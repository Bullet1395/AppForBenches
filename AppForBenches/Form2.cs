using AppForBenches.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForBenches
{
    public partial class Form2 : Form
    {
        public List<NameEquals> NewListSettings;
        public List<Devices> listDevices;

        public Form2()
        {
            NewListSettings = new List<NameEquals>();
            listDevices = new List<Devices>();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var currentSettings = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt");

            var listSettings = JsonSerializer.Deserialize<List<NameEquals>>(currentSettings);

            foreach (var device in listDevices)
            {
                foreach (var game in device.Games)
                {
                    if (!listSettings.Exists(t => t.OldName == game.NameGame))
                    {
                        listSettings.Add(
                            new NameEquals()
                            {
                                Id = listSettings.Count != 0 ? listSettings.Last().Id+1 : 1,
                                OldName = game.NameGame,
                                NewName = game.NameGame
                            });
                    }
                }
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt", JsonSerializer.Serialize<List<NameEquals>>(listSettings).ToString());

            names_listBox.DataSource = listSettings;
            names_listBox.DisplayMember = "OldName";
        }

        private void Names_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (names_listBox.SelectedItem != null)
            {
                var selItem = (NameEquals)names_listBox.SelectedItem;
                oldName_textBox.Text = selItem.OldName;
                newName_textBox.Text = selItem.NewName;
            } else
            {
                oldName_textBox.Text = null;
                newName_textBox.Text = null;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (names_listBox.SelectedItem != null)
            {
                var selItem = (NameEquals)names_listBox.SelectedItem;

                var currentSettings = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt");

                var listSettings = JsonSerializer.Deserialize<List<NameEquals>>(currentSettings);

                listSettings.First(t => t.OldName == selItem.OldName).NewName = newName_textBox.Text;

                File.WriteAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt", JsonSerializer.Serialize<List<NameEquals>>(listSettings).ToString());

                RepaintListBox();
            }
        }

        public void RepaintListBox()
        {
            var currentSettings = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt");

            var listSettings = JsonSerializer.Deserialize<List<NameEquals>>(currentSettings);

            names_listBox.DataSource = listSettings;
            names_listBox.DisplayMember = "OldName";
        }
    }
}
