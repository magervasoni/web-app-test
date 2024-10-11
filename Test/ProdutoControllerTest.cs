using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class ProdutoControllerTest
    {
        private readonly Mock<IProdutoRepository> _userRepositoryMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _userRepositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_userRepositoryMock.Object);
        }
        [Fact]
        public async Task Get_ListarProdutosOk()
        {
            //arrange
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    Id = 1,
                    Nome = "Marcela",
                    Preco = "12,90",
                    Quantidade_estoque = "43",
                    Data_criacao = "10/10/2023"
                }
            };
            _userRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);

            //act
            var result = await _controller.GetProduto();
            
            //asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]
        public async Task Get_ListarRetornoNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync((IEnumerable<Produto>)null);

            var result = await _controller.GetProduto();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarProduto()
        {
            //arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Marcela",
                Preco = "12,90",
                Quantidade_estoque = "43",
                Data_criacao = "10/10/2023"
            };
            _userRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            //act
            var result = await _controller.Post(produto);
            _userRepositoryMock.Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once());
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
