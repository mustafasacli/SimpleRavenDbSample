using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavendbSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var documentStore = new DocumentStore
            {
                Url = "http://localhost:8081",
                DefaultDatabase = "DemoBook3"
            }.Initialize())
            using (var session = documentStore.OpenSession())
            {
                var book = new Book2 { Title = "RavenDB Intro", ISBN = "1", Pages = 200 };

                session.Store(book);
                session.SaveChanges();

                Console.WriteLine(book.Id); //prints: books/1
            }

            using (var documentStore = new DocumentStore
            {
                Url = "http://localhost:8081",
                DefaultDatabase = "Demo"
            }.Initialize())
            using (var session = documentStore.OpenSession())
            {
                var book = new Book { Title = "RavenDB Intro", ISBN = "1", Pages = 200 };

                session.Store(book);
                session.SaveChanges();

                Console.WriteLine(book.Id); //prints: books/1
            }

            var dev = new Developer
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "ZD",
                Name = "Mustafa Saçlı",
                KnowledgeBase = new List<Knowledge> {
                    new Knowledge { Language = "C#", Rating = 4, Technology = "Net" },
                    new Knowledge { Language = "Java", Rating = 2, Technology = "Oracle" } }
            };
            var id = dev.Id;
            using (var session = ForaDocumentStore.Store.OpenSession())
            {
                session.Store(dev);
                session.SaveChanges();
            }

            using (var session = ForaDocumentStore.Store.OpenSession())
            {
                var developer = session.Load<Developer>(id);


            }

            Console.ReadKey();
        }
    }
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: '{1}' ISBN: {2} ({3} pages)",
                Id, Title, ISBN, Pages);
        }
    }

    public class Book2
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: '{1}' ISBN: {2} ({3} pages)",
                Id, Title, ISBN, Pages);
        }
    }
    public class Developer
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        private DateTime createdOn = DateTime.Now;

        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        public long CreatedOnUnix { get { return createdOn.Ticks; } }

        public List<Knowledge> KnowledgeBase { get; set; }
    }
    public class Knowledge
    {
        public string Language { get; set; }

        public string Technology { get; set; }

        public ushort Rating { get; set; }
    }
}
