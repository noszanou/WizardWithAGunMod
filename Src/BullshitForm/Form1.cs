using BullshitLib;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BullshitForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }
            int amount = numericUpDown1.Value <= 0 ? 1 : (int)numericUpDown1.Value;
            ItemInteraction.CreateItem((comboBox1.SelectedIndex + 1).ToString(), amount);
        }

        public void Xd()
        {
            comboBox1.Items.Clear();
            try // MDR le load des items prend une plombe rompiche
            {
                foreach (var allItem in ItemInteraction.GetItemsByName().Split('\n'))
                {
                    if (string.IsNullOrWhiteSpace(allItem)) continue;
                    comboBox1.Items.Add(allItem);
                }

            }
            catch { }
            if (comboBox1.Items.Count <= 0)
            {
                return;
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Xd();
        }
    }
}