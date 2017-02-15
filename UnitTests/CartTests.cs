using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //arrange
            Book book1 = new Book { BookId = 1, Name = "Book1 " };
            Book book2 = new Book { BookId = 2, Name = "Book2 " };

            Cart cart = new Cart();

            //act
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            List<CartLine> results = cart.Lines.ToList();

            //assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Book, book1);
            Assert.AreEqual(results[1].Book, book2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //arrange
            Book book1 = new Book { BookId = 1, Name = "Book1 " };
            Book book2 = new Book { BookId = 2, Name = "Book2 " };

            Cart cart = new Cart();

            //act
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c=>c.Book.BookId).ToList();

            //assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //arrange
            Book book1 = new Book { BookId = 1, Name = "Book1 " };
            Book book2 = new Book { BookId = 2, Name = "Book2 " };
            Book book3 = new Book { BookId = 3, Name = "Book3 " };

            Cart cart = new Cart();

            //act
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.AddItem(book3, 2);
            cart.RemoveLine(book2);

            //assert
            Assert.AreEqual(cart.Lines.Where(c => c.Book == book2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }


        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //arrange
            Book book1 = new Book { BookId = 1, Name = "Book1 " , Price = 100};
            Book book2 = new Book { BookId = 2, Name = "Book2 " , Price = 55};

            Cart cart = new Cart();

            //act
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            decimal result = cart.ComputeTotalValue();

            //assert
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            //arrange
            Book book1 = new Book { BookId = 1, Name = "Book1 ", Price = 100 };
            Book book2 = new Book { BookId = 2, Name = "Book2 ", Price = 55 };

            Cart cart = new Cart();

            //act
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.Clear();

            //assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
