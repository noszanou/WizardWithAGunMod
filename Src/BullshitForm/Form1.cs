using BullshitLib;
using System.Windows.Forms;

namespace BullshitForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadGameItemsAssets();
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

        private void button6_Click(object sender, System.EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }
            int amount = numericUpDown1.Value <= 0 ? 1 : (int)numericUpDown1.Value;
            ItemInteraction.CreateItem((comboBox1.SelectedIndex + 1).ToString(), amount, true);
        }

        public void LoadGameItemsAssets()
        {
            comboBox1.Items.Clear();
            try
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
            // It took to long to load assets item so force it with a button
            LoadGameItemsAssets();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            PlayerInteraction.HandleGodMode();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            PlayerInteraction.HandleDmg();
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            PlayerInteraction.HandleCrafting();
        }
    }
}