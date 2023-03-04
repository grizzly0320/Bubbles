namespace Bubbles
{
    public partial class Form1 : Form
    {

        private Painter p;
        public Form1()
        {
            InitializeComponent();
            p = new Painter(mainPanel.CreateGraphics());
            p.Start();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            p.AddNew();
        }


        private void mainPanel_Resize(object sender, EventArgs e)
        {
            p.MainGraphics = mainPanel.CreateGraphics();
        }
    }
}
// Пофиксить баги (закрытие окна и тд)
// Шарики отталкиваются др