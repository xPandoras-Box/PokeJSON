using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.IO;

namespace pokeJson
{
    public partial class Form1 : Form
    {
        private ArrayList pokes = new ArrayList();
        int current = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader inFile = File.OpenText("pokemon.json");
            while (inFile.Peek() != -1)
            {
                string p = inFile.ReadLine();
                Pokemon pString = JsonSerializer.Deserialize<Pokemon>(p);
                pokes.Add(pString);
            }
            
            show();
        }

        public void show()
        {
            if (pokes.Count > 0)
            {
            Pokemon p = (Pokemon)pokes[current];
            nameTextbox.Text = p.Name;
            moveTextbox.Text = p.Move;
            typeTextbox.Text = p.Type;
            hpNumeric.Value = p.HP;
            desTextbox.Text = p.Desc;
            if (File.Exists(p.Image))
            {
                pictureBox1.ImageLocation = p.Image;

            }
            

            }
            
        }

        public void save()
        {
            var entry = new Pokemon
            {
                Name = nameTextbox.Text,
                Move = moveTextbox.Text,
                Type = typeTextbox.Text,
                HP = (int)hpNumeric.Value,
                Desc = desTextbox.Text,
                Image = pictureBox1.ImageLocation

            };
            String pokemonString = JsonSerializer.Serialize(entry);
            pokes.Add(entry);
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            StreamWriter outFile = File.CreateText("pokemon.json");
            foreach (var item in pokes)
            {
                outFile.WriteLine(JsonSerializer.Serialize(item));
            }
            outFile.Close();
           
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (pokes.Count == current + 1)
            {
                current = 0;
            }
            else if (current < pokes.Count)
            {
                current++;
            }
            show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            nameTextbox.Text = null;
            desTextbox.Text = null;
            hpNumeric.Text = null;
            moveTextbox.Text = null;
            typeTextbox.Text = null;
            pictureBox1.Image = null;
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (current == 0)
            {
                current = pokes.Count - 1;
            }
            else if (current > 0)
            {
                current--;
            }
            show();
        }

        private void firstButton_Click(object sender, EventArgs e)
        {
            current = 0;
            show();
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            current = pokes.Count - 1;
            show();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            pokes.Remove(pokes[current]);
            show();
        }
    }
    }




