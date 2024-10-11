using Moq;
using web_app_domain;
using web_app_repository;

namespace Test
{
    public class UsuarioRepositoryTest
    {
        [Fact]
        public async Task ListarUsuarios()
        {
            //arrange
            var usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Email = "gaby@fiap.com",
                    Id = 1,
                    Nome = "Gaby Freitas"
                },
                new Usuario()
                {
                    Email = "marcela@fiap.com",
                    Id = 2,
                    Nome = "Marcela"
                }
            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = userRepositoryMock.Object;

            //act
            var result = await userRepository.ListarUsuarios();

            //assert
            Assert.Equal(usuarios, result);
        }

        [Fact]
        public async Task Salvar_Usuario()
        {
            var usuario = new Usuario()
            {
                Id = 1,
                Email = "gaby@fiap.com",
                Nome = "Gaby Freitas"
            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task .CompletedTask);
            var userRepository = userRepositoryMock.Object;

            //act
            await userRepository.SalvarUsuario(usuario);
            
            //assert
            userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once());
        }
    }
}
