using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Blog.DAL.Infrastructure;
using Blog.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD.DbTestHelpers.Core;

namespace Blog.DAL.Tests
{
    [TestClass]
    public class RepositoryTests: DbBaseTest<BlogFixtures>
    {
        [TestMethod]
        public void GetAllPost_OnePostInDb_ReturnOnePost()
        {
            // arrange
            var repository = new BlogRepository();

            // act
            var result = repository.GetAllPosts();

            // assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(10,result.First().Id);
        }
        [TestMethod]
        public void GetAllComments_ReturnFiveComments()
        {
            //arrange
            var repository = new BlogRepository();

            //act
            var result = repository.GetAllComments();

            //assert
            Assert.AreEqual(5, result.Count());
            Assert.AreEqual(1, result.First().Id);
            Assert.AreEqual(10, result.First().PostId);
        }
        [TestMethod]
        public void AddComment_ShouldAddComment()
        {
            //arrange
            var repository = new BlogRepository();

            //act
            repository.AddComment(15,"przykladowa zawartosc","autor",10);
            var result = repository.GetAllComments();

            //assert
            Assert.AreEqual(6, result.Count());

            Assert.AreNotEqual(0, result.Any(x=>x.Id==15));
            Assert.AreEqual("przykladowa zawartosc", result.Where(x=>x.Id==15).First().Content);
            Assert.AreEqual("autor", result.Where(x => x.Id == 15).First().Author);
            Assert.AreEqual(10, result.Where(x => x.Id == 15).First().PostId);
        }
        [TestMethod]
        public void GetCommentsForPost_ShouldReturnThreePosts()
        {
            //arrange
            var repository = new BlogRepository();

            //act
            var result = repository.GetCommentsForPost(10);

            //assert
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result.First().Id);
            Assert.AreEqual("jakis tam", result.First().Author);
            Assert.AreEqual(10, result.First().PostId);

            Assert.AreEqual(40, result.ElementAt(1).Id);
            Assert.AreEqual("zawartosc komentarza", result.ElementAt(1).Content);
            Assert.AreEqual(10, result.ElementAt(1).PostId);

            Assert.AreEqual(50, result.Last().Id);
            Assert.AreEqual("kolejny autor", result.Last().Author);
            Assert.AreEqual(10, result.Last().PostId);
        }
    }
}
