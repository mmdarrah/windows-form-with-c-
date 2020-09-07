using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Tipsmaskinen
{
    public partial class Form1 : Form
    {
        //Create tips object from the file loader class
        FileLoader tips = new FileLoader();

        public Form1()
        {
            InitializeComponent();
            //Caling the fileLoader function
            tips.fileLoader();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Righting inside the richTextBox1
            richTextBox1.Text = Convert.ToString(tips.randomBook());

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
    
    public class FileLoader
    {
        // Creat a list of Books
        List<Book> books = new List<Book>();
        public void fileLoader()
        {
            // Check if th text file exist
            if (File.Exists("texter.txt"))
            {
                // Creating a list of strings
                List<string> items = new List<string>();
                // StreamReader to load a text, line by line
                StreamReader reader = new StreamReader("texter.txt", Encoding.Default, false);
                string item = "";
                // if there are item
                while ((item = reader.ReadLine()) != null)
                {
                    items.Add(item);
                }
                //Lopping 
                foreach (string a in items)
                {
                    string[] vektor = a.Split(new string[] { "###" }, StringSplitOptions.None);
                    //Put each part in the correct variable
                    string titel = vektor[0];
                    string författare = vektor[1];
                    string type = vektor[2];// the type will be uesd in the switch to determine the book class
                    bool available = Convert.ToBoolean(vektor[3]);

                    
                    switch (type)
                    {
                        case "Roman":
                            Novel novelBook = new Novel(titel, författare, available);
                            books.Add(novelBook);
                            break;

                        case "Novellsamling":
                            Journal journalBbook = new Journal(titel, författare, available);
                            books.Add(journalBbook);
                            break;

                        case "Tidskrift":
                            ShorStory shorStoryBook = new ShorStory(titel, författare, available);
                            books.Add(shorStoryBook);
                            break;
                    }

                }

            }
            else
            {
                //A message will be show if the text file is missing
                MessageBox.Show("File is not found");
            }

        }
        // A function to generate a random book from the book list
        public string randomBook()
        {
            Random random = new Random();
            int r = random.Next(books.Count);
            string val = Convert.ToString(books[r]);
            return val;

        }

    }


        // Declaring the base class
        class Book
        {
            // Class properties all are public strings
            public string bookTitle;
            public string bookAuthor;
            public string bookType;
            public bool tillgänglig;// A new bool variable because it is in the text file. Maybe we need it in the future

        // Class constructor
        public Book(string title, string author, bool available)
            {
                bookTitle = title;
                bookAuthor = author;
                tillgänglig = available;
            }
        }

        // Subclass with one more propertie
        class Novel : Book
        {
            //Inherited constructor
            public Novel(string title, string author, bool available) : base(title, author, available)
            {
                bookType = "(Novel)";
            }

            public override string ToString()
            {
                return bookTitle + " of " + bookAuthor+ ". " + bookType;
        }
        }
        // Subclass with one more propertie
        class Journal : Book
        {
            //Inherited constructor
            public Journal(string title, string author, bool available) : base(title, author, available)
            {
                bookType = "(Journal)";
            }
            public override string ToString()
            {
                return bookTitle + " of " + bookAuthor + ". " + bookType;
        }
        }
        // Subclass with one more propertie
        class ShorStory : Book
        {
            //Inherited constructor
            public ShorStory(string title, string author, bool available) : base(title, author, available)
            {
                bookType = "(Short story)";
            }
            public override string ToString()
            {
                return bookTitle + " of " + bookAuthor + ". " + bookType;
        }
        }
}
