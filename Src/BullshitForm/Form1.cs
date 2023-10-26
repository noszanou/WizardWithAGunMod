using BullshitLib;
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
            ItemInteraction.CreateItem(amount: 5000);
        }
    }
}