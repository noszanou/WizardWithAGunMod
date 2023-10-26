using System.Threading;

namespace BullshitForm
{
    public static class Wtf
    {
        public static void Open()
        {
            new Thread(() =>
            {
                new Form1().ShowDialog();
            }).Start();
        }
    }
}