using Moq;
using web_app_domain;
using web_app_repository;

namespace Test
{
    public class ProdutoRepositoryTest
    {
        [Fact]
        public async Task ListarProdutos()
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
                },
                new Produto()
                {
                    Id = 1,
                    Nome = "Marcela",
                    Preco = "12,90",
                    Quantidade_estoque = "43",
                    Data_criacao = "10/10/2023"
                }
            };

            var userRepositoryMock = new Mock<IProdutpRepository>();
            userRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync(produtos);
            var userRepository = userRepositoryMock.Object;

            //act
            var result = await userRepository.ListarProdutos();

            //assert
            Assert.Equal(produtos, result);
        }

        [Fact]
        public async Task Salvar_Produto()
        {
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Marcela",
                Preco = "12,90",
                Quantidade_estoque = "43",
                Data_criacao = "10/10/2023"
            };

            var userRepositoryMock = new Mock<IProdutoRepository>();
            userRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>())).Returns(Task .CompletedTask);
            var userRepository = userRepositoryMock.Object;

            //act
            await userRepository.SalvarProduto(produto);
            
            //assert
            userRepositoryMock.Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once());
        }
    }
}
