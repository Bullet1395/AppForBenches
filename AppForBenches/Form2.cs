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

        public Form2()
        {
            NewListSettings = new List<NameEquals>();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var currentSettings = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Options\\NameChanger.txt");

            var listSettings = JsonSerializer.Deserialize<List<NameEquals>>(currentSettings);
        }
    }
}
