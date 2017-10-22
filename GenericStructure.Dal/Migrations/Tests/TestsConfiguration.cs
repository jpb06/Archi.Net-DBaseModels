namespace GenericStructure.Dal.Migrations.Tests
{
    using GenericStructure.Dal.Context.EndObjects;
    using GenericStructure.Models.CoreBusiness;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class TestsConfiguration : DbMigrationsConfiguration<CoreBusinessTestContext>
    {
        public TestsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Tests";
        }

        protected override void Seed(CoreBusinessTestContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Categories.AddOrUpdate(p => p.Title, new Category
            {
                Title = "Books",
                Articles = new[]{
                    new Article 
                    {
                        Title = "C# 6.0 in a nutshell", 
                        Description = "When you have questions about C# 6.0 or the .NET CLR and its core Framework assemblies, this bestselling guide has the answers you need. C# has become a language of unusual flexibility and breadth since its premiere in 2000, but this continual growth means there’s still much more to learn.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 60.0m
                    },
                    new Article
                    {
                        Title = "C# 5.0 pocket reference guide",
                        Description = "When you need answers for programming with C# 5.0, this practical and tightly focused book tells you exactly what you need to know—without long introductions or bloated samples. Easy to browse, it’s ideal as quick reference or as a guide to get you rapidly up to speed if you already know Java, C++, or an earlier version of C#. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 55.0m
                    },
                    new Article
                    {
                        Title = "Programming WCF Services Fourth Edition",
                        Description = "Programming WCF Services is the authoritative, bestselling guide to Microsoft's unified platform for developing modern service-oriented applications on Windows. Hailed as the definitive treatment of WCF, this book provides unique insight, rather than documentation, to help you learn the topics and skills you need for building WCF-based applications that are maintainable, extensible, and reusable. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 39.0m
                    },
                    new Article
                    {
                        Title = "C# 6.0 Cookbook Fourth Edition",
                        Description = "Completely updated for C# 6.0, the new edition of this bestseller offers more than 150 code recipes to common and not-so-common problems that C# programmers face every day. Each recipe includes tested code that you can download and reuse in your own applications, and contains a detailed discussion of how and why the underlying technology works. You’ll find new recipes that take advantage of recent C# innovations, such as expression-level, member-declaration, and statement-level features. New and existing recipes also include examples of dynamic and asynchronous programming to help you understand how to use those language features. If you prefer solutions to general C# language instruction and quick answers to theory, this is your book.",
                        ImagesPath = Guid.NewGuid(),
                        Price = 61.9m
                    },
                    new Article
                    {
                        Title = "Building Maintainable Software, C# Edition",
                        Description = "Have you ever felt frustrated working with someone else’s code? Difficult-to-maintain source code is a big problem in software development today, leading to costly delays and defects. Be part of the solution. With this practical book, you’ll learn 10 easy-to-follow guidelines for delivering C# software that’s easy to maintain and adapt. These guidelines have been derived from analyzing hundreds of real-world systems. Written by consultants from the Software Improvement Group (SIG), this book provides clear and concise explanations, with advice for turning the guidelines into practice. Examples for this edition are written in C#, while our companion Java book provides clear examples in that language. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 49.9m
                    },
                    new Article
                    {
                        Title = "Cassandra, the definitive guide",
                        Description = "What could you do with data if scalability wasn't a problem? With this hands-on guide, you'll learn how Apache Cassandra handles hundreds of terabytes of data while remaining highly available across multiple data centers -- capabilities that have attracted Facebook, Twitter, and other data-intensive companies. Cassandra: The Definitive Guide provides the technical details and practical examples you need to assess this database management system and put it to work in a production environment. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 19.995m
                    },
                    new Article
                    {
                        Title = "Data Algorithms",
                        Description = "If you are ready to dive into the MapReduce framework for processing large datasets, this practical book takes you step by step through the algorithms and tools you need to build distributed MapReduce applications with Apache Hadoop or Apache Spark. Each chapter provides a recipe for solving a massive computational problem, such as building a recommendation system. You’ll learn how to implement the appropriate MapReduce solution with code that you can use in your projects. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 101.1m
                    },
                    new Article
                    {
                        Title = "Client-Side Data Storage",
                        Description = "One of the most useful features of today’s modern browsers is the ability to store data right on the user’s computer or mobile device. Even as more people move toward the cloud, client-side storage can still save web developers a lot of time and money, if you do it right. This hands-on guide demonstrates several storage APIs in action. You’ll learn how and when to use them, their plusses and minuses, and steps for implementing one or more of them in your application. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 52.12m
                    },
                    new Article
                    {
                        Title = "The New Relational Database Dictionary",
                        Description = "No matter what DBMS you are using—Oracle, DB2, SQL Server, MySQL, PostgreSQL—misunderstandings can always arise over the precise meanings of terms, misunderstandings that can have a serious effect on the success of your database projects. For example, here are some common database terms: attribute, BCNF, consistency, denormalization, predicate, repeating group, join dependency. Do you know what they all mean? Are you sure? ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 49.9m
                    },
                    new Article
                    {
                        Title = "Exploring ES6",
                        Description = "This book is about ECMAScript 6 (whose official name is ECMAScript 2015), a new version of JavaScript.",
                        ImagesPath = Guid.NewGuid(),
                        Price = 15.0m
                    },
                    new Article
                    {
                        Title = "Node.js 8 the Right Way",
                        Description = "Node.js is the platform of choice for creating modern web services. This fast-paced book gets you up to speed on server-side programming with Node.js 8, as you develop real programs that are small, fast, low-profile, and useful. Take JavaScript beyond the browser, explore dynamic language features, and embrace evented programming.Harness the power of the event loop and non-blocking I/O to create highly parallel microservices and applications. This expanded and updated second edition showcases the latest ECMAScript features, current best practices, and modern development techniques.",
                        ImagesPath = Guid.NewGuid(),
                        Price = 69.999m
                    },
                    new Article
                    {
                        Title = "JavaScript: The Definitive Guide, 6th Edition ",
                        Description = "Since 1996, JavaScript: The Definitive Guide has been the bible for JavaScript programmers—a programmer's guide and comprehensive reference to the core language and to the client-side JavaScript APIs defined by web browsers. The 6th edition covers HTML5 and ECMAScript 5. Many chapters have been completely rewritten to bring them in line with today's best web development practices. New chapters in this edition document jQuery and server side JavaScript. It's recommended for experienced programmers who want to learn the programming language of the Web, and for current JavaScript programmers who want to master it. ",
                        ImagesPath = Guid.NewGuid(),
                        Price = 39.01m
                    }
                }
            });

            context.Categories.AddOrUpdate(p => p.Title, new Category
            {
                Title = "Hardware",
                Articles = new[]{
                    new Article 
                    {
                        Title = "Raspberry Pi", 
                        Description = "The Raspberry Pi is a series of small single-board computers developed in the United Kingdom by the Raspberry Pi Foundation to promote the teaching of basic computer science in schools and in developing countries. The original model became far more popular than anticipated, selling outside of its target market for uses such as robotics. Peripherals (including keyboards, mice and cases) are not included with the Raspberry Pi. Some accessories however have been included in several official and unofficial bundles.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 50.0m
                    },
                    new Article 
                    {
                        Title = "Raspberry Pi 3 Model B", 
                        Description = "Raspberry Pi® is an ARM based credit card sized SBC(Single Board Computer) created by Raspberry Pi Foundation. Raspberry Pi runs Debian based GNU/Linux operating system Raspbian and ports of many other OSes exist for this SBC.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 79.9m
                    },
                    new Article 
                    {
                        Title = "iPad", 
                        Description = "iPad is a line of tablet computers designed, developed and marketed by Apple Inc., which run the iOS mobile operating system. The first iPad was released on April 3, 2010; the most recent iPad models are the iPad (2017), released on March 24, 2017, and the 10.5-inch (270 mm) and 12.9-inch (330 mm) 2G iPad Pro released on June 13, 2017. The user interface is built around the device's multi-touch screen, including a virtual keyboard. All iPads can connect via Wi-Fi; some models also have cellular connectivity.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 499.9m
                    },
                    new Article 
                    {
                        Title = "Bose SoundSport", 
                        Description = "For many athletes, music helps power you through a difficult set or go that extra mile. Good sound is important, but perhaps even more crucial is the fit because no one wants to be fiddling with their earbuds in the middle of a workout. The sweat-proof Bose SoundSport wireless headphones, which come in three sizes, have StayHear+ eartips with soft silicon fins for a loose yet secure fit. And while fit can be extremely personal, reviewers on Amazon widely agree that comfortability is what makes the Bose SoundSport so great. Note that the looser fit will allow ambient sound to sneak in, but that’s OK for runners and bikers who need to be aware of their surroundings.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 149.5m
                    }
                }
            });

            context.Categories.AddOrUpdate(p => p.Title, new Category
            {
                Title = "Clothes",
                Articles = new[]{
                    new Article 
                    {
                        Title = "T-shirt", 
                        Description = "A T-shirt (or t shirt, or tee) is a style of unisex fabric shirt, named after the T shape of the body and sleeves. It is normally associated with short sleeves, a round neckline, known as a crew neck, with no collar. T-shirts are generally made of a light, inexpensive fabric, and are easy to clean. Typically made of cotton textile in a stockinette or jersey, knit, it has a distinctively pliable texture compared to shirts made of woven cloth. The majority of modern versions have a body made from a continuously woven tube, on a circular loom, so that the torso has no side seams. The manufacture of T-shirts has become highly automated and may include fabric cutting by laser or water jet.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 11.0m
                    },
                    new Article 
                    {
                        Title = "Polo shirt", 
                        Description = "A polo shirt, also known as a golf shirt and tennis shirt, is a form of shirt with a collar, a placket with typically two or three buttons, and an optional pocket. All three terms may be used interchangeably. Polo shirts are usually made of knitted cloth (rather than woven cloth), usually piqué cotton or, less commonly, interlock cotton, silk, merino wool, or synthetic fibers. A dress-length version of the shirt is called a polo dress.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 15.99m
                    },
                    new Article 
                    {
                        Title = "Bermuda short", 
                        Description = "Bermuda shorts, also known as walk shorts or dress shorts, are a particular type of short trousers, worn as semi-casual attire by both men and women. The hem, which can be cuffed or un-cuffed, is around 1 inch above the knee.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 9.95m
                    },
                    new Article 
                    {
                        Title = "Cargo pants", 
                        Description = "Cargo pants or cargo trousers, also sometimes called combat trousers (or combats) after their original military purpose, are loosely cut pants originally designed for tough, outdoor activities, and whose design is distinguished by one or more cargo pockets. Cargo pants have become popular in urban areas as well, since they are convenient for carrying items during day trips on foot. Cargo shorts are a shorts-length version.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 11.5m
                    },
                    new Article 
                    {
                        Title = "Academic dress", 
                        Description = "Academic dress is a traditional form of clothing for academic settings, mainly tertiary (and sometimes secondary) education, worn mainly by those who have been admitted to a university degree (or similar), or hold a status that entitles them to assume them (e.g., undergraduate students at certain old universities). It is also known as academical dress, academicals, and, in the United States, as academic regalia.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 5.99m
                    },
                    new Article 
                    {
                        Title = "School uniform", 
                        Description = "A school uniform is a uniform worn by students primarily for a school or otherwise educational institution. They are common in primary and secondary schools in various countries. Although often used interchangeably, there is an important distinction between dress codes and school uniforms: according to scholars such as Nathan Joseph, clothing can only be considered a uniform when it \"(a) serves as a group emblem, (b) certifies an institution's legitimacy by revealing individual’s relative positions and (c) suppresses individuality.\" An example of a uniform would be requiring white button-downs and ties for boys and pleated skirts for girls, with both wearing blazers. A uniform can even be as simple as requiring collared shirts, or restricting colour choices and limiting items students are allowed to wear. A dress code, on the other hand, is much less restrictive, and focuses \"on promoting modesty and discouraging anti-social fashion statements,\" according to Marian Wilde. Examples of a dress code would be not allowing ripped clothing, no logos or limiting the amount of skin that can be shown.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 19.9m
                    },
                    new Article 
                    {
                        Title = "Baseball cap", 
                        Description = "A baseball cap is a type of soft cap with a rounded crown and a stiff peak projecting in front. The front of the cap typically contains designs or logos of sports teams (namely baseball teams, or names of relevant companies, when used as a commercial marketing technique). The back of the cap may be \"fitted\" to the wearer's head size or it may have a plastic, Velcro, or elastic adjuster so that it can be quickly adjusted to fit different wearers. The baseball cap is a part of the traditional baseball uniform worn by players, with the brim pointing forward to shield the eyes from the sun. Since the 1980s varieties of the cap have become a common fashion accessory, particularly in the United States.",
                        ImagesPath = Guid.NewGuid(), 
                        Price = 14.9m
                    }
                }
            });
        }
    }
}
