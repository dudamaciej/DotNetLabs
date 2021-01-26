using MainProject.Controllers;
using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void AllProductsTest()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, ProductName = "Product1"},
                new Product {ProductId = 2, ProductName = "Product2"},
                new Product {ProductId = 3, ProductName = "Product3"}

            }.AsQueryable<Product>());

            var controller = new ProductController(mock.Object);

            Product[] result = GetViewModel<IEnumerable<Product>>(controller.ListAll())?.ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal("Product1", result[0].ProductName);
            Assert.Equal("Product2", result[1].ProductName);
            Assert.Equal("Product3", result[2].ProductName);

        }

        [Fact]
        public void ProductsByCategoryTest()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, ProductName = "Product1", Category="Category1"},
                new Product {ProductId = 2, ProductName = "Product2", Category="Category1"},
                new Product {ProductId = 3, ProductName = "Product3", Category="Category3"}

            }.AsQueryable<Product>());

            var controller = new ProductController(mock.Object);

            Product[] result = GetViewModel<IEnumerable<Product>>(controller.List("Category1")).ToArray();

            Assert.Equal(2, result.Length);
            Assert.True(result[0].Category == "Category1");
            Assert.True(result[1].Category == "Category1");
        }


        [Theory]
        [InlineData(1, "Product1")]
        [InlineData(2, "Product2")]
        
        public void GetProductByIdTest(int ProductId, string expectedProductName)
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, ProductName = "Product1"},
                new Product {ProductId = 2, ProductName = "Product2"},   

            }.AsQueryable<Product>());

            var controller = new ProductController(mock.Object);

            Product result = GetViewModel<Product>(controller.ProductById(ProductId));

            Assert.Equal(expected: expectedProductName, actual: result.ProductName);
            Assert.Equal(expected: ProductId, actual: result.ProductId);
        }
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
